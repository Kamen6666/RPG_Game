using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[SerializeField]
public class ResEnemy : MonoBehaviour
{
    [Header("生成的怪物")]
    public GameObject[] monsterPrefab;
    [Header("生成范围")]
    public int radius;
    [Header("生成数量")]
    public int enemyCount;
    [Header("刷新时间")]
    public float resetTime;
    [Header("怪物等级")]
    public int monsterLevel;
    private float TimeCd;
    private  int Monstercount;
    void Awake()
    {
        Monstercount = monsterPrefab.Length;
        if (resetTime < 1f)
            resetTime = 5f;
        TimeCd = 0f;
        //Debug.Log(Monstercount);
        for (int i = 0; i < Monstercount; i++)
        {
            int random = Random.Range(0, Monstercount);
            RandomRadius(1, radius, 1, monsterPrefab[random], transform);
        }
        
    }
    void FixedUpdate()
    {
        TimeCd += Time.deltaTime;
       
        if (TimeCd > resetTime)
        {
            if (transform.childCount<=Monstercount)
            {
                int random = Random.Range(0, Monstercount);
                RandomRadius(1, radius, 1, monsterPrefab[random], transform);
                TimeCd = 0f;
            }
           
        }
    }
    //生成怪物
    public void RandomRadius(int resNumber, int resRadius, int noResRadius, GameObject prefabs, Transform trans)
    {
        for (int i = 0; i < resNumber; i++)
        {
            Vector2 p = Random.insideUnitCircle * resRadius;
            Vector2 pos = p.normalized * (noResRadius + p.magnitude);
            Vector3 pos1 = new Vector3(trans.position.x + pos.x, transform.position.y, trans.position.z + pos.y);
            //Vector3 pos2 =new Vector3(pos.x, 0, pos.y);
            //出生点为其父类
            GameObject monster = Instantiate(prefabs, pos1, Quaternion.identity);
            monster.GetComponent<State>().ResetLevel(monsterLevel);
            monster.transform.parent = transform;
        }
    }

}
