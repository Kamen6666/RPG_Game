
using UnityEngine;
using UnityEngine.AI;

public class State : MonoBehaviour
{
    public MonsterData monsterData;
    //生命值
    public int hP;
    public int initialHP;
    public int G_hP;
    public int exp;
    public int G_exp;
    public int level;
    //怪物攻击力
    public int attackPower;
    public int attackBuff;
    public float speed;
    public float thinkTime;
    public int G_attackPower;

    public float backToHomeDistance;
    public float attackDistance;
    public float jingjueDistance;
    public float walkTime;
    public float xunLuoRange;

    public string monsterFallMat;
    public string monster_Ani_Attack;

    public string monster_Ani_Move;
    public string monster_FallMatValue;
    public int monster_FallgenerateCount;

    public float followDistance;
    public float jumpDistance;
    public bool isDefend;

    private Animator ani;
    private Collider col;
    private NavMeshAgent agent;
    private MonsterFollow follow;
    private MonsterThink think;
    private Rigidbody rig;
    private MonsterFallMat fallMat;
    private GameObject jinggao;
    public bool isCloseNav = false;
    private void Awake()
    {
        //MonsterManager.Instance.AddMonsterFromList(gameObject);
        ani = GetComponent<Animator>();
        col = GetComponent<Collider>();
        agent = GetComponent<NavMeshAgent>();
        follow = GetComponent<MonsterFollow>();
        think = GetComponent<MonsterThink>();
        fallMat = GetComponent<MonsterFallMat>();
        rig = GetComponent<Rigidbody>();
        
        Refesh();
        

    }
    private void Start()
    {

        
    }

    public void ResetLevel(int level)
    {
        hP +=G_hP * level;
        exp += G_exp * level;
        attackPower+= G_attackPower * level;

        initialHP = hP;
       
    }

    /// <summary>
    /// 怪物加buff
    /// </summary>
    public void AddAttackBuff()
    {
        attackPower += attackBuff;
    }
    public bool isDead = false;
    /// <summary>
    /// 怪物受伤
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {
        //Boss防御时的无敌
        if (isDefend)
        {
            return; 
        }
        hP -= damage;
        GameObject popupDamage = Instantiate(Resources.Load<GameObject>("PopupDamage"));
        popupDamage.GetComponent<PopupDamage>().Value = damage;
        popupDamage.transform.position = transform.position;
        if (hP <= 0)
        {
            isDefend = false;
            MonsterManager.Instance.RemoveMonsterFromList(gameObject);
            ani.SetBool(monster_Ani_Move, false);
            ani.SetTrigger("Die");
            col.enabled = false;
            rig.velocity = Vector3.zero;
            //增加经验
            PlayerManager.instance.UpLeve(exp);
            if (!isCloseNav)
            {
                agent.isStopped = true;
                if (follow != null )
                {
                    follow.enabled = false;
                }
                if (think != null)
                {
                    think.enabled = false;
                }

            }
            fallMat.enabled = true;

            //增加经验
            Destroy(gameObject, 3f);


        }
    }

    /// <summary>
    /// 初始化数据
    /// </summary>
    public void Refesh()
    {
        G_hP = monsterData.Monster_G_HP;
        G_exp = monsterData.Monster_G_Exp;
        G_attackPower = monsterData.Monster_G_Attack;
        hP = monsterData.MonsterHP;
        initialHP = hP;
        level = monsterData.MonsterLevel;
        exp = monsterData.MonsterExp;
        attackPower = monsterData.MonsterAttack;
        attackBuff = monsterData.MonsterAttackBuff;
        speed = monsterData.speed;
        backToHomeDistance = monsterData.followDistance;
        attackDistance = monsterData.attackDistance;
        xunLuoRange = monsterData.XunLuoRange;
        thinkTime = monsterData.thinkTime;
        walkTime = monsterData.walkTime;
        jingjueDistance = monsterData.jingjueDistance;
        followDistance = monsterData.followDistance;
        jumpDistance = monsterData.jumpDistance;
        monster_Ani_Move = monsterData.Monster_Ani_Move;
        monster_Ani_Attack = monsterData.Monster_Ani_Attack;
        monsterFallMat = monsterData.Monster_FallMat;
        monster_FallgenerateCount = monsterData.Monster_FallgenerateCount;

        monster_FallMatValue = monsterData.Monster_FallMatValue;
        if (!isCloseNav)
        {
            agent.speed = speed;
        }

    }
    private void YoYo_AttackEvent()
    {
        PlayerManager.instance.FallBlood(attackPower);
    }
    public void ChangeSkinned()
    {
        SkinnedMeshRenderer skinned = transform.Find("Toon Rock Golem").GetComponent<SkinnedMeshRenderer>();
        Texture texture = Resources.Load<Texture>("Toon Rock Golem-Red");
        skinned.materials[0].mainTexture = texture;
    }
}
