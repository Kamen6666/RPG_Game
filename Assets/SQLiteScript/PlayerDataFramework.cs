using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerDataFramework : SQLFramrwork
{
    #region 单例  
    private static PlayerDataFramework instance;
    private PlayerDataFramework() { }
    public static PlayerDataFramework GetInstance()
    {
        if (instance == null)
        {
            instance = new PlayerDataFramework();
        }
        return instance;
    }
    #endregion


    #region PlayerDataFrameword

    private string sqlQuery = "";

    #region Select

    /// <summary>
    /// 查询数据库中除装备名称以外的装备属性
    /// </summary>
    /// <param name="EquipName">装备名称</param>
    /// <returns>返回值按 HP MP ATK DEF Price LV 顺序</returns>
    public int[] SelectEquipData(string EquipName)
    {
        sqlQuery = $"select Equipment_HP,Equipment_MP,Equipment_ATK,Equipment_DEF,Equipment_Price,Equipment_LV from Equipment_Page where Equipment_Name = '{EquipName}'";
        List<ArrayList> Equips = SelectMultipleData(sqlQuery);
        int[] equipData = new int[Equips[0].Count];
        for (int i = 0; i < Equips[0].Count; i++)
        {
            equipData[i] = Convert.ToInt32(Equips[0][i]);
        }
        return equipData;
    }
    
    /// <summary>
    /// 查询数据库中除消耗品名称以外的消耗品属性
    /// </summary>
    /// <param name="EquipName">消耗品名称</param>
    /// <returns>返回值按 HP MP ATK DEF Price LV 顺序</returns>
    public int[] SelectConsumableData(string ConsumableName)
    {
        sqlQuery = $"select Consumable_HP,Consumable_MP,Consumable_ATK,Consumable_DEF,Consumable_Price,Consumable_LV from Consumable_Page where Consumable_Name = '{ConsumableName}'";
        List<ArrayList> Consumable = SelectMultipleData(sqlQuery);
        int[] ConsumableData = new int[Consumable[0].Count];
        for (int i = 0; i < Consumable[0].Count; i++)
        {
            ConsumableData[i] = Convert.ToInt32(Consumable[0][i]);
        }
        return ConsumableData;
    }

    #endregion


    #region Insert

    /// <summary>
    /// 插入玩家装备
    /// </summary>
    /// <param name="PlayerName">玩家名称</param>
    /// <param name="EquipName">装备名称</param>
    /// <param name="Num">装备数量</param>
    public void InsertPlayerEquip(string PlayerName, int EquipID, int Num)
    {
        sqlQuery = $"insert into Player_Equip values ('{PlayerName}','{EquipID}',{Num})";
        JustExecute(sqlQuery);
    }
    
    /// <summary>
    /// 插入玩家消耗品
    /// </summary>
    /// <param name="PlayerName">玩家名称</param>
    /// <param name="EquipName">消耗品名称</param>
    /// <param name="Num">消耗品数量</param>
    public void InsertPlayerConsumables(string PlayerName, int ConsumablesID, int Num)
    {
        sqlQuery = $"insert into Player_Consumables values ('{PlayerName}','{ConsumablesID}',{Num})";
        JustExecute(sqlQuery);
    }
    
    /// <summary>
    /// 插入玩家材料
    /// </summary>
    /// <param name="PlayerName">玩家名称</param>
    /// <param name="EquipName">材料名称</param>
    /// <param name="Num">材料数量</param>
    public void InsertPlayerMaterial(string PlayerName, int MaterialID, int Num)
    {
        sqlQuery = $"insert into Player_Material values ('{PlayerName}','{MaterialID}',{Num})";
        JustExecute(sqlQuery);
    }

    #endregion

    #endregion

    #region 新游戏

    public void DeletePlayerPos(int rowid)
    {
        sqlQuery = $"delete from Player_Pos where rowid = {rowid}";
        JustExecute(sqlQuery);
    }

    public void DeletePlayer(int rowid)
    {
        sqlQuery = $"delete from Player_Page where rowid = {rowid}";
        JustExecute(sqlQuery);
    }

    public void InsertPlayer(string playerName, int[] data)
    {

        string s = "{\"skillBox\":[\"\",\"\",\"\",\"\",\"\",\"\"],\"equipBoxs\":[{\"equipBoxName\":\"Head\",\"equipIndex\":0},{\"equipBoxName\":\"Chest\",\"equipIndex\":0},{\"equipBoxName\":\"Leg\",\"equipIndex\":0},{\"equipBoxName\":\"Ring\",\"equipIndex\":0},{\"equipBoxName\":\"Hand\",\"equipIndex\":0}]}";
        sqlQuery = $"insert into Player_Page values ('{playerName}',{data[0]},{data[1]},{data[2]},{data[3]},{data[4]},{data[5]},{data[6]},'"+s+"')";
        JustExecute(sqlQuery);
    }

    public void InsertPlayerPos(string playerName,Vector3 pos)
    {
        sqlQuery = $"insert into Player_Pos values ('{playerName}',{pos.x},{pos.y},{pos.z})";
        JustExecute(sqlQuery);
    }
    #endregion

    #region 进入游戏时需要调取的方法

    /// <summary>
    /// 查询数据库是否有玩家
    /// </summary>
    /// <returns></returns>
    public bool SelectIsHavePlayerName()
    {
        string[] Names_str = SelectPlayerName();
        if (Names_str.Length == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    /// <summary>
    /// 查询数据库中有哪些玩家名字
    /// </summary>
    /// <returns>玩家名字</returns>
    public string[] SelectPlayerName()
    {
        sqlQuery = $"select Player_Name from Player_Page";
        List<ArrayList> names = SelectMultipleData(sqlQuery);
        string[] playerNames_str = new string[names.Count];
        for (int i = 0; i < names.Count; i++)
        {
            playerNames_str[i] = names[i][0].ToString();
        }
        return playerNames_str;
    }

    /// <summary>
    /// 查询除玩家名字外的玩家数据
    /// </summary>
    /// <param name="PlayerName">玩家名字</param>
    /// <returns>从前到后依次为 等级，经验，金钱，血量，蓝量，攻击，防御</returns>
    public List<object> SelectPlayerData(string PlayerName)
    {
        sqlQuery = $"select * from Player_Page where Player_Name = '{PlayerName}'";
        List<ArrayList> Data = SelectMultipleData(sqlQuery);
        List<object> playerdata = new List<object>();
        //Debug.Log(Data[0].Count);
        for (int i = 0; i < Data[0].Count; i++)
        {
            //Debug.Log("i:"+i+" Data:"+Data[0][i].ToString());
            playerdata.Add(Data[0][i]);
        }
        return playerdata;
    }

    /// <summary>
    /// 查询玩家下线时位置
    /// </summary>
    /// <param name="PlayerName">玩家名称</param>
    /// <returns>下线位置</returns>
    public Vector3 SelectPlayerPos(string PlayerName)
    {
        sqlQuery = $"select x,y,z from Player_Pos where Player_Name = '{PlayerName}'";
        List<ArrayList> Data = SelectMultipleData(sqlQuery);
        float[] Pos = new float[Data[0].Count];
        for (int i = 0; i < Data[0].Count; i++)
        {
            Pos[i] = Convert.ToSingle(Data[0][i]);
        }
        float x = Pos[0];
        float y = Pos[1];
        float z = Pos[2];
        Vector3 PlayerPos = new Vector3(x,y,z);
        return PlayerPos;
    }

    /// <summary>
    /// 查询玩家背包中具有的装备和对应数量
    /// </summary>
    /// <param name="PlayerName">玩家名称</param>
    /// <returns>字典键为装备名称，值为数量</returns>
    public List<int> SelectPlayerEquipAndNum(string PlayerName)
    {
        sqlQuery = $"select Equip_Name from Player_Equip where Player_Name = '{PlayerName}'";
        List<ArrayList> Equips = SelectMultipleData(sqlQuery);
        List<int> EquipName = new List<int>();
        for (int i = 0; i < Equips.Count; i++)
        {
            EquipName.Add(Convert.ToInt32(Equips[i][0]));
        }
        return EquipName;
    }

    /// <summary>
    /// 查询玩家背包中具有的消耗品和对应数量
    /// </summary>
    /// <param name="PlayerName">玩家名称</param>
    /// <returns>字典键为消耗品名称，值为数量</returns>
    public List<int> SelectPlayerConsumablesAndNum(string PlayerName,out List<int> num)
    {
        sqlQuery = $"select Consumables_Name, Consumables_Num from Player_Consumables where Player_Name = '{PlayerName}'";
        List<ArrayList> Consumables = SelectMultipleData(sqlQuery);
        List<int> PropName = new List<int>();
        num = new List<int>();
        for (int i = 0; i < Consumables.Count; i++)
        {
            PropName.Add(Convert.ToInt32(Consumables[i][0]));
            num.Add(Convert.ToInt32(Consumables[i][1]));
        }
        return PropName;
    }

    /// <summary>
    /// 查询玩家背包中具有的材料和对应数量
    /// </summary>
    /// <param name="PlayerName">玩家名称</param>
    /// <returns>字典键为材料名称，值为数量</returns>
    public List<int> SelectPlayerMaterialAndNum(string PlayerName,out List<int> num)
    {
        sqlQuery = $"select Material_Name, Material_Num from Player_Material where Player_Name = '{PlayerName}'";
        List<ArrayList> Materials = SelectMultipleData(sqlQuery);
        List<int> PropName = new List<int>();
        num = new List<int>();
        for (int i = 0; i < Materials.Count; i++)
        {
            PropName.Add(Convert.ToInt32(Materials[i][0]));
            num.Add(Convert.ToInt32(Materials[i][1]));
        }
        return PropName;
    }

    #endregion


    #region 退出游戏时向数据库写入的方法

    /// <summary>
    /// 更新玩家数据
    /// 注意！！！ 参数playerData必须按 (等级，经验，金钱，血量，蓝量，攻击，防御) 顺序。
    /// </summary>
    /// <param name="PlayerName">玩家名字</param>
    /// <param name="playerData">玩家信息</param>
    public void UpdatePlayerData(string PlayerName, int[] playerData)
    {
        string shortcutJson = PlayerManager.instance.CreateShortcutJson();
        sqlQuery = $"update Player_Page set Player_LV = {playerData[0]},Player_Exp = {playerData[1]},Player_Gold = {playerData[2]},Player_HP = {playerData[3]},Player_MP = {playerData[4]},Player_ATK = {playerData[5]},Player_DEF = {playerData[6]},Player_shortcut='{shortcutJson}' where Player_Name = '{PlayerName}'";
        JustExecute(sqlQuery);
    }

    /// <summary>
    /// 向数据库写入玩家位置
    /// </summary>
    /// <param name="PlayerName">玩家名称</param>
    /// <param name="playerPos">玩家位置</param>
    public void WritePlayerPos(string PlayerName,Vector3 playerPos)
    {
        sqlQuery = $"update Player_Pos set x = {playerPos.x},y = {playerPos.y},z = {playerPos.z} where Player_Name = '{PlayerName}'";
        JustExecute(sqlQuery);
    }

    /// <summary>
    /// 向数据库写入玩家装备和数量
    /// </summary>
    /// <param name="PlayerName">玩家名称</param>
    /// <param name="crtEquip">当前装备</param>
    public void WritePlayerEquipAndNum(string PlayerName, List<PropItems_Enum> Equipbags)
    {
        sqlQuery = $"DELETE FROM Player_Equip";
        JustExecute(sqlQuery);
        for (int i = 0; i < Equipbags.Count; i++)
        {
            InsertPlayerEquip(PlayerName, (int)Equipbags[i], (int)Equipbags[i] == 0 ? 0 : 1);
        }
    }
    
    /// <summary>
    /// 向数据库写入玩家消耗品和数量
    /// </summary>
    /// <param name="PlayerName">玩家名称</param>
    /// <param name="crtEquip">当前消耗品</param>
    public void WritePlayerConsumablesAndNum(string PlayerName, List<PropItems_Enum> ConsumablesBags,List<int> num)
    {
        sqlQuery = $"DELETE FROM Player_Consumables";
        JustExecute(sqlQuery);
        for (int i = 0; i < ConsumablesBags.Count; i++)
        {
            InsertPlayerConsumables(PlayerName, (int)ConsumablesBags[i], num[i]);
        }
    }
  
    /// <summary>
    /// 向数据库写入玩家材料和数量
    /// </summary>
    /// <param name="PlayerName">玩家名称</param>
    /// <param name="crtEquip">当前材料</param>
    public void WritePlayerMaterialsAndNum(string PlayerName, List<PropItems_Enum> crtMaterialsBags,List<int> num)
    {
        sqlQuery = $"DELETE FROM Player_Material";
        JustExecute(sqlQuery);
        for (int i = 0; i < crtMaterialsBags.Count; i++)
        {
            InsertPlayerMaterial(PlayerName, (int)crtMaterialsBags[i], num[i]);
        }
    }

    #endregion
}
