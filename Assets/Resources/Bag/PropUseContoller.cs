using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PropUseContoller : MonoBehaviour, IPointerDownHandler
{
    private ViewMananger vm;
    private Transform player;
    void Start()
    {
        //获取管理组件
        vm = GameObject.FindGameObjectWithTag("GameController").GetComponent<ViewMananger>();
        //获取当前激活的玩家
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //当有道具的道具格被右键时
        if (Input.GetMouseButtonDown(1) && transform.GetChild(0).gameObject.activeSelf)
        {
            //获取当前道具格的道具标签
            PropItems_Enum pie = vm.GetPropItem(transform.GetSiblingIndex(), ItemType.Consumable).ID;
            if (pie == PropItems_Enum.空)
                return;
            //吃药
            UseItem(pie);
        }
    }
    private void UseItem(PropItems_Enum propItems_Enum)
    {
        switch (propItems_Enum)
        {
            case PropItems_Enum.原素瓶:
                //获取当前格子物体数量数量减一
                vm.UsePropItem(transform.GetSiblingIndex());
                //回血
                PlayerManager.instance.CrtHp += 50;
                break;
            case PropItems_Enum.女神的祝福:
                //获取当前格子物体数量数量减一
                vm.UsePropItem(transform.GetSiblingIndex());
                //回满血
                PlayerManager.instance.CrtHp = PlayerManager.instance.maxHp;
                break;
            case PropItems_Enum.原素灰瓶:
                //获取当前格子物体数量数量减一
                vm.UsePropItem(transform.GetSiblingIndex());
                //回蓝
                PlayerManager.instance.CrtMp += 50;
                break;
            case PropItems_Enum.秘藏的祝福:
                //获取当前格子物体数量数量减一
                vm.UsePropItem(transform.GetSiblingIndex());
                //回满蓝
                PlayerManager.instance.CrtMp = PlayerManager.instance.maxMp;
                break;
            case PropItems_Enum.返回骨片:
                //获取当前格子物体数量数量减一
                vm.UsePropItem(transform.GetSiblingIndex());
                //回城
                player.transform.position = new Vector3(180f, 0.5f, 190f);
                break;
            case PropItems_Enum.苔藓球果实:
                if (!GameObject.FindGameObjectWithTag("Pet"))
                {
                    //获取当前格子物体数量数量减一
                    vm.UsePropItem(transform.GetSiblingIndex());
                    Instantiate(Resources.Load("Pet/Dragon-Green") as GameObject, player.position, player.rotation);
                }
                break;
            case PropItems_Enum.红虫药丸:
                if (!GameObject.FindGameObjectWithTag("Pet"))
                {
                    //获取当前格子物体数量数量减一
                    vm.UsePropItem(transform.GetSiblingIndex());
                    Instantiate(Resources.Load("Pet/Dragon-Red") as GameObject, player.position, player.rotation);
                }
                break;
            case PropItems_Enum.诱敌头盖骨:
                if (!GameObject.FindGameObjectWithTag("Pet"))
                {
                    //获取当前格子物体数量数量减一
                    vm.UsePropItem(transform.GetSiblingIndex());
                    Instantiate(Resources.Load("Pet/Dragon-Purple") as GameObject, player.position, player.rotation);
                }
                break;
            case PropItems_Enum.锈斑硬币:
                //获取当前格子物体数量数量减一
                vm.UsePropItem(transform.GetSiblingIndex());
                PlayerManager.instance.UpLeve(50);
                break;
            case PropItems_Enum.锈斑金币:
                //获取当前格子物体数量数量减一
                vm.UsePropItem(transform.GetSiblingIndex());
                PlayerManager.instance.UpLeve(200);
                break;
            default:
                break;
        }
    }

}
