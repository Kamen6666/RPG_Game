using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentData :PropItem
{
    /// <summary>
    /// 力量
    /// </summary>
    public int Strength { get; set; }

    /// <summary>
    /// 防御力
    /// </summary>
    public int Defence { get; set; }

    /// <summary>
    /// 法力值
    /// </summary>
    public int MP { get; set; }

    /// <summary>
    /// 体力
    /// </summary>
    public int HP { get; set; }

    /// <summary>
    /// 等级
    /// </summary>
    public int Level { get; set; }

    public PlayerType playerType;
    /// <summary>
    /// 装备类型
    /// </summary>
    public EquipmentType EquipType { get; set; }

    #region 枚举类型

    /// <summary>
    /// 装备类型
    /// </summary>
    public enum EquipmentType
    {
        None,       //空
        Head,       //头部
        Chest,      //胸甲
        Hand,       //手部
        Ring,       //戒指
        Leg,        //腿部
    }
    public enum PlayerType 
    { 
        a_Magician, 
        b_Werwolf, 
        c_Archer, 
        d_Knight 
    };
    #endregion 枚举类型
    /// <summary>
    /// 构造方法
    /// </summary>
    /// 父类构造
    /// <param name="id">ID</param>
    /// <param name="itemType">物品类型</param>
    /// <param name="des">描述</param>
    /// <param name="capacity">容量</param>
    /// <param name="sprite">图标</param>
    /// 本身构造
    /// <param name="level">等级</param>
    /// <param name="strength">力量</param>
    /// <param name="defence">防御力</param>
    /// <param name="hp">hp</param>
    /// <param name="equipType">装备类型</param>
    public EquipmentData(PropItems_Enum id,string des, ItemType itemType,  int capacity, int price, string sprite,
        int level, int strength, int defence, int hp,int mp, EquipmentType equipType, PlayerType playerType)
        : base(id, des, itemType, capacity, price, sprite)
    {
        this.playerType = playerType;
        this.Level = level;
        this.Strength = strength;
        this.Defence = defence;
        this.HP = hp;
        this.MP = mp;
        this.EquipType = equipType;
        this.Capacity = 1;
    }

  

    
}
