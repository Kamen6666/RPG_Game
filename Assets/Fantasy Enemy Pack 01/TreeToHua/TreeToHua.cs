using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeToHua : MonoBehaviour
{
    private Transform flower3;
    private Transform tree;
    private ParticleSystem particle;
    private SphereCollider collider;
    private Transform duwu;
    private int aa = 0;
    private void Awake()
    {
        collider = GetComponent<SphereCollider>();
        flower3 = transform.Find("ThreeHua");
        tree = transform.Find("Tree");
        duwu = transform.Find("duwu");
        particle = transform.Find("duwu").GetComponent<ParticleSystem>();
    }
    private void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            aa++;
            if (aa++<=1)
            {
                StartCoroutine(ChangTree());
            }
            collider.enabled = false;


        }
    }
    IEnumerator ChangTree()
    {
        tree.gameObject.SetActive(false);
        duwu.gameObject.SetActive(true);
        particle.Play();
        yield return new WaitForSeconds(2);
        duwu.gameObject.SetActive(false);
        flower3.gameObject.SetActive(true);
    }
    
}
