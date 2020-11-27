
using UnityEngine;
using UnityEngine.AI;

public class MonsterFollow : MonoBehaviour
{
    private SkinnedMeshRenderer skinned;
    private Transform playerPos;
    private NavMeshAgent agent;
    private Animator ani;
    private float distance;
    private bool isFollow = false;
    private CanvasGroup jinggaoGroup;
    private float shake;
   
    private MonsterThink monsterThink;
    //
    private State state;

    private void Awake()
    {
        state = GetComponent<State>();
        monsterThink = GetComponent<MonsterThink>();
        skinned = GetComponentInChildren<SkinnedMeshRenderer>();
        playerPos = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        ani = GetComponent<Animator>();

    }
    private void Start()
    {
        //jinggaoGroup = transform.Find("Canvas/Panel/Image").GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        if (PlayerManager.instance.CrtHp<=0)
        {
            return;
        }
        FollowAndAttackPlayer();
    }

    /// <summary>
    /// 怪物跟随攻击玩家
    /// </summary>
    public   void FollowAndAttackPlayer()
    {
        distance = Vector3.Distance(transform.position, monsterThink.startPos);
        //警觉距离
        //怪物跟随
        if (distance <=state.backToHomeDistance)
        {
            distance = Vector3.Distance(transform.position, playerPos.position);
            //JingGao();
            agent.SetDestination(playerPos.position);
            ani.SetBool(state.monster_Ani_Move, true);
            IsNavStop(false);
            //怪物攻击
            if (distance <= state.attackDistance)
            {
                Monster_BiteAttack();
            }
            else
            {
                IsNavStop(false);
            }
        }
        else
        {
            //处于静止状态
            ani.SetBool(state.monster_Ani_Move, false);
            IsNavStop(true);
            this.enabled = false;
            monsterThink.enabled = true;
        }
    }

    /// <summary>
    /// 怪物头上警告
    /// </summary>
    private void JingGao()
    {
        
        if (distance < state.jingjueDistance)
        {
            JingGaoShanSuo();
        }
    }

    /// <summary>
    /// 警告闪烁
    /// </summary>
    private void JingGaoShanSuo()
    {

        shake += Time.deltaTime;
        if (shake > 3.05)
            return;
        if (shake % 1 > 0.5f)
        {
            jinggaoGroup.alpha = 1;
        }
        else
        {
            jinggaoGroup.alpha = 0;
        }
    }

    /// <summary>
    /// 怪物攻击
    /// </summary>
    private  void Monster_BiteAttack()
    {
        IsNavStop(true);

        ani.SetBool(state.monster_Ani_Move, false);
        ani.SetTrigger(state.monster_Ani_Attack);
    }
    /// <summary>
    /// 帧事件怪物攻击
    /// </summary>
    private void YoYo_AttackEvent()
    {
        PlayerManager.instance.FallBlood(state.attackPower);
    }
    /// <summary>
    /// 停止导航
    /// </summary>
    /// <param name="isStop"></param>
    private void IsNavStop(bool isStop)
    {
        agent.isStopped = isStop;
    }
    private  bool isBuff = false;

    /// <summary>
    /// 怪物一技能
    /// </summary>
    private  void Monster_CastSpell()
    {
        if (!isBuff)
        {
            state.AddAttackBuff();
            ani.SetTrigger("Cast Spell");

        }
        isBuff = true;
    }

}
