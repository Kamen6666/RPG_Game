using UnityEngine;
using Mono.Data.Sqlite;
using System.Collections.Generic;
using System.Collections;

public class SQLFramrwork
{
    #region 单例
    private static SQLFramrwork instance;
    protected SQLFramrwork() { }
    public static SQLFramrwork GetInstance()
    {
        if (instance == null)
        {
            instance = new SQLFramrwork();
        }
        return instance;
    }
    #endregion

    #region SQL字段
    SqliteConnection con;
    SqliteCommand cmd;
    SqliteDataReader reader;
    #endregion

    #region SQL Frame
    /// <summary>
    /// 打开数据库
    /// </summary>
    /// <param name="dbName"></param>
    public void OpenSQLDataBase(string dbName)
    {
        //动态添加后缀
        if (!dbName.Contains(".sqlite"))
        {
            dbName += ".sqlite";
        }
        string dbPath = "";


#if UNITY_EDITOR || UNITY_STANDALONE
        dbPath = "Data Source = " + Application.streamingAssetsPath + "/" + dbName;
#endif
        con = new SqliteConnection(dbPath);
        con.Open();
        cmd = con.CreateCommand();

    }

    /// <summary>
    /// 关闭数据库
    /// </summary>
    public void CloseDataBase()
    {
        //清堆内存
        con.Close();
        cmd.Dispose();
        if (reader != null)
        {
            reader.Close();
        }
        
        //清栈内存 回收速度会快一点
        con = null;
        cmd = null;
        reader = null;
    }

    /// <summary>
    /// 仅仅执行SQL语句
    /// </summary>
    /// <param name="sqlQuery"></param>
    public void JustExecute(string sqlQuery)
    {
        cmd.CommandText = sqlQuery;
        cmd.ExecuteNonQuery();
    }

    /// <summary>
    /// 查询单个数据
    /// </summary>
    /// <param name="sqlQuery"></param>
    public object SelectSingleData(string sqlQuery)
    {
        cmd.CommandText = sqlQuery;
        return cmd.ExecuteScalar();
    }

    /// <summary>
    /// 查询多个数据
    /// </summary>
    /// <param name="sqlQuery"></param>
    public List<ArrayList> SelectMultipleData(string sqlQuery)
    {
        cmd.CommandText = sqlQuery;
        reader = cmd.ExecuteReader();
        List<ArrayList> result = new List<ArrayList>();
        while (reader.Read())
        {
            ArrayList rowData = new ArrayList();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                //当前行的当前列添加到rowData
                rowData.Add(reader.GetValue(i));
            }
            //将储存好的当前行数据添加到List
            result.Add(rowData);
        }
        reader.Close();
        return result;
    }
    #endregion
}
