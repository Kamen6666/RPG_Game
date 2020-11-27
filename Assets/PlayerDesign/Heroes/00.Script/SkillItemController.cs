using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class SkillItemController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image skillimage;
    private bool CanMove;
    void Start()
    {
        //获取用于移动的图片
        skillimage = transform.root.Find("Skill/SkillImage").GetComponent<Image>();
        skillimage.sprite = transform.Find("icon").GetComponent<Image>().sprite;
        //初始化移动开关
        CanMove = false;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        CanMove = !transform.Find("lock").gameObject.activeSelf;
        if (!CanMove)
            return;
        //刷新图片
        skillimage.sprite = transform.Find("icon").GetComponent<Image>().sprite;
        //设置位置
        skillimage.transform.position = transform.position;
        //设置名字
        skillimage.name = transform.Find("name").GetComponent<Text>().text;
        //显示图片
        skillimage.gameObject.SetActive(true);
        //跟随鼠标移动
        skillimage.transform.position = Input.mousePosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!CanMove)
            return;
        //跟随鼠标移动
        skillimage.transform.position = Input.mousePosition;    
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        skillimage.gameObject.SetActive(false);
        //Debug.Log(eventData.pointerEnter.transform.tag);
        if (eventData.pointerEnter && eventData.pointerEnter.transform.CompareTag("Shortcut"))
        {
            eventData.pointerEnter.GetComponent<ShortCutGrid>().GetSkillName(skillimage.name);
        }
    }
}
