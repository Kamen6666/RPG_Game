using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public static MonsterManager Instance;
    List<GameObject> monstersList;
    

    private void Awake()
    {
        Instance = this;
        monstersList = new List<GameObject>();
       /* foreach (GameObject go in GameObject.FindGameObjectsWithTag("Enemy")) 
        {
            monstersList.Add(go);
        }*/
       
    }
    //死了

    public void RemoveMonsterFromList(GameObject monster)
    {
        if (monstersList.Contains(monster))
        {
            //从列表中移除
            monstersList.Remove(monster);
        }
    }
    //出生
    public void AddMonsterFromList(GameObject monster)
    {
            monstersList.Add(monster);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="player">玩家位置</param>
    /// <param name="angle">攻击角度</param>
    /// <param name="distance">攻击距离</param>
    /// <param name="number">怪物数量</param>
    /// <param name="attackPower">玩家攻击力</param>
    public void ReduceMonsterHP(Transform player,float angle,float distance,int number,int attackPower)
    {
        for (int i = 0; i < monstersList.Count; i++)
        {
            if (number <= 0)
                return;
            if (Vector3.Distance(player.position,monstersList[i].transform.position)>distance)
            {
                
                continue;
            }
            if (Vector3.Angle(monstersList[i].transform.position-player.position,player.forward)>angle)
            {
                continue;
            }
            monstersList[i].GetComponent<State>().TakeDamage(attackPower);

            number--;
        }
    }

}
