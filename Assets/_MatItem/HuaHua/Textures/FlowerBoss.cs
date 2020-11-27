using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerBoss : MonoBehaviour
{
    private Animator ani;
    public  State state;
    private Transform playerPos;
    private Transform duwuPos;
    private ParticleSystem particle;
    private GameObject breathAttack;
    public int duwuDistance;
    public int attackDistance;
    public int spellDistance;

    private GameObject resEnemy;
    private float timeCount=0;

    private void Awake()
    {
        ani = GetComponent<Animator>();
        state = GetComponent<State>();
        playerPos = GameObject.FindWithTag("Player").transform;
        breathAttack = transform.Find("BreathAttack").gameObject;
        duwuPos = transform.Find("DuWuPos");
        resEnemy = transform.parent.Find("ResEnemy").gameObject;
        state.isCloseNav = true;
    }
    private  float timecount = 0;

    private bool isRes = false;
    private void Update()
    {

        if (PlayerManager.instance.CrtHp <= 0)
        {
            return;
        }

        if (state.hP<=0&& !isRes)
        {
            GameObject baowu = Resources.Load<GameObject>("baoxiang");
            Instantiate(baowu,new Vector3(-236.9f,8f,165f),transform.rotation);
            GameObject.FindWithTag("NpcFather").transform.Find("TP_NPC").gameObject.SetActive(true);
            isRes = true;
            Destroy(GameObject.FindWithTag("FlowerSpawn").transform.Find("HuaPath(Clone)").gameObject, 5);
           
        }

       
        //Debug.Log(PlayerManager.instance.CrtHp + "|" + isRes);

        float distance = Vector3.Distance(playerPos.position, transform.position);

        timecount += Time.deltaTime;
        //Debug.Log(distance);
        if (distance> duwuDistance)
        {
            return;
        }
        if (spellDistance<=distance&&distance<=duwuDistance)
        {
            DuWuAttack();
        }
        else if (attackDistance <= distance && distance <= spellDistance)
        {
            BreathAttack();
            bool isAttack=UmbrellaAttact(transform, playerPos, 90, 10);
            if (isAttack)
            {
                if (timecount >= 2)
                {
                    PlayerManager.instance.CrtHp -= 8;
                    timecount = 0;
                }
               
            }
            //Debug.Log(isAttack);
        }
        else if (distance <= attackDistance)
        {
            BiteAttack();
        }
        if (state.initialHP * 0.4f >= state.hP)
        {
            resEnemy.SetActive(true);
        }
        if (FlowerManager.Instance.aliveFake.Count > 0)
        {
            gameObject.tag = "Untagged";
        }
        else
        {
            gameObject.tag = "Enemy";
        }

    }

    

    private void DuWuAttack()
    {
        breathAttack.SetActive(false);
        //particle.Stop();
        ani.SetBool("Breath Attack", false);
        timeCount += Time.deltaTime;
        if (timeCount >= 5)
        {
            DuWu();
            timeCount = 0;
        }
    }
    /// <summary>
    /// 近距离喷毒攻击
    /// </summary>
    private void BreathAttack()
    {
        //particle.Play();
        breathAttack.SetActive(true);
        transform.LookAt(playerPos);
        ani.SetBool("Breath Attack",true);
       
    }


    /// <summary>
    /// 近距离攻击
    /// </summary>
    private void BiteAttack()
    {
        breathAttack.SetActive(false);
        //particle.Stop();
        ani.SetBool("Breath Attack", false);
        transform.LookAt(playerPos);
        ani.SetTrigger("Bite Attack");
    }


    /// <summary>
    /// 毒雾攻击
    /// </summary>
    private void DuWu()
    {
        ani.SetTrigger("Cast Spell");
        GameObject duwu = Instantiate(Resources.Load<GameObject>("DuWu"));
        if (duwu == null)
            throw new System.Exception("duwu==nil");
        duwu.transform.position = duwuPos.position;
    }

    /// <summary>
    /// 伞形攻击范围
    /// </summary>
    /// <param name="attacker">攻击方</param>
    /// <param name="attacked">被攻击方</param>
    /// <param name="angle">伞形角度</param>
    /// <param name="radius">伞形半径</param>
    /// <returns></returns>
    public bool UmbrellaAttact(Transform attacker, Transform attacked, float angle, float radius)
    {

        Vector3 deltaA = attacked.position - attacker.position;

        float tmpAngle = Mathf.Acos(Vector3.Dot(deltaA.normalized, attacker.forward)) * Mathf.Rad2Deg;

        if (tmpAngle < angle * 0.5f && deltaA.magnitude < radius)
        {
            return true;
        }
        return false;
    }
}
