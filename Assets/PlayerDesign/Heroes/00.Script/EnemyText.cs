using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyText : MonoBehaviour
{
    [Header("生成的怪物")]
    public GameObject prefabs;
    [Header("生成范围")]
    public int radius;
    [Header("生成数量")]
    public int enemyCount;
    [Header("刷新时间")]
    public float resetTime;
    private float TimeCd;
    void Awake()
    {
        if (resetTime < 1f)
            resetTime = 5f;
        TimeCd = 0f;
        RandomRadius(enemyCount, radius, 1, prefabs, transform);
    }

    void FixedUpdate()
    {
        TimeCd += Time.deltaTime;
        if (TimeCd > resetTime)
        {
            RandomRadius(enemyCount - transform.childCount , radius, 1, prefabs, transform);
            TimeCd = 0f;
        }
    }
    //生成怪物
    public void RandomRadius(int resNumber, int resRadius, int noResRadius, GameObject prefabs, Transform trans)
    {
        for (int i = 0; i < resNumber; i++)
        {
            Vector2 p = Random.insideUnitCircle * resRadius;
            Vector2 pos = p.normalized * (noResRadius + p.magnitude);
            Vector3 pos1 = new Vector3(trans.position.x + pos.x, 0, trans.position.z + pos.y);
            //Vector3 pos2 =new Vector3(pos.x, 0, pos.y);
            //出生点为其父类
            Instantiate(prefabs, pos1, Quaternion.identity).transform.parent = transform;
        }
    }
}
