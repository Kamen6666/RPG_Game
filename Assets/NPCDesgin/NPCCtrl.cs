using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCCtrl : MonoBehaviour
{
    private Transform playerPos;
    private NpcLoadData loadData;
    public bool isBuy = false;
  
    private void Awake()
    {
        
        loadData = GetComponent<NpcLoadData>();
        playerPos = GameObject.FindWithTag("Player").transform;
    }
    private void Start()
    {
        
    }
    public  float distance;
    void Update()
    {
        distance = Vector3.Distance(playerPos.position, transform.position);
        if (distance <= 5)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                isBuy = true;
                loadData.enabled = true;
            }
        }
        else
        {
            
            loadData.enabled = false;
        }
    }
}
