using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeBoss : MonoBehaviour
{
    private State state;
    private FlowerManager flowerManager;
    private Transform duwuPos;
    private Animator ani;
    private float timeCount = 8;
    private Transform playerPos;
    public int duwuDistance;
    public int attackDistance;
    private void Awake()
    {
        ani = GetComponent<Animator>();
        duwuPos = transform.Find("DuWuPos");
        state = GetComponent<State>();
        playerPos = GameObject.FindWithTag("Player").transform;
        state.isCloseNav = true;
       
    }
    private void Start()
    {
        flowerManager = transform.parent.parent.GetComponent<FlowerManager>();
        if (flowerManager == null)
            throw new System.Exception("flowerManager==nil");
        
    }
    private bool isRemove = false;
    private void Update()
    {
        if (state.hP <= 0 && !isRemove)
        {
            flowerManager.RemoveAliveList(gameObject);
            Debug.Log("Has Removed!!!");
            isRemove = true;
        }
        if (state.hP <= 0)
            return;
        float distance = Vector3.Distance(playerPos.position, transform.position);
        if (attackDistance<=distance&&distance<=duwuDistance)
        {
            DuWuAttack();
        }
        else if (distance <= attackDistance)
        {
            BiteAttack();
        }

       
      
    }
    /// <summary>
    /// 近距离攻击
    /// </summary>
    private void BiteAttack()
    {
        //particle.Stop();
        transform.LookAt(playerPos);
        ani.SetTrigger("Bite Attack");
    }
    private void DuWuAttack()
    {
       
        //particle.Stop();
        ani.SetBool("Breath Attack", false);
        timeCount += Time.deltaTime;
        if (timeCount >= 10)
        {
            DuWu();
            timeCount = 0;
        }
    }
    private void DuWu()
    {
        ani.SetTrigger("Cast Spell");
        GameObject duwu = Instantiate(Resources.Load<GameObject>("FakeDuWu"));
        if (duwu == null)
            throw new System.Exception("duwu==nil");
        duwu.transform.position = duwuPos.position;
    }


}
