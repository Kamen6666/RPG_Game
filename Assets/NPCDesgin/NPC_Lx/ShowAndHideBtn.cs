using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowAndHideBtn : MonoBehaviour
{
    [SerializeField]
    GameObject btn;
    [SerializeField]
    GameObject flowChart;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == GameConst.PLAYER_TAG)
        {
            btn.SetActive(true);
            flowChart.SetActive(true);
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == GameConst.PLAYER_TAG)
        {
            btn.SetActive(false);
            flowChart.SetActive(false);
        }
    }
}
