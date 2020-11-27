using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : SingleView<SettingPanel>
{
    public MenuPanel menuPanel;
    public Slider slider_bgMusic;  //背景音乐
    public Slider slider_SFX;      //音效

    protected override void Awake()
    {
        base.Awake();
        slider_bgMusic = transform.Find("BGM/Slider").GetComponent<Slider>();
        slider_SFX = transform.Find("SFX/Slider").GetComponent<Slider>();
    }

    public void OnBackClick()
    {
        this.Hide();
        if (menuPanel != null)
        {
            menuPanel.Show();
        }

    }
    public void OnExitClick()
    {
            if (PlayerManager.instance.isSet)
            {
                SavePlayerData();
            }
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
    public void SavePlayerData()
    {
        //开启数据库
        PlayerDataFramework.GetInstance().OpenSQLDataBase("RPG_Game");
        //存入玩家信息
        PlayerDataFramework.GetInstance().UpdatePlayerData(PlayerManager.instance.playername, PlayerManager.instance.PlayerData());
        //存入玩家位置
        PlayerDataFramework.GetInstance().WritePlayerPos(PlayerManager.instance.playername, PlayerManager.instance.PlayerPos());
        //存入玩家背包信息
        //装备背包
        PlayerDataFramework.GetInstance().WritePlayerEquipAndNum(PlayerManager.instance.playername, PlayerManager.instance.bagDatas[0].propName);
        //道具背包
        PlayerDataFramework.GetInstance().WritePlayerConsumablesAndNum(PlayerManager.instance.playername, PlayerManager.instance.bagDatas[1].propName, PlayerManager.instance.bagDatas[1].propNum);
        //材料背包
        PlayerDataFramework.GetInstance().WritePlayerMaterialsAndNum(PlayerManager.instance.playername, PlayerManager.instance.bagDatas[2].propName, PlayerManager.instance.bagDatas[2].propNum);
        //关闭数据库
        PlayerDataFramework.GetInstance().CloseDataBase();
    }

    #region  事件监听

    public void OnMusicValueChange(float f)
    {
        //保存音量
        PlayerPrefs.SetFloat("BGMVolume", f);
        //修改大小
        AudioManager.Instance.ChangeBGMVolume(f);
    }

    public void OnSFXValueChange(float f)
    {
        //保存音效
        PlayerPrefs.SetFloat("SFXVolume", f);
        //修改大小
        AudioManager.Instance.ChangeSFXVolume(f);
    }


    #endregion

    #region  重写方法

    public override void Show()
    {
        //获取当前音量大小，进行赋值
        base.Show();
        slider_bgMusic.value = PlayerPrefs.GetFloat("BGMVolume");
        slider_SFX.value = PlayerPrefs.GetFloat("SFXVolume");
    }

    #endregion
}
