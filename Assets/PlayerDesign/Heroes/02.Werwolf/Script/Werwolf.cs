using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Werwolf : MonoBehaviour
{
    //蓄力粒子
    public ParticleSystem PowerParticle;
    //普通攻击效果
    private void NormalAttackEffect()
    {
        //蓄力向前攻击
        //需求：蓄力粒子 蓄力声音（可选）
        //启动粒子
        PowerParticle.Play();
        //播放声音
    }
    //单体技能效果
    private void SOLOAttackEffect()
    {
        //地裂
    }
    //群攻技能效果
    private void ALLAttackEffect()
    {
        //旋风斩 （吸怪效果）
    }
    //变成龙
    private void DragonShape()
    {
        //魔法阵
        //遮挡玩家视线
        //替换预制体
    }
}
