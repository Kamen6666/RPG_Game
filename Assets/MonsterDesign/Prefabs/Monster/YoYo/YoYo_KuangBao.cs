using System.Collections;
using UnityEngine;

public class YoYo_KuangBao : MonoBehaviour
{
    private  Texture texture;
    private  SkinnedMeshRenderer renderer;
    private State state;
    private Animator ani;
    //小怪物
    private GameObject smallRes;
    private  RandomRadius random;
    private void Awake()
    {
        state = GetComponent<State>();
        renderer = GetComponentInChildren<SkinnedMeshRenderer>();
        ani = GetComponent<Animator>();
        
    }
    private void Start()
    {
        smallRes = Resources.Load(GameConst.MONSTER_YOYO_YOYO_RES) as GameObject;

        texture = Resources.Load(GameConst.MONSTER_YOYO_MAT_ORANGE) as Texture;
        if (texture == null)
            throw new System.Exception("yoyo_red==nil");
    }
    private bool isRes = false;
    /// <summary>
    /// boss生成小怪
    /// </summary>
    /// <returns></returns>
    IEnumerator  YoYosamallRes()
    {
        isRes = true;
        yield return new WaitForSeconds(2f);
        random = new RandomRadius(5, 3, 1, smallRes, transform);
       
    }
    /// <summary>
    /// boss变狂暴
    /// </summary>
    public void  YoYoChanegRed()
    {
        renderer.materials[0].mainTexture = texture;
        ani.speed = GameConst.MONSTER_YOYO_BOSS_SPEEDUP;
    }
    private void Update()
    {
        if (isRes)
            return;
        if (state.hP<= state.initialHP * 0.8f)
        {
            YoYoChanegRed();
            if (state.hP<= state.initialHP * 0.4f)
            {
                StartCoroutine(YoYosamallRes()) ;
            }
        }
       
        
    }
}
