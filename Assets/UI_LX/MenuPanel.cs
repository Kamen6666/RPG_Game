using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPanel : ViewBase
{
    public SettingPanel SettingPanel;
    private void Start()
    {
        PlayerDataFramework.GetInstance().OpenSQLDataBase("RPG_Game");
        if (!PlayerDataFramework.GetInstance().SelectIsHavePlayerName())
        {
            transform.Find("Button/btn_continue").gameObject.SetActive(false);
        }
        PlayerDataFramework.GetInstance().CloseDataBase();
    }
   
    public void OnSettingClick()
    {
        this.Hide();
        SettingPanel.Show();
    }
    public void OnExitClick()
    {
#if UNITY_EDITOR

        UnityEditor.EditorApplication.isPlaying = false;
#else
                            Application.Quit();
#endif
    }
}
