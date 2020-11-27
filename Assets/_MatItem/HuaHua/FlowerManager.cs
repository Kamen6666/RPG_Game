using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerManager : MonoBehaviour
{
    public static FlowerManager Instance;
    private List<Transform> path;
    private FlowerBoss flowerBoss;
    public GameObject bossParent;
    private GameObject boss;
    public GameObject duwu;
    //活着的花
    public  List<GameObject> aliveFake;
    private State state;

    //=======================================================
    //第一次生成
    private FristResFlower fristRes;
    private FlowerClone flowerClone;
    private void Awake()
    {
        Instance = this;
        fristRes = GetComponent<FristResFlower>();
        flowerClone = GetComponent<FlowerClone>();
        aliveFake = new List<GameObject>();
        bossParent = Instantiate(bossParent);
        //获取所有随机生成位置
        path = new List<Transform>();
        //获取所有随机生成位置
        for (int i = 0; i < transform.childCount; i++)
        {
            path.Add(transform.GetChild(i));
        }
        //找到boss
        boss = bossParent.transform.Find("HuaHuaBoss").gameObject;
        boss.transform.localPosition = Vector3.zero;
        int bossBothCount = Random.Range(2, path.Count);
        //boss的老爹设置位置
        bossParent.transform.parent = path[bossBothCount];
        bossParent.transform.localPosition = Vector3.zero;
    }
    private void Start()
    {
        state = boss.GetComponent<State>();
    }
    private bool isRes = false;
    private bool isFristRes = false;
    private float timeCount = 0;
    private void Update()
    {
        if (PlayerManager.instance.CrtHp <= 0)
        {
            
            Destroy(gameObject, 3f);
            
        }
        //Debug.Log(aliveFake.Count);
        if (state.initialHP*0.9f>=state.hP&&!isFristRes)
        {
            fristRes.enabled = true;
            isFristRes = true;
           
        }
        if (aliveFake.Count<=0&&!isRes&&isFristRes)
        {
           StartCoroutine(CloseOpenClone());
        }
       
        
    }
   
    IEnumerator CloseOpenClone()
    {
        yield return new WaitForSeconds(15);
        isRes = true;
        flowerClone.enabled = true;
       
        yield return new WaitForSeconds(40);
        isRes = false;
        
    }
    /// <summary>
    /// 从list中移除死掉的花
    /// </summary>
    /// <param name="fakeFlower"></param>
    public void RemoveAliveList(GameObject fakeFlower)
    {
        Debug.Log("RemoveAliveList" + fakeFlower.name);
        aliveFake.Remove(fakeFlower);
    }
}
