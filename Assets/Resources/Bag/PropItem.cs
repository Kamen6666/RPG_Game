using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 物品类型
/// </summary>
public enum ItemType
{
    Equipment,  //装备
    Consumable, //消耗品
    Material,   //材料
}

public class PropItem
{
    #region 属性
    /// <summary>
    /// ID
    /// </summary>
    public PropItems_Enum ID { get; set; }



    /// <summary>
    /// 物品类型
    /// </summary>
    public ItemType ItemType { get; set; }


    
    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 描述
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// 容量
    /// </summary>
    public int Capacity { get; set; }

    /// <summary>
    /// 价格
    /// </summary>
    public int Price { get; set; }

    /// <summary>
    /// 图标
    /// </summary>
    public string Sprite { get; set; }

    #endregion 属性

    /// <summary>
    /// 构造方法
    /// </summary>
    /// <param name="id">ID</param>
    /// <param name="name">名称</param>
    /// <param name="itemType">物品类型</param>
    /// <param name="des">描述</param>
    /// <param name="capacity">容量</param>
    /// <param name="Price">购买价格</param>
    /// <param name="sprite">图标</param>
    public PropItem(PropItems_Enum id, string des, ItemType itemType, int capacity, int Price, string sprite)
    {
        this.ID = id;
        this.Description = des;
        this.ItemType = itemType;
        this.Capacity = capacity;
        this.Price = Price;
        this.Sprite = sprite;
    }
    public PropItem(PropItem propItem)
    {
        this.ID = propItem.ID;
        this.Name = propItem.Name;
        this.ItemType = propItem.ItemType;
        this.Description = propItem.Description;
        this.Capacity = propItem.Capacity;
        this.Price = propItem.Price;
        this.Sprite = propItem.Sprite;
    }
}