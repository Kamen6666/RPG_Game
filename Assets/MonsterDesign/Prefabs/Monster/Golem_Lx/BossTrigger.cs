using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    DeliveryCondition deliveryCondition;
    GameObject boss;
    GameObject TriggerCamera;
    private void Awake()
    {
       
        TriggerCamera = transform.Find("Camera").gameObject;
        deliveryCondition = TriggerCamera.GetComponent<DeliveryCondition>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == GameConst.PLAYER_TAG)
        {
            boss = GameObject.FindWithTag("GolemBossSpawn").transform.GetChild(0).gameObject;
            TriggerCamera.SetActive(true);
        }
    }
    private void Update()
    {
        if (deliveryCondition.isArrived)
        {
            boss.GetComponent<Animator>().SetTrigger("Cast Spell");
            deliveryCondition.isArrived = false;
            StartCoroutine(ShutDownCamera());
        }


       
    }
    IEnumerator ShutDownCamera()
    {
        yield return new WaitForSeconds(1f);
        TriggerCamera.SetActive(false);
        boss.GetComponent<GolemAttack>().enabled = true;
    }
}
