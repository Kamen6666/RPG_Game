using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostFireBallController : MonoBehaviour
{
    //鬼火球预制体
    public GameObject GhostFireBallPrefab;
    //鬼火球列表
    private Queue<GameObject> GhostFireBalls;
    void Awake()
    {
        GhostFireBalls = new Queue<GameObject>();
    }

    void OnEnable()
    {
        AttackTimeCd = 0.5f;
        AttackTime = 0f;
        StartCoroutine(CreateGhostFire(16));
    }
    void FixedUpdate()
    {
        AttackTime += Time.deltaTime;
    }
    IEnumerator CreateGhostFire(int ballcount)
    {
        while (ballcount-- > 0)
        {
            GhostFireBalls.Enqueue(Instantiate(GhostFireBallPrefab, transform.position, transform.rotation));
            yield return new WaitForSeconds(0.5f);
        }
    }
    public float AttackTime;
    public float AttackTimeCd;
    private void OnTriggerStay(Collider other)
    {

        if (other.transform.CompareTag("Enemy"))
        {
            //吐出来一个鬼火
            if (AttackTime> AttackTimeCd && GhostFireBalls.Count > 0)
            {
                AttackTime = 0f;
                GameObject go = GhostFireBalls.Dequeue();
                go.GetComponent<GhostFireBall>().taget = other.transform;
                go.GetComponent<GhostFireBall>().isfollow = false;
            }
        }
    }
}
