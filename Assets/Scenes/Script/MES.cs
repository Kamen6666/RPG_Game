using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MES : MonoBehaviour
{
    public Animator ani;
    public Transform c_transform;
    Vector3 dis;
    void Awake()
    {
        ani = transform.GetChild(1).GetComponent<Animator>();
        dis = c_transform.position - ani.transform.position;
    }
    void OnEnable()
    {
        ani.SetTrigger("Jump");
        ani.transform.position += ani.transform.forward * 2f;
        ani.SetTrigger("Victory");
        Destroy(gameObject, 3f);

    }
    void Update()
    {
        c_transform.position = ani.transform.position + dis;
    }
    
    void OnDisable()
    {
        GameObject.FindGameObjectWithTag("GameManager").gameObject.SetActive(true);
    }
}
