using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public int atk;
    public int num;
    void Awake()
    {
        atk = 10;
    }
    void Start()
    {
        if (num < 1)
            num = 1;
        Physics.IgnoreCollision(GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider>(), transform.GetComponent<SphereCollider>(), true);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Enemy")&&num>0)
        {
            other.transform.GetComponent<State>().TakeDamage(atk);
            num--;
            if(num<1)
                Destroy(gameObject);
        }
    }
}
