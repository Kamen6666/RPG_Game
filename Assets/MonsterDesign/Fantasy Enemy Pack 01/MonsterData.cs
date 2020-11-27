using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="New Monter",menuName="Monster/New Monster")]
public class MonsterData: ScriptableObject
{
    [Header("怪物名称")]
    public string MosterName;
    [Header("怪物等级")]
    public int MonsterLevel;
    [Header("怪物生命值")]
    public int MonsterHP;
    [Header("怪物成长生命值")]
    public int Monster_G_HP;
    [Header("怪物经验")]
    public int MonsterExp;
    [Header("怪物成长经验")]
    public int Monster_G_Exp;
    [Header("怪物物攻")]
    public int MonsterAttack;
    [Header("怪物成长物攻")]
    public int Monster_G_Attack;
    [Header("怪物攻击buff")]
    public int MonsterAttackBuff;


    [Header("巡逻范围")]
    public float XunLuoRange;
    [Header("巡逻思考的时间")]
    public float thinkTime;
    [Header("巡逻走的时间")]
    public float walkTime;
    [Header("导航速度")]
    public float speed;
    [Header("跟随距离")]
    public float followDistance;
    [Header("攻击距离")]
    public float attackDistance;
    [Header("警觉距离")]
    public float jingjueDistance;
    [Header("Boss跳跃距离")]
    public float jumpDistance;



    [Header("怪物移动Ani名字")]
    public string Monster_Ani_Move;

    [Header("怪物攻击Ani名字")]
    public string Monster_Ani_Attack;
    [Header("怪物掉落材料")]
    public string Monster_FallMat;
    [Header("怪物材料爆率")]
    public string Monster_FallMatValue;

    [Header("怪物材料生成调用几次")]
    public int Monster_FallgenerateCount;



}
