using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : MonoBehaviour
{
    //弓箭预制体
    public GameObject Arrow;
    //普通攻击效果
    private void NormalAttackEffect()
    {
        //向前方射出一只箭
        //需求：箭预制体 开火点 箭飞行速度 箭存在时间
        //获得开火点 的Transfrom
        Transform firepoint = transform.Find("FirePoint");
        //获取箭的刚体
        Rigidbody rd = Instantiate(Arrow,firepoint.position,firepoint.rotation).GetComponent<Rigidbody>();
        //调整箭头方向
        rd.transform.eulerAngles += new Vector3(-90, 0, 0);
        //给箭一个速度
        rd.velocity = firepoint.forward * 20f;
        //箭的存活时间
        Destroy(rd.gameObject, 1f);
    }
    //单体技能效果
    private void SOLOAttackEffect()
    {
        //连射
    }
    //群攻技能效果
    private void ALLAttackEffect()
    {
        //前方第一个怪物为中心 释放箭雨
    }
    //变成龙
    private void DragonShape()
    {
        //魔法阵
        //遮挡玩家视线
        //替换预制体
    }
}
