using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuaHuaBreathAttack : MonoBehaviour
{
    private float timeCount;
    private Transform duwuPos;
    private Animator ani;
    private int randomAttack;
    [Header("毒雾")]
    public GameObject duwu;

    public bool isDuwuAttack;
    private void Awake()
    {
        ani = GetComponent<Animator>();
        duwuPos = transform.Find("DuWuPos");
    }
   
    void Update()
    {
        timeCount += Time.deltaTime;

        if (timeCount >= 5)
        {
            randomAttack = Random.Range(0, 100);
            if (randomAttack <= 40)
            {
                // Debug.Log(randomAttack);
                isDuwuAttack = true;

                DuWuAttack();
            }
         
            timeCount = 0;

        }
        Debug.Log("isDuwuAttack=====" + isDuwuAttack);
    }

  
    private void Start()
    {
        //DuWuAttack();
    }
    /// <summary>
    /// 毒雾攻击
    /// </summary>
    private void DuWuAttack()
    {
        ani.SetTrigger("Cast Spell");
        GameObject duwu =Instantiate(Resources.Load<GameObject>("DuWu"));
        if (duwu == null)
            throw new System.Exception("duwu==nil");
        duwu.transform.position = duwuPos.position;
        isDuwuAttack = false;
    }


}
