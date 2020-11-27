
using UnityEngine;

public class Res_RandomYoYo : MonoBehaviour
{
    private int currentDifficulty;
    private SkinnedMeshRenderer skinned;
    private ParticleSystem particle;
    private  Texture texture;
    private  void Start()
    {
        skinned = GetComponentInChildren<SkinnedMeshRenderer>();
        particle = GetComponentInChildren<ParticleSystem>();
        
        currentDifficulty = Random.Range(1, 4);
        RandomYoYo();
       
    }

    /// <summary>
    /// 随机生成怪物颜色，改变粒子特效颜色
    /// </summary>
    public void RandomYoYo()
    {
        switch (currentDifficulty)
        {

            case 1:
                texture = Resources.Load(GameConst.MONSTER_YOYO_MAT_BLUE) as Texture;
                skinned.materials[0].mainTexture = texture;
                particle.startColor = Color.blue;
                break;
            case 2:
                texture = Resources.Load(GameConst.MONSTER_YOYO_MAT_ORANGE) as Texture;
                skinned.materials[0].mainTexture = texture;
                particle.startColor = Color.red;
                break;
            case 3:
                texture = Resources.Load(GameConst.MONSTER_YOYO_MAT_GREEN) as Texture;
                skinned.materials[0].mainTexture = texture;
                particle.startColor = Color.green;
                break;
        }
    }
   
}
