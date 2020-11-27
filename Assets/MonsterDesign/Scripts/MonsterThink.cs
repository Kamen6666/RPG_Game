using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterThink : MonoBehaviour
{
    private  float currentWalkTime = 0;//记录走的时间
    private float currentThinkTime = 0f;//记录思考的时间
    private NavMeshAgent agent;
    private Animator ani;
    private State state;
    public Vector3 startPos;
    private float distance;
    private Transform playerPos;
    //private CanvasGroup jinggaoGroup;
    private bool isBackHome;
    private MonsterFollow monsterFollow;


    private void Awake()
    {
        startPos = transform.position;
        monsterFollow = GetComponent<MonsterFollow>();
        ani = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        state = GetComponent<State>();
        
        playerPos = GameObject.FindWithTag("Player").transform;
        //jinggaoGroup = transform.Find("Canvas/Panel/Image").GetComponent<CanvasGroup>();
    }
 
    private void Update()
    {
        MonsterThinks();
        
    }
    private void OnEnable()
    {
        agent.isStopped = false;
        
    }
    /// <summary>
    /// 怪物思考巡逻  回家
    /// </summary>
    private void MonsterThinks()
    {
        ani.SetBool(state.monster_Ani_Move, true);
        /// Debug.Log("xxxxxx");
        isBackHome = Vector3.Distance(transform.position, startPos) > 8f ? true : false;

        if (Vector3.Distance(transform.position, startPos) >= state.backToHomeDistance)
        {
            agent.SetDestination(startPos);
        }
        else
        {

            currentWalkTime += Time.deltaTime;//开始记录走的时间
            if (currentWalkTime >= state.walkTime)
            {//当走的时间大于设定的时间时


                currentThinkTime += Time.deltaTime;//记录思考的时间
                if (currentThinkTime >= state.thinkTime)
                {//当思考时间大于开始设定的时间时

                    currentWalkTime = 0f;
                    currentThinkTime = 0f;

                    float x = Random.Range(-state.xunLuoRange, state.xunLuoRange);
                    float y = Random.Range(-state.xunLuoRange, state.xunLuoRange);

                    agent.SetDestination(startPos + new Vector3(x, 0, y));
                }

            }

        }

        distance = Vector3.Distance(transform.position, playerPos.position);

        //Debug.Log(distance + "|" + state.jingjueDistance + "|" + isBackHome + "|" + startPos +"|"+transform.position);
        if (distance < state.jingjueDistance && !isBackHome)
        {
          
            enabled = false;
            monsterFollow.enabled = true;

        }
       
           
    }
    
}
