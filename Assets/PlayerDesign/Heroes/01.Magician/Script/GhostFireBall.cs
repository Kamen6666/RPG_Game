using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostFireBall : MonoBehaviour
{
    //环绕目标
    public Transform taget;
    //是否跟随玩家
    public bool isfollow;
    //鬼火攻击力
    private int Atk;
    void Start()
    {
        taget = GameObject.FindGameObjectWithTag("Player").transform;
        transform.SetParent(taget);
        isfollow = true;
        transform.localPosition = new Vector3(1f, 1f, 1f);

        Atk = (int)(PlayerManager.instance.atk * 0.5f);
    }

    void Update()
    {
        if (isfollow)
        {
            //跟随玩家
            transform.RotateAround(taget.position, Vector3.up, Time.deltaTime * 50f);
        }
        else
        {
            if (Vector3.Distance(transform.position, taget.position) < 0.5f)
            {
                taget.GetComponent<State>().TakeDamage(Atk);
                Destroy(gameObject);
            }
            //追踪敌人
            transform.position = Vector3.Lerp(transform.position, taget.position, 0.4f);
        }
    }
}
