using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectHeroManager : MonoBehaviour
{
    public Text Name_txt;
    public BagData[] bagdatas;
    public EquipItemData equips;
    private GameObject[] Heros;
    private int CurrentHeros;
    private PlayerDataFramework playerDataFramework;
    void Start()
    {
        Heros = GameObject.FindGameObjectsWithTag("Player");

        for (int i = 0; i < Heros.Length; i++)
        {
            Heros[i].SetActive(false);
        }
        CurrentHeros = 0;
        Heros[CurrentHeros].SetActive(true);

        playerDataFramework = PlayerDataFramework.GetInstance();
    }
    //挑选英雄
    public void ChangeHero(int num)
    {
        //当前英雄隐藏
        Heros[CurrentHeros].SetActive(false);
        //下标增加
        CurrentHeros += num;
        //处理下标
        if (CurrentHeros < 0)
            CurrentHeros = Heros.Length - 1;
        CurrentHeros %= Heros.Length;
        //显示下个英雄
        Heros[CurrentHeros].SetActive(true);
    }
    //选择英雄
    public void ConfirmHero()
    {
        if (Name_txt.text.Length < 2)
        {
            Debug.Log("请输入游戏名字");
            return;
        }

        if (Name_txt.text.Length > 6)
        {
            Debug.Log("游戏名字过长");
            return;
        }
        //生成进度条
        go = Instantiate(Resources.Load<GameObject>("Panel"));
        //设置进度
        go.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Slider>().value = 0.1f;
        //开始加载场景
        StartCoroutine(LoadScene("GameScene"));
        //进入游戏场景

    }
    GameObject go;
    bool isFinish;
    AsyncOperation SG;
    //场景预先加载协成
    IEnumerator LoadScene(string name)
    {
        isFinish = true;
        //延迟1秒
        yield return new WaitForSeconds(1);
        //设置进度
        go.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Slider>().value = 0.2f;
        //加载信息
        //选择存档
        string playerName = Name_txt.text;
        //初始化存档属性
        PlayerDataFramework.GetInstance().OpenSQLDataBase("RPG_Game");
        int[] state = new int[] { 1, 0, 0, 99999, 99999, 99999, 99999 };
        playerDataFramework.DeletePlayerPos(1);
        playerDataFramework.DeletePlayer(1);
        //初始化存档玩家属性
        playerDataFramework.InsertPlayer(playerName, state);
        //初始化存档玩家坐标
        playerDataFramework.InsertPlayerPos(playerName, new Vector3(180, 0, 190));
        //初始化数据库内玩家背包信息
        BagData bd = new BagData();
        bd.propName = new List<PropItems_Enum>();
        bd.propNum = new List<int>();
        for (int i = 0; i < 30; i++)
        {
            bd.propName.Add(PropItems_Enum.空);
            bd.propNum.Add(0);
        }
        //装备背包
        playerDataFramework.WritePlayerEquipAndNum(playerName, bd.propName);
        //消耗品背包
        playerDataFramework.WritePlayerConsumablesAndNum(playerName, bd.propName, bd.propNum);
        //材料背包
        playerDataFramework.WritePlayerMaterialsAndNum(playerName, bd.propName, bd.propNum);
        //延迟1秒
        yield return new WaitForSeconds(1);
        //设置进度
        go.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Slider>().value = 0.4f;
        //延迟1秒
        yield return new WaitForSeconds(1);
        //设置进度
        go.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Slider>().value = 0.7f;
        //加载场景
        SG = SceneManager.LoadSceneAsync(name);
        SG.allowSceneActivation = false;
        while (SG.progress<0.8f)
        {
            //场景预先加载
            yield return 0;
        }
        yield return new WaitForSeconds(3);
        //设置进度
        go.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Slider>().value = 1f;
        SG.allowSceneActivation = true;
    }

}
