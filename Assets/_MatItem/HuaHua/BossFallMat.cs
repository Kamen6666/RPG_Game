using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFallMat : MonoBehaviour
{
    private GameObject baowu;
    private MeshRenderer mesh;
    private BoxCollider box;
    private void Start()
    {
        baowu = transform.Find("baowu").gameObject;
        box = GetComponent<BoxCollider>();
        mesh = GetComponent<MeshRenderer>();
    }
    private bool isShow = false;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player")&&!isShow)
        {
          
            box.enabled = false;
            mesh.enabled = false;
            baowu.SetActive(true);
            isShow = true;
        }
    }

}
