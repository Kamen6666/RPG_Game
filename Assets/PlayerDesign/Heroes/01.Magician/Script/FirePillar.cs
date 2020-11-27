using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePillar : MonoBehaviour
{
    public GameObject FirePillarPrefabs;
    public int Atk;
    private Transform[] transforms;
    void Awake()
    {
        //初始化数组
        transforms = new Transform[transform.childCount];
        //放入子物体
        for (int i = 1; i < transform.childCount; i++)
            transforms[i] = transform.GetChild(i);
    }
    void OnEnable()
    {
        //启动线程
        StartCoroutine(CrateFirePillarPrefabs());
    }
    IEnumerator CrateFirePillarPrefabs()
    {
        //初始距离
        float currectDistance = 1f;
        //初始大小
        float currentSize = 1f;
        //等待
        yield return new WaitForSeconds(1f);
        for (int c = 2; c < 5; c++)
        {
            for (int i = 1; i < transforms.Length; i++)
            {
                //创建预制体
                FireBall fb = Instantiate(FirePillarPrefabs,
                    transforms[i].forward * currectDistance + transforms[i].position,
                    transforms[i].rotation).GetComponent<FireBall>();
                fb.atk = Atk;
                fb.transform.localScale = Vector3.one * currentSize;
            }
            yield return new WaitForSeconds(c*0.15f);
            currectDistance += c;
            currentSize += 0.6f;
        }
        Destroy(gameObject);
        yield return new WaitForSeconds(1f);
    }
}
