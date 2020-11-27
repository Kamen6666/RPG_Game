using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BagItemController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    , IBeginDragHandler, IDragHandler, IEndDragHandler,IPointerDownHandler
{
    //信息显示框
    private Transform stateInfo;
    private ViewMananger vm;
    private bool ishave;
    void OnEnable()
    {
        //获取info的位置
        stateInfo = transform.root.Find("ItemInfo").transform;
        //获取view管理器
        vm = GameObject.FindGameObjectWithTag("GameController").GetComponent<ViewMananger>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        //如果没子物体则不显示
        if (transform.childCount < 1 || !transform.GetChild(0).gameObject.activeSelf)
            return;
        //更新位置
        stateInfo.transform.position = transform.position;
        int num = transform.GetSiblingIndex();
        //更新显示位置
        if ((num + 1) % 5 == 0)
            stateInfo.GetComponent<RectTransform>().pivot = new Vector2(1, 1);
        else
            stateInfo.GetComponent<RectTransform>().pivot = new Vector2(0, 1);
        //获取当前格子物品属性
        PropItem minfo = vm.GetPropItem(num, (ItemType)transform.parent.GetSiblingIndex());
        
        //名字
        stateInfo.GetChild(0).GetComponent<Text>().text = minfo.ID.ToString();
        //类型
        stateInfo.GetChild(1).GetComponent<Text>().text = transform.parent.name;
        //如果是装备显示属性
        if (transform.parent.CompareTag("Equipment"))
        {
            stateInfo.Find("equipinfo").GetComponent<Text>().text = ShowEquipInfo((EquipmentData)minfo);
        }
        else
        {
            //不是装备不显示属性
            stateInfo.Find("equipinfo").GetComponent<Text>().text = "";
        }
        //介绍
        stateInfo.GetChild(4).GetComponent<Text>().text = minfo.Description;
        //显示
        stateInfo.gameObject.SetActive(true);

    }
    private string ShowEquipInfo(EquipmentData pi)
    {
        string s = "";
        if (pi.Strength > 0)
        {
            s += "攻击力 +" + pi.Strength+"\n";
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
        stateInfo.gameObject.SetActive(false);
    }
    private Transform ChildImage;
    public void OnBeginDrag(PointerEventData eventData)
    {

        stateInfo.gameObject.SetActive(false);
        //抓取子物体
        ChildImage = transform.GetChild(0);
        //子物体更换父类
        ChildImage.SetParent(transform.root);
    }
    public void OnDrag(PointerEventData eventData)
    {
        //跟随鼠标移动
        ChildImage.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //回到父类下
        ChildImage.SetParent(transform);
        //重置坐标
        ChildImage.localPosition = Vector3.zero;
        //检测底下格子是否是同父类
        if (eventData.pointerEnter && eventData.pointerEnter.transform.parent == transform.parent)
        {
            //进行格子内物品交换
            vm.BagItemCharge(transform.GetSiblingIndex(), eventData.pointerEnter.transform.GetSiblingIndex(), (ItemType)transform.parent.GetSiblingIndex());
        }
        else if(eventData.pointerEnter && eventData.pointerEnter.transform.CompareTag("ShopItem"))
        {
            //丢到商店 则直接出售
            vm.SellPropItem(transform.GetSiblingIndex(), (ItemType)transform.parent.GetSiblingIndex());
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //按下右键穿戴装备
        if (Input.GetMouseButtonDown(1) && transform.parent.CompareTag("Equipment"))
        {
            vm.SwapEquip(transform.GetSiblingIndex());
        }
    }
}
