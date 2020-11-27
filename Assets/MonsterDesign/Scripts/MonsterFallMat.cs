using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFallMat : MonoBehaviour
{
    private State state;
  
    private List<string> mosnterMatList;
    private List<string> monsterMatBaoLVList;
    private List<int> matGaiLv;
    private List<string > result;
    private void Awake()
    {
        state = GetComponent<State>();
       
    }
    private void OnEnable()
    {
        ReaderMats();
        dsdada(state.monster_FallgenerateCount);
        ResMat();

    }
  
    /// <summary>
    /// 读取怪物所有掉落品
    /// </summary>
    private void ReaderMats()
    {
        result = new List<string>();

        mosnterMatList = new List<string>(state.monsterFallMat.Split('_'));

        monsterMatBaoLVList = new List<string>(state.monster_FallMatValue.Split('_'));

        matGaiLv = new List<int>();

        for (int i = 0; i < monsterMatBaoLVList.Count; i++)
        {
            matGaiLv.Add(int.Parse(monsterMatBaoLVList[i]));
        }
    }
  
    

    private void dsdada(int matCount)
    {
        int a = 0;
        for (int i = 0; i < mosnterMatList.Count; i++)
        {
            if (Random.Range(0, 100) < matGaiLv[i])
            {
                //生成
                result.Add(mosnterMatList[i]);
                
                a++;
                if (a > matCount)
                {
                    break;
                }
            }
            
        }
    }

    private void  ResMat()
    {
        for (int i = 0; i < result.Count; i++)
        {
            ResFall(result[i]);
            //Debug.Log(result[i]);
        }

    }
    /// <summary>
    /// 加载掉落
    /// </summary>
    /// <param name="material"></param>
    /// <returns></returns>
    private void  ResFall(string  material)
    {
        GameObject mat = Instantiate(Resources.Load("Mat/"+material)) as GameObject;
        if (mat == null)
            throw new System.Exception("Mat==nil");
        Transform pos= transform.Find("FallPos");
        if (pos == null)
            throw new System.Exception("pos==nil");
        mat.transform.SetParent(pos,false);
        //mat.transform.localPosition = Vector3.zero;
        mat.transform.parent = null;
    }
}
