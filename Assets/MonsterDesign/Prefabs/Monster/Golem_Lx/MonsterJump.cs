using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterJump : MonoBehaviour
{
    GameObject upTarget;
    GameObject Player;
    Vector3 upTargetPos;
    Vector3 TargetPos;
    NavMeshAgent nav;
    BoxCollider boxCollider;
    GolemAttack golemAttack;
    public const float g = 9.8f;
    public float speed = 10;
    public float hight = 2;
    [Range(0.1f,0.9f)][Tooltip("越大越快")]
    public float fallSpeed = 0.3f;
    float verticalSpeed;
    float time;
    bool isArrivedUp = false;
    bool isArrived = false;
    bool jump = false;
    bool isInit;

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        boxCollider = GetComponent<BoxCollider>();
        golemAttack = GetComponent<GolemAttack>();
        Player = GameObject.FindWithTag("Player");
        upTarget = Player.transform.Find("upTarget").gameObject;
    }
    private void OnEnable()
    {
        nav.enabled = false;
        boxCollider.enabled = false;
        golemAttack.enabled = false;
        jump = true;
    }
    private void OnDisable()
    {
        nav.enabled = true;
        boxCollider.enabled = true;
        golemAttack.enabled = true;
        jump = false;
        isInit = false;
        isArrivedUp = false;
        isArrived = false;
        verticalSpeed = 0f;
        time = 0f;
    }
    void Update()
    {
        if (jump)
        {
            Jump();
        }

    }

    private void Jump()
    {
       
        if (!isInit)
        {//初始化  算一些跳跃参数
            float tmepDistance = Vector3.Distance(transform.position, upTarget.transform.position);
            float tempTime = tmepDistance / speed;
            float riseTime, downTime;
            riseTime = downTime = tempTime / 2;
            verticalSpeed = g * riseTime;
            transform.LookAt(upTarget.transform);
            upTargetPos = upTarget.transform.position;
            TargetPos = Player.transform.position;
            isInit = true;
        }
        if (!isArrivedUp)
        {
            //跳向玩家头顶
            if (Vector3.Distance(transform.position, upTargetPos) < 0.5f)
            {
                isArrivedUp = true;
            }
            time += Time.deltaTime;
            float test = verticalSpeed - g * time;
            transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
            transform.Translate(transform.up * test * Time.deltaTime, Space.World);
        }
        else
        {   //修正欧拉角
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z);
            //下砸
            if (!isArrived)
            {
                transform.position = Vector3.Lerp(transform.position, TargetPos,fallSpeed);
                if (Vector3.Distance(transform.position, TargetPos) < 0.05f)
                {
                    isArrived = true;
                    jump = false;
                    this.enabled = false;
                    //打开落地烟雾特效TODO
                    //镜头震动
                    Player.GetComponent<PlayerController>().ShakeCamera();
                    golemAttack.AttackPlayer();
                }
            }
        }

    }
}
