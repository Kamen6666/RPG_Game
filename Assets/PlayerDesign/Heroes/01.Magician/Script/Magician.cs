using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum MagicianSkillDataEnum {火球,炎球,闪现,雷盾,黑洞,火柱 };
public class Magician : MonoBehaviour
{
    //技能计时器
    //public Slider Normalskill;
    //技能释放时间记录
    public float[] SkillUseTime;
    [Header("技能数据时间")]
    public SkillData magicianSkillData;
    [Header("火球预制体")]
    public GameObject fireballPrefab;
    [Header("魔法阵预制体")]
    public GameObject MagicCirclePrefab;
    [Header("黑洞预制体")]
    public GameObject BlackHolePrefab;
    [Header("闪电盾预制体")]
    public GameObject LightingShieldPrefab;
    [Header("火柱预制体")]
    public GameObject FirePaillarPointPrefab;
    //玩家动画组件
    private Animator ani;
    //玩家控制组件
    private PlayerController pc;
    //攻击状态开关
    private bool isAttack = false;
    void Awake()
    {
        ani = GetComponent<Animator>();
        pc = GetComponent<PlayerController>();
        //初始化数组
        SkillUseTime = new float[magicianSkillData.skills.Count];
        for (int i = 0; i < SkillUseTime.Length; i++)
            SkillUseTime[i] = 0f;
    }
    //释放技能
    public void UseEffect(string com)
    {
        if (!isAttack)
        {
            switch (com)
            {
                case "火球":
                    if (Time.time - SkillUseTime[(int)MagicianSkillDataEnum.火球] > magicianSkillData.skills[(int)MagicianSkillDataEnum.火球].SkillCd)
                    {
                        //火球
                        StartCoroutine(NormalAttack()); 
                    }
                    break;
                case "炎球":
                    //连发火球
                    if (Time.time - SkillUseTime[(int)MagicianSkillDataEnum.炎球] > magicianSkillData.skills[(int)MagicianSkillDataEnum.炎球].SkillCd)
                    {
                        StartCoroutine(SOLOAttack());
                    }
                    break;
                case "黑洞":
                    //黑洞
                    if (Time.time - SkillUseTime[(int)MagicianSkillDataEnum.黑洞] > magicianSkillData.skills[(int)MagicianSkillDataEnum.黑洞].SkillCd)
                    {
                        StartCoroutine(BlackHole(8f));
                    }
                    break;
                case "雷盾":
                    //闪电盾
                    if (Time.time - SkillUseTime[(int)MagicianSkillDataEnum.雷盾] > magicianSkillData.skills[(int)MagicianSkillDataEnum.雷盾].SkillCd)
                    {
                        StartCoroutine(Lighting());
                    }
                    break;
                case "火柱":
                    //连环火柱
                    if (Time.time - SkillUseTime[(int)MagicianSkillDataEnum.火柱] > magicianSkillData.skills[(int)MagicianSkillDataEnum.火柱].SkillCd)
                    {
                        StartCoroutine(FirePaillar());
                    }
                    break;
                case "闪现":
                    //闪现
                    if (Time.time - SkillUseTime[(int)MagicianSkillDataEnum.闪现] > magicianSkillData.skills[(int)MagicianSkillDataEnum.闪现].SkillCd)
                    {
                        StartCoroutine(Twinkle(5f));
                    }
                    break;
                default:
                    break;
            }
        }
    }
    #region 火柱
    IEnumerator FirePaillar()
    {
        if (PlayerManager.instance.CrtMp < magicianSkillData.skills[(int)MagicianSkillDataEnum.火柱].needmp)
            yield break;
        //进入攻击状态
        isAttack = true;
        //开启攻击动画
        ani.SetTrigger("Shockwave Attack");
        //扣除消耗
        PlayerManager.instance.CrtMp -= magicianSkillData.skills[(int)MagicianSkillDataEnum.火柱].needmp;
        //释放火柱点
        Instantiate(FirePaillarPointPrefab, transform).transform.localPosition = new Vector3(0, 0.1f, 0);
        //进入CD
        SkillUseTime[(int)MagicianSkillDataEnum.火柱] = Time.time;
        //等待技能结束
        yield return new WaitForSeconds(5f);
        //关闭协成
        StopCoroutine(NormalAttack());
        //离开攻击状态
        isAttack = false;
    }
    #endregion
    #region 闪电盾
    IEnumerator Lighting()
    {
        if (PlayerManager.instance.CrtMp < magicianSkillData.skills[(int)MagicianSkillDataEnum.雷盾].needmp)
            yield break;
        //进入cd
        SkillUseTime[(int)MagicianSkillDataEnum.雷盾] = Time.time;
        //进入攻击状态
        isAttack = true;
        //开启攻击动画
        ani.SetTrigger("Shockwave Attack");
        //动画延迟
        yield return new WaitForSeconds(1f);
        //检测是否被打断
        //扣除消耗
        //扣除消耗
        PlayerManager.instance.CrtMp -= magicianSkillData.skills[(int)MagicianSkillDataEnum.雷盾].needmp;
        //释放闪电盾
        Transform lighting = Instantiate(LightingShieldPrefab, transform).transform;
        lighting.localPosition = new Vector3(0, 0.75f, 0);
        lighting.GetComponent<LigthningShield>().AtkPower = (int)(PlayerManager.instance.atk * 0.1f);
        //技能僵直
        yield return new WaitForSeconds(0.5f);
        //关闭协成
        StopCoroutine(NormalAttack());
        //离开攻击状态
        isAttack = false;
    }
    #endregion
    #region 黑洞
    IEnumerator BlackHole(float distance)
    {
        if (PlayerManager.instance.CrtMp < magicianSkillData.skills[(int)MagicianSkillDataEnum.黑洞].needmp)
            yield break;
        //进入攻击状态
        isAttack = true;
        //开启攻击动画
        ani.SetTrigger("Shockwave Attack");
        //动画延迟
        yield return new WaitForSeconds(1f);
        //检测是否被打断
        //扣除消耗
        PlayerManager.instance.CrtMp -= magicianSkillData.skills[(int)MagicianSkillDataEnum.黑洞].needmp;
        //进入CD
        SkillUseTime[(int)MagicianSkillDataEnum.黑洞] = Time.time;
        //释放黑洞
        InstallBlackHole(distance);
        yield return new WaitForSeconds(0.5f);
        //关闭协成
        StopCoroutine(NormalAttack());
        //离开攻击状态
        isAttack = false;
    }
    //召唤黑洞
    private void InstallBlackHole(float distance)
    {
        Transform firepoint = transform.Find("FirePoint");
        BlackBall bb = Instantiate(BlackHolePrefab, firepoint.position, firepoint.rotation).GetComponent<BlackBall>();
        //设置黑洞吸怪范围
        bb.MaxRadius = 8f;
        //设置黑洞初始位置
        bb.transform.position += (firepoint.forward * distance);
    }
    #endregion
    #region 闪烁协成
    IEnumerator Twinkle(float distance)
    {
        if (PlayerManager.instance.CrtMp < magicianSkillData.skills[(int)MagicianSkillDataEnum.闪现].needmp)
            yield break;
        //进入CD
        SkillUseTime[(int)MagicianSkillDataEnum.闪现] = Time.time;
        //进入攻击状态
        isAttack = true;
        //扣除消耗
        PlayerManager.instance.CrtMp -= magicianSkillData.skills[(int)MagicianSkillDataEnum.闪现].needmp;
        //设置位移距离
        transform.position += transform.forward * distance;
        //间隙
        yield return new WaitForSeconds(0.5f);
        //退出攻击状态
        isAttack = false;
    }
    #endregion
    #region 火球
    //普通攻击协成
    IEnumerator NormalAttack()
    {
        if (PlayerManager.instance.CrtMp < magicianSkillData.skills[(int)MagicianSkillDataEnum.火球].needmp)
            yield break;
        //进入攻击状态
        isAttack = true;
        //开启攻击动画
        ani.SetTrigger("Attack");
        //等待动画播放
        yield return new WaitForSeconds(0.8f);
        //扣除消耗
        PlayerManager.instance.CrtMp -= magicianSkillData.skills[(int)MagicianSkillDataEnum.火球].needmp;
        //释放火球
        NormalAttackEffect(20f);
        //进入CD
        SkillUseTime[(int)MagicianSkillDataEnum.火球] = Time.time;
        //等待火球结束
        yield return new WaitForSeconds(0.6f);
        //关闭协成
        StopCoroutine(NormalAttack());
        //离开攻击状态
        isAttack = false;
    }

