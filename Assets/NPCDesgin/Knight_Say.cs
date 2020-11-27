using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight_Say : MonoBehaviour
{
    private GameObject canvas;
    private NPCCtrl npcCtrl; 
  
    // Start is called before the first frame update
    void Start()
    {
        npcCtrl = GetComponent<NPCCtrl>();
        canvas = transform.Find("Canvas").gameObject;
    }
    // Update is called once per frame
    private bool isClik = false;
    void Update()
    {
        if (npcCtrl.distance<=10)
        {
            canvas.SetActive(true);
            if (npcCtrl.distance <=4 && !isClik)
            {
                if (Input.GetKeyDown(KeyCode.C))
                {
                    isClik = true;
                }
            }
        }
        else
        {
            isClik = false;
            canvas.SetActive(false);
        }
    }
}
