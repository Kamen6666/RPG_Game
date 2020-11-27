using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultEffectController : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.transform.CompareTag("Enemy"))
        {
            other.transform.position = transform.position;
        }
    }

}
