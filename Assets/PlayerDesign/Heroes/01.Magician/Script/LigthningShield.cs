using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LigthningShield : MonoBehaviour
{
    //攻击效果
    public GameObject AttackEffect;
    //攻击间隔
    public float AttackCd;
    //攻击计时器
    private float Attack_time;
    //范围内怪物列表
    private List<Transform> monsters;
    //伤害
    public int AtkPower;
    void Start()
    {
        //清空计时器
        Attack_time = 0f;
        //初始化怪物列表
        monsters = new List<Transform>();
    }

    void FixedUpdate()
    {
        //开始计时
        Attack_time += Time.deltaTime;
        //开始攻击
        if (Attack_time > AttackCd && monsters.Count > 0)
        {
            //计时器清空
            Attack_time = 0f;
            //随机目标
            int RRR = Random.Range(0, monsters.Count);
            //在目标身上创建受伤特效
            Instantiate(AttackEffect, monsters[RRR]).transform.localPosition = Vector3.zero;
            //如果怪物已经死亡或者不存在 则从列表删除
            if (monsters[RRR].GetComponent<State>().hP <= 0 || !monsters[RRR].gameObject)
            {
                monsters.Remove(monsters[RRR]);
                return;
            }
            //造成伤害
            monsters[RRR].GetComponent<State>().TakeDamage(AtkPower);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        //检测到怪物时加入列表
        if (other.CompareTag("Enemy"))
        {
            monsters.Add(other.transform);
        }
    }
    void OnTriggerExit(Collider other)
    {
        //怪物离开事删除列表
        if (other.CompareTag("Enemy"))
        {
            monsters.Remove(other.transform);
        }
    }
}
