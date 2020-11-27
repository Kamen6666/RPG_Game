using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonPet : MonoBehaviour
{
    [Header("血量回复量")]
	public int hpReplyValue;
	[Header("mp回复量")]
	public int mpReplyValue;
    [Header("最大生命值buff")]
    public int HpBuffValue;
    [Header("最大魔法值buff")]
    public int MpBuffValue;
    [Header("攻击力buff")]
	public int atkBuffValue;
	[Header("防御力buff")] 
	public int defenceBuffValue;
	[Header("移速buff")]
	public int moveBuffValue;
	[Header("隔几秒加一次")]
	public int updataTime;
    [Header("宠物存活时间")]
    public int petAliveTime;

    private Rigidbody rig;
    private BoxCollider boxCol;
    private Animator ani;
    private PetFlow petFlow;

	private float timeCount = 0;
    private float aliveTime = 0;
    /// <summary>
    /// 出生时添加数值
    /// </summary>
    private void Start()
    {
        ani=GetComponent<Animator>();
        rig = GetComponent<Rigidbody>();
        boxCol = GetComponent<BoxCollider>();
        petFlow = GetComponent<PetFlow>();

        //添加属性
        PlayerManager.instance.petState[0] = HpBuffValue;
        PlayerManager.instance.petState[1] = MpBuffValue;
        PlayerManager.instance.petState[2] = atkBuffValue;
        PlayerManager.instance.petState[3] = defenceBuffValue;
        //添加速度
        PlayerManager.instance.speed += moveBuffValue;
    }
    private bool isDestory=false;
    private void Update()
    {
        PatAlive();
        PlayerReply();
    }
    
    private void PatAlive()
    {
    	aliveTime += Time.deltaTime;
        if (petAliveTime <= aliveTime && !isDestory)
        {
	        
        	petFlow.enabled=false;
            //销毁时数值回收
            PlayerManager.instance.petState[0] = 0;
            PlayerManager.instance.petState[1] = 0;
            PlayerManager.instance.petState[2] = 0;
            PlayerManager.instance.petState[3] = 0;
            PlayerManager.instance.speed -= moveBuffValue;

            rig.useGravity = true;
            boxCol.enabled = true;
            ani.SetTrigger("Fly Die");
            Destroy(gameObject, 5f);
            isDestory = true;
            
        }

    }
    /// <summary>
    /// 玩家回复
    /// </summary>
    private void PlayerReply()
	{
        timeCount += Time.deltaTime;
        if (timeCount>=updataTime)
        {
            PlayerManager.instance.CrtHp += hpReplyValue;
            PlayerManager.instance.CrtMp += mpReplyValue;
            timeCount = 0;
        }
	}
    
}
