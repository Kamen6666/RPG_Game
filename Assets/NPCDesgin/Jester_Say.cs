using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jester_Say : MonoBehaviour
{
    private  Animator ani;
    private GameObject canvas;
    //滚
    private GameObject gun;
    //离我远点
    private GameObject awayme;
    private NPCCtrl npcCtrl;
    private Transform playerPos;
    public  int trigerCount = 0;
    private PlayerController playerCtrl;
    private void Awake()
    {
        playerPos = GameObject.FindWithTag("Player").transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        playerCtrl = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        ani = GetComponent<Animator>();
        npcCtrl = GetComponent<NPCCtrl>();
        canvas = transform.Find("Canvas").gameObject;
        gun = canvas.transform.Find("gun").gameObject;
        awayme = canvas.transform.Find("away").gameObject;

    }
    
    IEnumerator StopPlayerMove()
    {
        playerCtrl.enabled = false;
        yield return new WaitForSeconds(1.5f);
        playerCtrl.enabled = true;
    }

   
    private void Update()
    {
        float distance = Vector3.Distance(playerPos.position, transform.position);
        if (distance<=3)
        {
            transform.LookAt(playerPos);
            ani.SetBool("isDance", true);
            if (npcCtrl.isBuy)
            {
                ani.SetBool("isBuy", true);
                return;
            }
            trigerCount++;
            awayme.SetActive(true);
            if (trigerCount > 5)
            {
                awayme.SetActive(false);
                gun.SetActive(true);
            }
            transform.LookAt(playerPos.transform.position);
            Rigidbody rig = playerPos.GetComponent<Rigidbody>();
            rig.AddForce(transform.forward * rig.mass * 15, ForceMode.Impulse);
            StartCoroutine(StopPlayerMove());
        }
        else
        {
            ani.SetBool("isDance", false);
        }
        
    }
}
