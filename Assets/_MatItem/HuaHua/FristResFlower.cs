using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FristResFlower : MonoBehaviour
{
    private List<Transform> path;
    private List<int> result;
    private GameObject bossParent;
    private GameObject duwu;
    private GameObject boss;
    private GameObject resFakeFlower;

    private FlowerManager flowerManager;
    //活着的花
    private void Awake()
    {
        result = new List<int>();
       
       
        flowerManager = GetComponent<FlowerManager>();
        bossParent = flowerManager.bossParent;
        boss = bossParent.transform.Find("HuaHuaBoss").gameObject;
        duwu = bossParent.transform.Find("duwu").gameObject;
        //获取所有随机生成位置
        path = new List<Transform>();
        //获取所有随机生成位置
        for (int i = 0; i < transform.childCount; i++)
        {
            path.Add(transform.GetChild(i));
            result.Add(i);
        }
    }
    private void OnEnable()
    {
        /// boss70%血  以下 第一次生成
        StartCoroutine(BossDisappear());
    }
    
    IEnumerator BossDisappear()
    {
        duwu.SetActive(false);
        boss.SetActive(false);
        //生成不重复的路径随机怪物
        int bossBothCount = Random.Range(2, path.Count);
        List<int> a = new List<int>();

        a = MyRandom(result, 5);
        for (int i = 0; i < a.Count; i++)
        {
            if (a[i] == bossBothCount)
            {
                a[i] = 1;
            }
        }
        for (int i = 0; i < 5; i++)
        {
            int aa = a[i];
            ResFakeFlower(path[aa]);
        }


        yield return new WaitForSeconds(3.5f);


        // Debug.Log(path.Length);



        bossParent.transform.parent = path[bossBothCount];
        bossParent.transform.localPosition = Vector3.zero;
      
        duwu.SetActive(true);
        boss.SetActive(true);
    }

    /// <summary>
    /// 实例化 假花boss
    /// </summary>
    /// <param name="resPos"></param>
    private void ResFakeFlower(Transform resPos)
    {
        resFakeFlower = Instantiate(Resources.Load<GameObject>("FakeBoss"));
        resFakeFlower.transform.parent = resPos;
        resFakeFlower.transform.localPosition = Vector3.zero;
        //添加到活的假boss
        flowerManager.aliveFake.Add(resFakeFlower);
    }
    /// <summary>
    /// 生成不重复的随机数
    /// </summary>
    /// <param name="total">一共有几个</param>
    /// <param name="n"></param>
    /// <returns></returns>
    /// <summary>
    /// 固定数组中的不重复随机
    /// </summary>
    /// <param name="nums">数组</param>
    /// <param name="count">要随机的个数</param>
    /// <returns></returns>
    public List<int> MyRandom(List<int> nums, int count)
    {
        if (count > nums.Count)
        {
            Debug.LogError("要取的个数大于数组长度！");
            return null;
        }

        List<int> result = new List<int>();
        List<int> id = new List<int>();

        for (int i = 0; i < nums.Count; i++)
        {
            id.Add(i);
        }

        int r;
        while (id.Count > nums.Count - count)
        {
            r = Random.Range(0, id.Count);
            result.Add(nums[id[r]]);
            id.Remove(id[r]);
        }
        return (result);
    }
}
