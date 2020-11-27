using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipItemController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    , IPointerDownHandler
{
    //设置装备栏装备类型
    public EquipmentData.EquipmentType equipmentType;
    //信息显示框
    private Transform EquipInfo;
    private ViewMananger vm;
    private Image childImage;
    void OnEnable()
    {
        //获取info的位置
        EquipInfo = transform.root.Find("EquipItemInfo").transform;
        //获取view管理器
        vm = GameObject.FindGameObjectWithTag("GameController").GetComponent<ViewMananger>();
        //获取子类显示的图片
        childImage = transform.GetChild(0).GetComponent<Image>();
    }
    private EquipmentData currrentEquip;
    public void OnPointerEnter(PointerEventData eventData)
    {     
        //如果子物体显示 则当前部位有装备
        if (childImage.gameObject.activeSelf)
        {
            //传入部位类型返回道具编号
            currrentEquip  = (EquipmentData)PropData.Props[vm.GetEquipItem(equipmentType)];
            //移动到鼠标位置
            EquipInfo.transform.position = transform.position;
            //名字
            EquipInfo.GetChild(0).GetComponent<Text>().text = currrentEquip.ID.ToString();
            //类型
            EquipInfo.GetChild(1).GetComponent<Text>().text = transform.parent.name;
            //装备属性
            EquipInfo.Find("equipinfo").GetComponent<Text>().text = ShowEquipInfo(currrentEquip);
            //介绍
            EquipInfo.GetChild(4).GetComponent<Text>().text = currrentEquip.Description;
            //显示
            EquipInfo.gameObject.SetActive(true);
        }
    }
    private string ShowEquipInfo(EquipmentData pi)
    {
        string s = "";
        if (pi.Strength > 0)
        {
            s += "攻击力 +" + pi.Strength + "\n";
        }
        if (pi.Defence > 0)
        {
            s += "防御力 +" + pi.Defence + "\n";
        }
        if (pi.HP > 0)
        {
            s += "生命值 +" + pi.HP + "\n";
        }
        if (pi.MP > 0)
        {
            s += "魔法值 +" + pi.MP + "\n";
        }
        return s;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        //隐藏
        EquipInfo.gameObject.SetActive(false);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        //按下左键脱下装备
        if (Input.GetMouseButtonDown(0) && transform.GetChild(0).gameObject.activeSelf)
        {
            //传入部位类型返回道具编号
            currrentEquip = (EquipmentData)PropData.Props[vm.GetEquipItem(equipmentType)];
            //查看背包有没有格子
            bool IsEmpty = vm.HaveEmpty(vm.GetEquipItem(equipmentType), ItemType.Equipment, 1);
            if (IsEmpty)
            {
                //放入背包
                vm.GetNewMateria(vm.GetEquipItem(equipmentType), ItemType.Equipment, 1);
                //去除装备栏装备
                vm.TakeoffEquip(equipmentType);
            }
        }
    }
}
