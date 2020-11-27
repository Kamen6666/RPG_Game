using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewBase : MonoBehaviour
{
    #region 方法
    //显示自身界面
    public virtual void Show()
    {
        transform.gameObject.SetActive(true);

    }


    //隐藏自身界面
    public virtual void Hide()
    {
        transform.gameObject.SetActive(false);

    }

    //判断自身界面是否显示
    public bool IsShow()
    {
        return gameObject.activeSelf;

    }
    #endregion

}
