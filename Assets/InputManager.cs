using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Analytics;

public class InputManager : MonoBehaviour
{
    Toggle[] toggle;
    private void Awake()
    {
       toggle = GameObject.FindWithTag("MainCanvas").transform.Find("UI/Function").GetComponentsInChildren<Toggle>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            toggle[0].isOn = !toggle[0].isOn;
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            toggle[1].isOn = !toggle[1].isOn;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            toggle[2].isOn = !toggle[2].isOn;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            for (int i = 0; i < toggle.Length; i++)
            {
                toggle[i].isOn = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.F1))
        {
            GhostFireBallController gfbc = GameObject.FindGameObjectWithTag("GhostFire").GetComponent<GhostFireBallController>();
            gfbc.enabled = false;
            gfbc.enabled = true;
        }
    }
}
