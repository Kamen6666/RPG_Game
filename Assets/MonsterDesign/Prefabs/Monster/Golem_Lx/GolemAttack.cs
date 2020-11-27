using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public enum AttackType
{
    LeftPunchAttack,
    RightPunchAttack,
    ShockwaveAttack,
    DoublePunchAttack,
    SpinAttackOnce,
    SpinAttackLoop,
    CastSpell
}
public class GolemAttack : MonoBehaviour
{
    AttackType attackType;
    Transform playerTra;
    NavMeshAgent nav;
    Animator anim;
    Rigidbody rig;
    State state;
    MonsterJump monsterJump;
    GameObject Shield;
    float distance;
    bool isAttacking = false;
    bool isBuffed = false;
    bool isLowHp = false;
    bool isDefend = false;
    bool isPlayerDead = false;
    bool isResetBoss = false;
    bool isDead = false;
    private void Awake()
    {
        state = GetComponent<State>();
        playerTra = GameObject.FindWithTag("Player").transform;
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        rig = GetComponent<Rigidbody>();
        monsterJump = GetComponent<MonsterJump>();
        Shield = transform.Find("Shield").gameObject;
    }
    private void Update()
    {
        if (state.hP <= 0 && !isDead)
        {
            GameObject.FindWithTag("NpcFather").transform.Find("TP_NPC").gameObject.SetActive(true);
            isDead = true;
        }
        if (state.hP <= 0)
        {
            return;
        }
        Listen();

        if (!isResetBoss)
        {
            if (PlayerManager.instance.CrtHp <= 0)
            {
                StartCoroutine(ResetBoss());
            }
        }
        
        if (!isBuffed)
        {
            if (state.hP < state.initialHP * 0.5f)
            {
                state.AddAttackBuff();
                anim.SetTrigger("Cast Spell");
                state.ChangeSkinned();
                anim.speed = 1.5f;
                isBuffed = true;
            }
        }
        if (!isLowHp)
        {

            if (state.hP < state.initialHP * 0.2f)
            {
                isLowHp = true;
            }
        }

    }
    IEnumerator ResetBoss()
    {
        isResetBoss = true;
        anim.SetTrigger("Cast Spell");
        yield return new WaitForSeconds(1.5f);
        Destroy(this.gameObject);
        //transform.localPosition = Vector3.zero;
        //state.Refesh();
        //this.enabled = false;
    }
    private void Listen()
    {
        if (isAttacking)
        {
            return;
        }
        distance = Vector3.Distance(transform.position, playerTra.position);
        if (distance < state.attackDistance)
        {//玩家进入攻击距离  近距离
            IsNavStop(true);
            Attack();
        }
        else if (distance < state.followDistance && distance > state.attackDistance)
        {//玩家进入追击距离  中距离
            //打开导航
            //boss转圈攻击玩家
            IsNavStop(false);
            nav.SetDestination(playerTra.position);
            anim.SetBool("Spin Attack Loop", true);
        }
        else if (distance > state.followDistance && distance < state.jumpDistance)
        {//玩家脱离追击距离  远距离
            //boss跳向玩家
            monsterJump.enabled = true;
        }
    }
    private void IsNavStop(bool isStop)
    {
        nav.isStopped = isStop;
    }

    private void Attack()
    {//随机攻击
        isAttacking = true;
        attackType = (AttackType)UnityEngine.Random.Range(0, 5);
        anim.SetBool("Spin Attack Loop", false);
        switch (attackType)
        {
            case AttackType.LeftPunchAttack:
                anim.SetTrigger("Left Punch Attack");
                Defend();
                break;
            case AttackType.RightPunchAttack:
                anim.SetTrigger("Right Punch Attack");
                Defend();
                break;
            case AttackType.DoublePunchAttack:
                anim.SetTrigger("Double Punch Attack");
                Defend();
                break;
            case AttackType.ShockwaveAttack:
                anim.SetTrigger("Shockwave Attack");
                Defend();
                break;
            case AttackType.SpinAttackOnce:
                anim.SetTrigger("Spin Attack Once");
                Defend();
                break;
            default:
                Debug.LogError("攻击类型出错");
                break;
        }
    }

    private void Defend()
    {
        if (!isLowHp)
        {
            return;
        }
        if (!isDefend)
        {
            isDefend = true;
            state.isDefend = true;
            Shield.SetActive(true);
            anim.SetBool("Defend", true);
            Invoke("StopDefend", 3f);
        }

    }
    private void StopDefend()
    {
        anim.SetBool("Defend", false);
        isDefend = false;
        Shield.SetActive(false);
        state.isDefend = false;
    }

    //动画结束后  帧事件
    private void EndingAttack()
    {
        isAttacking = false;
    }
    public void AttackPlayer()
    {
        Debug.Log("distance" + distance);
        if (distance < state.attackDistance)
        {
            //玩家掉血
            PlayerManager.instance.FallBlood(state.attackPower);
        }
    }
    private void SpinAttack()
    {
        //击退敌人
        GameObject.FindWithTag(GameConst.PLAYER_TAG).GetComponent<PlayerController>().PlayerHurt(5f);
        AttackPlayer();
    }
}
