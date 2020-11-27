using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == GameConst.PLAYER_TAG)
        {
            PlayerManager.instance.CrtHp = -1;
        }
    }
}
