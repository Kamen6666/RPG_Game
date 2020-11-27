using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// npc记载ui商店信息
/// </summary>
public class NpcLoadData : MonoBehaviour
{
    //npc信息
   private  NPCState npcState;
    //商城位置
    private Transform shop;
    //scollerView 的content
    private Transform content;
    //关闭按钮
    private Button closeBtn;

    private void Awake()
    {
        npcState = GetComponent<NPCState>();
       
    }
    
    private void OnEnable()
    {
        //获取canvas
        Transform canvas = GameObject.FindWithTag("MainCanvas").transform;
        shop = canvas.Find("Shop");
        //获取content
        content = shop.transform.Find("Image/Scroll View/Viewport/Content");
        //获取btn
        closeBtn = shop.transform.Find("closeBtn").GetComponent<Button>();
        //关闭按钮点击事件
        closeBtn.onClick.AddListener(() =>
        {
            //隐藏shop界面
            shop.gameObject.SetActive(false);
            //失活脚本
            enabled = false;
            //刷新界面
            Refesh();
        });
        //加载npc prop信息
        ResScroll();
    }
    /// <summary>
    ///  加载npc prop信息
    /// </summary>
    private void ResScroll()
    {
        
        //激活shop界面
        shop.gameObject.SetActive(true);
        //加载npc信息
        for (int i = 0; i < npcState.props.Count; i++)
        {
          //
            string imagePath = "图片/" + npcState.props[i];
            //加载图片
            Sprite tmp = Resources.Load<Sprite>(imagePath);
            //获取left 更换图片
            Image left = content.GetChild(i).transform.Find("left/left").GetComponent<Image>();
            //获取right
            Transform right = content.GetChild(i).transform.Find("right");

            //Debug.Log(right.name);
            //更改姓名
            right.GetChild(0).GetComponent<Text>().text = npcState.props[i].ToString();

            string price= (PropData.Props[npcState.props[i]].Price).ToString();
            //更改价格
            right.GetChild(1).GetComponent<Text>().text = price;
            //更改描述
            right.GetChild(2).GetComponent<Text>().text = PropData.Props[npcState.props[i]].Description;
            //更改图片
            left.GetComponent<Image>().sprite = tmp;
        }
        //Debug.Log(content.childCount - npcState.props.Count);

        //Debug.Log(npcState.props.Count);
        for (int i = npcState.props.Count+1; i < content.childCount; i++)
        {
            //Debug.Log(npcState.props.Count); 
            content.GetChild(i).gameObject.SetActive(false);
        }
    }
    private void Refesh()
    {
        for (int i = npcState.props.Count; i < content.childCount; i++)
        {
            //Debug.Log(npcState.props.Count);
            content.GetChild(i).gameObject.SetActive(true);
        }
    }
}
