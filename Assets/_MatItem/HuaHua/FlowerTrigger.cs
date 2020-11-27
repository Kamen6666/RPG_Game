using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == GameConst.PLAYER_TAG)
        {
            GameObject flower = GameObject.FindWithTag("FlowerSpawn").transform.Find("HuaPath(Clone)").gameObject;
            if (flower == null)
                throw new System.Exception("Flower==nil!!!1");
            flower.SetActive(true);

        }
    }
}
