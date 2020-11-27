using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerClone : MonoBehaviour
{
    private List<Transform> path;
    private List<int> result;
    private FlowerBoss flowerBoss;
    private GameObject bossParent;
    private GameObject duwu;
    private GameObject boss;
    private GameObject resFakeFlower;
    //活着的花
    private FlowerManager flowerManager;
    private State state;

    //=======================================================
    //第一次生成
    private FristResFlower fristRes;
    private void Awake()
    {
        result = new List<int>();
       
        flowerManager = GetComponent<FlowerManager>();
        bossParent = flowerManager.bossParent;
        fristRes = GetComponent<FristResFlower>();
        path = new List<Transform>();
        //获取所有随机生成位置
        for (int i = 0; i < transform.childCount; i++)
        {
            path.Add(transform.GetChild(i));
            result.Add(i);
        }
      
        boss = bossParent.transform.Find("HuaHuaBoss").gameObject;
        duwu = bossParent.transform.Find("duwu").gameObject;
    }
    // Start is called before the first frame update
    private void OnEnable()
    {
        //五个怪被打死之后 在生成一波  30s一波
        StartCoroutine(BossDisappear());
    }

    IEnumerator BossDisappear()
    {
        duwu.SetActive(false);
        boss.SetActive(false);
        //生成不重复的路径随机怪物
        yield return new WaitForSeconds(3.5f);
        int bossBothCount = Random.Range(2, path.Count);
        result = MyRandom(result, 5);
        bossParent.transform.parent = path[bossBothCount];
        bossParent.transform.localPosition = Vector3.zero;
        duwu.SetActive(true);
        boss.SetActive(true);

        yield return new WaitForSeconds(15f);

        for (int i = 0; i < result.Count; i++)
        {
            if (result[i] == bossBothCount)
            {
                result[i] = 1;
            }
        }
        for (int i = 0; i < 5; i++)
        {
            int a = result[i];
            ResFakeFlower(path[a]);
        }

      
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
