using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class TestShop : MonoBehaviour,IPointerDownHandler
{
    ViewMananger viewMananger;

    private float moneny;
    private void Awake()
    {
        viewMananger = GameObject.FindWithTag("GameController").GetComponent<ViewMananger>();
    }
    //传参PropItems_Enum prop---prop名字, ItemType itemType--prop类型, int num--数量
    public void OnPointerDown(PointerEventData eventData)
    {
        Image a = transform.GetComponent<Image>();
        if (a.sprite.name == "ShopGround")
            return;
       // Debug.Log(a.sprite.name);
       //string强转枚举
        PropItems_Enum propItems_Enum = (PropItems_Enum)Enum.Parse(typeof(PropItems_Enum),a.sprite.name);
        //获取prop 枚举
        ItemType itemType = PropData.Props[propItems_Enum].ItemType;
        //当前道具需要的价格
        moneny = PropData.Props[propItems_Enum].Price;
        
        int num = 1;
        //判断背包是否有空位置
        bool isEmpty =  viewMananger.HaveEmpty(propItems_Enum, itemType, num);

        //钱够并且有空位
        if (isEmpty&& PlayerManager.instance.money >= moneny)
        {
            //如果有空 添加进去
            viewMananger.GetNewMateria(propItems_Enum, itemType, num);

            PlayerManager.instance.money -=(int) moneny;
        }
    }
}
