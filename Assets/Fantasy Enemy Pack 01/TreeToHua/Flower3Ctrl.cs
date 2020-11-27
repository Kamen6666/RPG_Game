using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower3Ctrl : MonoBehaviour
{
    private Transform playerPos;
    private Animator ani;
    private State state;
    private void Awake()
    {
        state = GetComponent<State>();
        playerPos = GameObject.FindWithTag("Player").transform;
        ani = GetComponent<Animator>();
        state.isCloseNav =true;
    }
    private void Start()
    {
        
    }
    private void Update()
    {
        if (state.hP<=0)
        {
            transform.LookAt(null);
            return;
        }
        float distance = Vector3.Distance(playerPos.position, transform.position);
        if (distance <= state.attackDistance)
        {
            transform.LookAt(playerPos);
            ani.SetTrigger("Attack");
        }
    }


}
