using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class knight : Magician
{
    public Slider Normalskill;
    private float NormalTimer;
    private Animator ani;
    private PlayerController pc;
    private bool isAttack;
    private Rigidbody rigid;
    void Awake()
    {
        NormalTimer = Time.time;
        ani = GetComponent<Animator>();
        pc = GetComponent<PlayerController>();
        rigid = GetComponent<Rigidbody>();
    }
    void Update()
    {
        //普通攻击CD
        float ff = 1 - ((Time.time - NormalTimer) / 1f);

        if (!isAttack)
        {
            //释放普通攻击
            if (Input.GetMouseButtonDown(0) && ff < 0.01f)
            {
                StartCoroutine(NormalAttack());
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(SOLOAttack());
            }
        }
    }
    //普通攻击效果
    private void NormalAttackEffect()
    {
        //播放攻击动画
        ani.SetTrigger(GameConst.PLAYER_ANIMATOR_PARA_ATTACK);
    }
    IEnumerator NormalAttack()
    {
        //进入攻击状态
        isAttack = true;
        //计时器进入计时
        NormalTimer = Time.time;
        //普通攻击
        NormalAttackEffect();
        yield return new WaitForSeconds(0.2f);
        //造成伤害
        MonsterManager.Instance.ReduceMonsterHP(transform, 150f, 3f, 3, 10);
        //离开攻击状态
        isAttack = false;
    }
    //单体技能效果
    private void SOLOAttackEffect()
    {
        //冲锋
    }
    IEnumerator SOLOAttack()
    {
        //进入攻击状态
        isAttack = true;
        //关闭移动
        pc.enabled = false;
        //计时器进入计时
        NormalTimer = Time.time;
        //延迟2s
        yield return new WaitForSeconds(0.2f);
        /*
        while (Time.time - NormalTimer < 0.5f)
        {
            //transform.position += transform.forward;
            yield return new WaitForSeconds(0.02f);
        }
        */
        rigid.velocity = transform.forward * 20;
        yield return new WaitForSeconds(0.4f);
        rigid.velocity = Vector3.zero;
        //停止协成
        StopCoroutine(NormalAttack());
        //离开攻击状态
        isAttack = false;
        //开启移动
        pc.enabled = true;

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
