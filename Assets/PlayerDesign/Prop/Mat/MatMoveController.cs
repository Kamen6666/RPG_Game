using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatMoveController : MonoBehaviour
{
    //金币移动的的目标
    GameObject target;
    //金币是否可以移动
    public bool isCanMove = false;

    
    private MatToPlayer mattoplayer;
    //金币移动的速度
    public float speed = 50;

    ViewMananger viewMananger;
    private void Awake()
    {

        mattoplayer = GameObject.FindWithTag("Player").GetComponent<MatToPlayer>();
        viewMananger = GameObject.FindWithTag("GameController").GetComponent<ViewMananger>();
    }
    void Start()
    {
        //获取玩家
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (isCanMove)
        {
            //金币向玩家移动的速度
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * speed);
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(transform.name);
        //string强转枚举
        string name = transform.name;
        
        PropItems_Enum propItems_Enum = (PropItems_Enum)Enum.Parse(typeof(PropItems_Enum), name.Replace("(Clone)",""));
          //获取prop 枚举
        ItemType itemType = PropData.Props[propItems_Enum].ItemType;

        int num = 1;

      

        //如果金币碰到的物体是玩家
        if (collision.transform.tag=="Player")
        {
            bool isEmpty = viewMananger.HaveEmpty(propItems_Enum, itemType, num);
            if (isEmpty)
            {
                mattoplayer.enabled = true;
                //如果有空 添加进去
                viewMananger.GetNewMateria(propItems_Enum, itemType, num);
                //金币碰到玩家后就会销毁金币
                Destroy(gameObject);
            }
            else
            {
                mattoplayer.enabled = false;
                Debug.Log("背包满了！！！！");
            }
           
        }
    }

   
}