    //发射火球
    private void NormalAttackEffect(float distance)
    {
        //向前方释放一个火球
        //需求：火球预制体 开火点 火球飞行速度 火球存在时间
        //获得开火点 的Transfrom
        Transform firepoint = transform.Find("FirePoint");
        //给火球刚体
        Rigidbody rd = Instantiate(fireballPrefab, firepoint.position, firepoint.rotation).GetComponent<Rigidbody>();
        //给火球一个速度
        rd.velocity = rd.transform.forward * 20f;
        //给火球攻击力赋值
        rd.transform.GetComponent<FireBall>().atk = (int)(PlayerManager.instance.atk * 0.75f);
        //火球的存活时间
        Destroy(rd.gameObject, distance / 20f);
    }
    #endregion
    #region 炎球
    IEnumerator SOLOAttack()
    {
        if (PlayerManager.instance.CrtMp < magicianSkillData.skills[(int)MagicianSkillDataEnum.炎球].needmp)
            yield break;
        //进入攻击状态
        isAttack = true;
        //开启攻击动画
        ani.SetTrigger("Shockwave Attack");
        //动画延迟
        yield return new WaitForSeconds(1f);
        //获取发射点
        Transform firepoint = transform.Find("FirePoint");
        //扣除消耗
        PlayerManager.instance.CrtMp -= magicianSkillData.skills[(int)MagicianSkillDataEnum.炎球].needmp;
        //生成魔法阵
        Destroy(Instantiate(MagicCirclePrefab, firepoint), 3f);
        //进入CD
        SkillUseTime[(int)MagicianSkillDataEnum.炎球] = Time.time;
        int i = 20;
        while (--i > 0)
        {
            //随机生成点
            SOLOAttackEffect(firepoint.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.1f, 0.5f), 0f) + firepoint.forward * 1f);
            yield return new WaitForSeconds(0.1f);
        }
        //等待火球都消亡
        yield return new WaitForSeconds(1f);
        //关闭协成
        StopCoroutine(NormalAttack());
        //离开攻击状态
        isAttack = false;
    }
    //连续召唤火球
    private void SOLOAttackEffect(Vector3 newPoint)
    {
        //火球连发
        //向前方释放一个火球
        //需求：火球预制体 开火点 火球飞行速度 火球存在时间
        Transform firepoint = transform.Find("FirePoint");
        //给火球刚体
        Rigidbody rd = Instantiate(fireballPrefab, newPoint, firepoint.rotation).GetComponent<Rigidbody>();
        //给火球一个速度
        rd.velocity = rd.transform.forward * 10f;
        //给火球攻击力赋值
        rd.transform.GetComponent<FireBall>().atk = (int)(PlayerManager.instance.atk * 0.2f);
        //火球的存活时间
        Destroy(rd.gameObject, 1f);
    }
    #endregion
    //变成龙
    private void DragonShape()
    {
        //魔法阵
        //遮挡玩家视线
        //替换预制体
    }
    /// <summary>
    /// 返回技能冷却时间
    /// </summary>
    /// <param name="skillname">技能名字</param>
    /// <param name="useSkillTimer">释放技能的时间</param>
    public void SkillShadow(string skillname, out float useSkillTimer,out float Skillcd)
    {
        switch (skillname)
        {
            case "火球":
                useSkillTimer = SkillUseTime[(int)MagicianSkillDataEnum.火球];
                Skillcd = magicianSkillData.skills[(int)MagicianSkillDataEnum.火球].SkillCd;
                break;
            case "炎球":
                useSkillTimer = SkillUseTime[(int)MagicianSkillDataEnum.炎球];
                Skillcd = magicianSkillData.skills[(int)MagicianSkillDataEnum.炎球].SkillCd;
                break;
            case "黑洞":
                useSkillTimer = SkillUseTime[(int)MagicianSkillDataEnum.黑洞];
                Skillcd = magicianSkillData.skills[(int)MagicianSkillDataEnum.黑洞].SkillCd;
                break;
            case "雷盾":
                useSkillTimer = SkillUseTime[(int)MagicianSkillDataEnum.雷盾];
                Skillcd = magicianSkillData.skills[(int)MagicianSkillDataEnum.雷盾].SkillCd;
                break;
            case "火柱":
                useSkillTimer = SkillUseTime[(int)MagicianSkillDataEnum.火柱];
                Skillcd = magicianSkillData.skills[(int)MagicianSkillDataEnum.火柱].SkillCd;
                break;
            case "闪现":
                useSkillTimer = SkillUseTime[(int)MagicianSkillDataEnum.闪现];
                Skillcd = magicianSkillData.skills[(int)MagicianSkillDataEnum.闪现].SkillCd;
                break;
            default:
                useSkillTimer = 0f;
                Skillcd = 0f;
                break;
        }
    }
}
