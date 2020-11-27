
using LitJson;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //唯一实例
    public static PlayerManager instance;
    //玩家预制体
    public GameObject Player_prefabs;
    //背包
    public List<BagData> bagDatas;
    //装备栏
    public EquipItemData eid;
    //vm组件
    private ViewMananger vm;
    void Awake()
    {
        //获取组件
        vm = GetComponent<ViewMananger>();
        //实例化自己
        instance = this;
        //添加背包资源
        bagDatas.Add(Resources.Load<BagData>("Bag/EquipBagData"));
        bagDatas.Add(Resources.Load<BagData>("Bag/PropBagData"));
        bagDatas.Add(Resources.Load<BagData>("Bag/MaterialBagData"));
        //添加装备栏资源
        eid = Resources.Load<EquipItemData>("Bag/EquipItem");
        //读取数据
        instance.SetPlayerState();
        //初始化速度
        instance.speed = 3f;
        //初始化宠物属性
        petState = new int[5];
    }
    //是否读取出数据
    public bool isSet = false;
    //玩家名字
    public string playername;
    //生命值
    private int crtHp;
    //最大生命值
    public int maxHp;
    //法力值
    private int crtMp;
    //最大法力值
    public int maxMp;
    //材料是否满了
    public bool isMatMax;
    //攻击力
    public int atk;
    //防御力
    public int def;
    //移动速度
    public float speed;
    //当前经验值
    public int exp;
    //当前等级
    public int level;
    //金钱
    public int money;
    //宠物加成
    public int[] petState;
    //玩家初始位置
    public Vector3 playerpoint;
    //玩家当前位置
    public Vector3 playerCrtPos;
    //设置完毕属性 则进行调用
    public void SetPlayerState()
    {
        isSet = true;
        //打开数据库连接
        PlayerDataFramework.GetInstance().OpenSQLDataBase("RPG_Game");
        //角色名称
        playername = PlayerDataFramework.GetInstance().SelectPlayerName()[0];
        //获取人物属性列表
        List<object> d = new List<object>();
        d = PlayerDataFramework.GetInstance().SelectPlayerData(playername);
        //获取人物坐标列表
        playerpoint = PlayerDataFramework.GetInstance().SelectPlayerPos(playername);
        //获取人物装备背包列表
        bagDatas[0].GetData(PlayerDataFramework.GetInstance().SelectPlayerEquipAndNum(playername));
        //获取人物消耗品背包列表
        bagDatas[1].GetData(PlayerDataFramework.GetInstance().SelectPlayerConsumablesAndNum(playername, out bagDatas[1].propNum), 1);
        //获取人物材料背包列表
        bagDatas[2].GetData(PlayerDataFramework.GetInstance().SelectPlayerMaterialAndNum(playername, out bagDatas[2].propNum), 1);
        //关闭数据库连接
        PlayerDataFramework.GetInstance().CloseDataBase();
        //人物属性赋值
        level = Convert.ToInt32(d[1]);
        exp = Convert.ToInt32(d[2]);
        money = Convert.ToInt32(d[3]);
        crtHp = Convert.ToInt32(d[4]);
        crtMp = Convert.ToInt32(d[5]);
        //给场景中的玩家赋予初始位置
        GameObject.FindWithTag("Player").transform.position = playerpoint;

        //给场景中的快捷栏赋予初始技能
        GameObject[] go = GameObject.FindGameObjectsWithTag("Shortcut");
        string s = d[8].ToString();
        //Debug.Log(s);
        LitJson.JsonData jd = JsonMapper.ToObject(s);
        //Debug.Log(jd["skillBox"][0]);
        //Debug.Log(jd["equipBoxs"][0]["equipBoxName"]);
        for (int i = 0; i < go.Length; i++)
        {
            //读取技能快捷栏
            go[i].GetComponent<ShortCutGrid>().GetSkillName(jd["skillBox"][i].ToString());
        }
        
        for (int i = 0; i < eid.keys.Count; i++)
        {
            //读取装备栏信息
            eid.values[i] = (PropItems_Enum)Convert.ToInt32(jd["equipBoxs"][i]["equipIndex"].ToString());
        }
        
        //刷新当前最大属性
        /*
        maxHp = level * 15 + 80;
        maxMp = level * 25 + 50;
        atk = level * 10 + 110;
        def = level * 3 + 5;
        */
        //读取数据完毕
    }
    //生成玩家
    public void SetPlayerPoint()
    {
        Instantiate(Player_prefabs).transform.position = playerpoint;
    }
    //返回玩家属性  方便调用
    public int[] PlayerData()
    {
        int[] a = { level, exp, money, CrtHp, CrtMp, atk, def };
        return a;
    }
    //返回玩家当前位置
    public Vector3 PlayerPos()
    {
        playerCrtPos = GameObject.FindWithTag(GameConst.PLAYER_TAG).transform.position;
        playerCrtPos = new Vector3(playerCrtPos.x, 0.5f, playerCrtPos.z);
        return playerCrtPos;
    }
    //玩家扣血
    public void FallBlood(int result)
    {
        result -= instance.def;
        result = result < 0 ? 0 : result;
        CrtHp -= result;
    }
    //玩家升级
    public void UpLeve(int getExp)
    {
        exp += getExp;
        while (exp >= level * 50)
        {
            exp -= level * 50;
            level++;
            //恢复血量蓝量
            CrtHp = 9999999;
            CrtMp = 9999999;
            vm.ShowSkillData();
        }
    }
    public int CrtHp
    {
        get => crtHp;
        set
        {
            crtHp = (crtHp > maxHp) ? maxHp : value;
            crtHp = crtHp < 0 ? 0 : value;
        }
    }
    public int CrtMp
    {
        get => crtMp;
        set
        {
            crtMp = (crtMp > maxMp) ? maxMp : value;
            crtMp = crtMp < 0 ? 0 : value;
        }
    }

    //玩家快捷键设置信息转为JSON
    public String CreateShortcutJson()
    {
        GameObject[] go = GameObject.FindGameObjectsWithTag("Shortcut");

        Msg msg = new Msg();
        msg.skillBox = new string[6];
        msg.equipBoxs = new EquipBox[5];
        for (int i = 0; i < go.Length; i++)
        {
            msg.skillBox[i] = go[i].GetComponent<ShortCutGrid>().SkillName;
        }
        for (int i = 0; i < eid.keys.Count; i++)
        {
            msg.equipBoxs[i] = new EquipBox(eid.keys[i].ToString(), (int)eid.values[i]);
        }
        return JsonUtility.ToJson(msg);
    }
}

[System.Serializable]
public class Msg
{
    public string[] skillBox;
    public EquipBox[] equipBoxs;
}

[System.Serializable]
public class EquipBox
{
    public string equipBoxName;
    public int equipIndex;

    public EquipBox(string equipBoxName, int equipIndex)
    {
        this.equipBoxName = equipBoxName;
        this.equipIndex = equipIndex;
    }
}
