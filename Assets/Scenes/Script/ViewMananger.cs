using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewMananger : MonoBehaviour
{
    public Text P_Name;
    public Text level;
    public Text hp;
    public Text mp;
    public Text exp;
    public Slider Hp;
    public Slider Mp;
    public Slider Exp;

    void Awake()
    {
        //初始化材料列表
        PropData.GetPrpoData();
    }

    void Start()
    {
        //技能界面
        ShowSkillData();
        //背包界面
        BagData = PlayerManager.instance.bagDatas;
    }
    void Update()
    { 
        if (PlayerManager.instance.isSet)
        {
            //基础UI
            StateBase_1();
            //装备界面
            StateBase_2();
            //材料背包界面
            UpdateBag();
            //装备栏界面
            UpdateEquipItem();
        }
    }
    #region 玩家属性显示
    private void StateBase_1()
    {
        RefreshPlayerInfo();
        P_Name.text = PlayerManager.instance.playername;
        level.text = PlayerManager.instance.level.ToString();
        hp.text = PlayerManager.instance.CrtHp + " / " + PlayerManager.instance.maxHp;
        Hp.value = (float)PlayerManager.instance.CrtHp / (float)PlayerManager.instance.maxHp;
        mp.text = PlayerManager.instance.CrtMp + " / " + PlayerManager.instance.maxMp;
        Mp.value = (float)PlayerManager.instance.CrtMp / (float)PlayerManager.instance.maxMp;
        exp.text = PlayerManager.instance.exp + " / " + PlayerManager.instance.level * 50;
        Exp.value = (PlayerManager.instance.exp * 1f) / (PlayerManager.instance.level * 50f);
    }
    public void RefreshPlayerInfo()
    {
        //当前装备属性
        int[] equipItemInfo = AddEquipInfo();
        //刷新当前最大属性
        PlayerManager.instance.maxHp = PlayerManager.instance.level * 15 + 80 + equipItemInfo[2]+ PlayerManager.instance.petState[0];
        PlayerManager.instance.maxMp = PlayerManager.instance.level * 25 + 150 + equipItemInfo[3] + PlayerManager.instance.petState[1];
        PlayerManager.instance.atk = PlayerManager.instance.level * 10 + 110 + equipItemInfo[0] + PlayerManager.instance.petState[2];
        PlayerManager.instance.def = PlayerManager.instance.level * 3 + 5 + equipItemInfo[1] + PlayerManager.instance.petState[3];
        PlayerManager.instance.CrtHp = PlayerManager.instance.CrtHp > PlayerManager.instance.maxHp ? PlayerManager.instance.maxHp : PlayerManager.instance.CrtHp;
        PlayerManager.instance.CrtMp = PlayerManager.instance.CrtMp > PlayerManager.instance.maxMp ? PlayerManager.instance.maxMp : PlayerManager.instance.CrtMp;
    }
    #endregion
    #region 属性界面属性显示
    public Text stateHp;
    public Text stateMp;
    public Text stateAtk;
    public Text stateDef;
    public Text stateSpeed;
    public Text BagMoney;
    private void StateBase_2()
    {
        stateHp.text = PlayerManager.instance.CrtHp + "/" + PlayerManager.instance.maxHp;
        stateMp.text = PlayerManager.instance.CrtMp + "/" + PlayerManager.instance.maxMp;
        stateAtk.text = PlayerManager.instance.atk.ToString();
        stateDef.text = PlayerManager.instance.def.ToString();
        stateSpeed.text = (float)PlayerManager.instance.speed / 2f * 100 + "%";
        BagMoney.text = PlayerManager.instance.money.ToString();
    }
    #endregion
    #region 道具背包
    private List<BagData> BagData;//背包信息
    public List<Transform> BagFather;//背包父类
    //材料字典
    //刷新材料背包
    public void UpdateBag()
    {
        for (int j = 0; j < BagData.Count; j++)
        {
            for (int i = 0; i < BagFather[j].childCount; i++)
            {
                if (BagFather[j].GetChild(i).childCount < 1)
                    return;
                if (BagData[j].propName[i] == PropItems_Enum.空 || BagData[j].propNum[i] <= 0)
                {
                    //没物品则设置为空
                    BagData[j].propName[i] = PropItems_Enum.空;
                    //物品栏没物品则隐藏
                    BagFather[j].GetChild(i).GetChild(0).gameObject.SetActive(false);
                }
                else
                {
                    //物品栏有物品则读取属性
                    Image icon = BagFather[j].GetChild(i).GetChild(0).GetComponent<Image>();
                    //不是装备则读取数
                    Text count = icon.transform.GetChild(0).GetComponent<Text>();
                    //加载图片
                    icon.sprite = Resources.Load<Sprite>("图片/" + BagData[j].propName[i].ToString());
                    //装备背包 不显示道具数量
                    icon.transform.GetChild(0).gameObject.SetActive(BagData[j].itemType == ItemType.Equipment ? false : true);
                    //显示道具图片
                    icon.gameObject.SetActive(true);
                    //显示道具数量
                    count.text = BagData[j].propNum[i].ToString();
                }
            }
        }
    }
    //获取材料信息MaterialData.mats[material]
    //向背包内添加新材料
    public void GetNewMateria(PropItems_Enum prop, ItemType itemType, int num)
    {
        //背包数据
        BagData bagdata = BagData[0];
        //按照类型分类
        switch (itemType)
        {
            case ItemType.Equipment:
                bagdata = BagData[0];
                break;
            case ItemType.Consumable:
                bagdata = BagData[1];
                break;
            case ItemType.Material:
                bagdata = BagData[2];
                break;
            default:
                Debug.Log("没找到对应的背包");
                break;
        }
        //堆叠相同的材料
        for (int i = 0; i < bagdata.propName.Count; i++)
        {
            //判断物品种类是否相同
            if (bagdata.propName[i] == prop)
            {
                //判断是否有位置放
                if (bagdata.propNum[i] < PropData.Props[prop].Capacity)
                {
                    //判断当前格子能放几个
                    int SetCount = PropData.Props[prop].Capacity - bagdata.propNum[i];
                    //判断当前格子放得下吗
                    if (SetCount >= num)
                        bagdata.propNum[i] += num;
                    else
                        bagdata.propNum[i] += SetCount;
                    num -= SetCount;
                }
                //还有物体要放嘛
                if (num <= 0)
                    return;
            }
        }
        //多余的材料放在空的格子里
        for (int i = 0; i < bagdata.propName.Count; i++)
        {
            //Debug.Log(MaterialBagData.propName[i]);
            //判断是否有空位
            if (bagdata.propName[i] == PropItems_Enum.空)
            {
                //Debug.Log("11");
                //修改当前格子Icon图片
                bagdata.propName[i] = prop;
                //判断当前格子放得下吗
                int SetCount = PropData.Props[prop].Capacity;
                //判断当前格子放得下吗
                if (SetCount >= num)
                    bagdata.propNum[i] += num;
                else
                    bagdata.propNum[i] += SetCount;
                num -= SetCount;
            }
            //还有物体要放嘛
            if (num <= 0)
                return;
        }
    }
    //背包放得下吗
    public bool HaveEmpty(PropItems_Enum prop, ItemType itemType, int num)
    {
        //背包数据
        BagData bagdata = BagData[0];
        //按照类型分类
        switch (itemType)
        {
            case ItemType.Equipment:
                bagdata = BagData[0];
                break;
            case ItemType.Consumable:
                bagdata = BagData[1];
                break;
            case ItemType.Material:
                bagdata = BagData[2];
                break;
            default:
                Debug.Log("没找到对应的背包");
                break;
        }
        int CanCount = 0;
        for (int i = 0; i < bagdata.propName.Count; i++)
        {
            if (bagdata.propName[i] == prop)
                CanCount += PropData.Props[prop].Capacity - bagdata.propNum[i];
            if (bagdata.propName[i] == PropItems_Enum.空)
                CanCount += PropData.Props[prop].Capacity;
            if (CanCount >= num)
                return true;
        }
        return false;
    }

    //格子内物品交换
    public void BagItemCharge(int a, int b, ItemType itemType)
    {
        //背包根物体
        Transform bagroot = BagFather[0].transform;
        //背包数据
        BagData bagdata = BagData[0];
        //按照类型分类
        switch (itemType)
        {
            case ItemType.Equipment:
                bagroot = BagFather[0].transform;
                bagdata = BagData[0];
                break;
            case ItemType.Consumable:
                bagroot = BagFather[1].transform;
                bagdata = BagData[1];
                break;
            case ItemType.Material:
                bagroot = BagFather[2].transform;
                bagdata = BagData[2];
                break;
            default:
                Debug.Log("没找到对应的背包");
                break;
        }
        //编号交换
        PropItems_Enum m_name = bagdata.propName[a];
        bagdata.propName[a] = bagdata.propName[b];
        bagdata.propName[b] = m_name;
        //数量交换
        int m_count = bagdata.propNum[a];
        bagdata.propNum[a] = bagdata.propNum[b];
        bagdata.propNum[b] = m_count;
    }
    //背包内放得是啥
    public PropItem GetPropItem(int a, ItemType itemType)
    {
        //背包数据
        BagData bagdata = BagData[0];
        //按照类型分类
        switch (itemType)
        {
            case ItemType.Equipment:
                bagdata = BagData[0];
                break;
            case ItemType.Consumable:
                bagdata = BagData[1];
                break;
            case ItemType.Material:
                bagdata = BagData[2];
                break;
            default:
                Debug.Log("没找到对应的背包");
                break;
        }
        return PropData.Props[bagdata.propName[a]];
    }
    //背包内当前物品被使用
    public void UsePropItem(int index)
    {
        //背包根物体
        Transform bagroot = BagFather[1].transform;
        //背包数据
        BagData bagdata = BagData[1];

        //如果数量大于1则减减
        if (bagdata.propNum[index] > 0)
            bagdata.propNum[index]--;
    }
    //出售道具
    public void SellPropItem(int a, ItemType itemType)
    {
        //背包数据
        BagData bagdata = BagData[0];
        //按照类型分类
        switch (itemType)
        {
            case ItemType.Equipment:
                bagdata = BagData[0];
                break;
            case ItemType.Consumable:
                bagdata = BagData[1];
                break;
            case ItemType.Material:
                bagdata = BagData[2];
                break;
            default:
                Debug.Log("没找到对应的背包");
                break;
        }
        //获取游戏币
        PlayerManager.instance.money += PropData.Props[bagdata.propName[a]].Price * bagdata.propNum[a];
        //数据库归零
        bagdata.propName[a] = PropItems_Enum.空;
        bagdata.propNum[a] = 0;
    }
    //文本框
    public Text SetItemtxt;
    //文本框最大行数
    private int hangNum = 0;
    //材料获取测试
    public void SetItemText()
    {
        PropItems_Enum items = (PropItems_Enum)Random.Range((int)PropItems_Enum.铁锭, (int)PropItems_Enum.魔法师长裤);
        Debug.Log(items);
        //测试
        //MaterialBagData.propName[0] = items;
        //MaterialBagData.propNum[0] = count;

        //清空文本框
        hangNum++;
        if (hangNum % 10 == 0)
            SetItemtxt.text = "";
        //测试
        ItemType it = PropData.Props[items].ItemType;
        if (HaveEmpty(items, it, 1))
        {
            //放入道具
            GetNewMateria(items, it, 1);
            SetItemtxt.text += "\n" + items + " *1";
        }
        else
        {
            SetItemtxt.text += "\n背包放不下了";
        }
    }
    #endregion
    #region 技能界面
    //技能界面根物体
    public Transform SkillView;
    //技能数据库
    public SkillData skillData;
    //通过职业取出新的技能列表

    //刷新技能信息
    public void ShowSkillData()
    {
        //zSkillView.GetComponent<RectTransform>().sizeDelta = new Vector2(0f, (skillData.skills.Count * 105f + 120f));
        for (int i = 0; i < skillData.skills.Count; i++)
        {
            Transform item = SkillView.GetChild(i);
            //更新图标

            item.Find("icon").GetComponent<Image>().sprite
                = Resources.Load<Sprite>("Skill/" + skillData.skills[i].SkillIcon);

            //技能名字
            item.Find("name").GetComponent<Text>().text
                = skillData.skills[i].SkillName;
            //技能消耗
            item.Find("consume").GetComponent<Text>().text
                = skillData.skills[i].needmp + "Mp";
            //技能介绍
            item.Find("introduce").GetComponent<Text>().text
                = skillData.skills[i].SkillIntrduce;
            //判断技能等级是否到位
            if (PlayerManager.instance.level >= skillData.skills[i].level)
            {
                item.Find("lock").gameObject.SetActive(false);
            }
            else
            {
                item.Find("lock/Text").GetComponent<Text>().text = skillData.skills[i].level + "级解锁";
                item.Find("lock").gameObject.SetActive(true);
            }
        }
    }
    #endregion
    #region 装备栏界面
    //装备栏数据库
    public EquipItemData equipItemData;
    //装备栏
    public GameObject[] equipItems;
    //更新装备栏信息
    public void UpdateEquipItem()
    {
        for (int i = 0; i < 5; i++)
        {
            equipItems[i].GetComponent<EquipItemController>().equipmentType
                = equipItemData.keys[i];
            if (equipItemData.values[i] == PropItems_Enum.空)
            {
                equipItems[i].transform.GetChild(0).gameObject.SetActive(false);
                continue;
            }
            //物体名字改为道具编号
            equipItems[i].transform.GetChild(0).name = ((int)equipItemData.values[i]).ToString();
            //修改物体图片
            equipItems[i].transform.GetChild(0).GetComponent<Image>().sprite
                = Resources.Load<Sprite>("图片/" + equipItemData.values[i].ToString());
            //激活物体
            equipItems[i].transform.GetChild(0).gameObject.SetActive(true);

        }
    }
    //计算装备总属性
    public int[] AddEquipInfo()
    {
        //攻击 防御 生命 魔法
        int[] equipInfo = new int[4] { 0, 0, 0, 0 };
        for (int i = 0; i < equipItemData.values.Count; i++)
        {
            if (equipItemData.values[i] == PropItems_Enum.空)
                continue;
            EquipmentData ed = (EquipmentData)PropData.Props[equipItemData.values[i]];
            equipInfo[0] += ed.Strength;
            equipInfo[1] += ed.Defence;
            equipInfo[2] += ed.HP;
            equipInfo[3] += ed.MP;
        }
        return equipInfo;
    }
    //交换装备
    public void SwapEquip(int index)
    {
        //查询当前装备所在部位
        if (BagData[0].propName[index] == PropItems_Enum.空)
            return;
        EquipmentData ed = (EquipmentData)PropData.Props[BagData[0].propName[index]];
        EquipmentData.EquipmentType equipmentType = ed.EquipType;
        //查询对应部位装备栏是否有装备
        int num = equipItemData.keys.IndexOf(equipmentType);
        PropItems_Enum pe = equipItemData.values[num];
        //交换
        if (pe == PropItems_Enum.空)
        {
            equipItemData.values[num] = BagData[0].propName[index];
            BagData[0].propName[index] = PropItems_Enum.空;
        }
        else
        {
            PropItems_Enum temp = equipItemData.values[num];
            equipItemData.values[num] = BagData[0].propName[index];
            BagData[0].propName[index] = temp;
        }
    }
    //脱下装备
    public void TakeoffEquip(EquipmentData.EquipmentType equipmentType)
    {
        int index = equipItemData.keys.IndexOf(equipmentType);
        equipItemData.values[index] = PropItems_Enum.空;
    }
    //返回当前部位的装备
    public PropItems_Enum GetEquipItem(EquipmentData.EquipmentType equipmentType)
    {
        int index = equipItemData.keys.IndexOf(equipmentType);
        return equipItemData.values[index];
    }
    #endregion
}