using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_TiShi : MonoBehaviour
{
    private Transform tishi;
    // Start is called before the first frame update
    private void Awake()
    {
       
        tishi = GameObject.FindWithTag("MainCanvas").transform.Find("TiShi");
        if (tishi == null)
            throw new System.Exception("tishi==nil");
    }
    private void Start()
    {
      
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            tishi.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            tishi.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            tishi.gameObject.SetActive(false);
        }
    }

}
