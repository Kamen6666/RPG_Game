using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShortCutGrid : MonoBehaviour
{
    //设置按键
    public KeyCode _keycode;
    //当前放置的是什么技能
    public string SkillName;
    public Text skillcd;
    public Image skillshadow;
    //技能使用的时间
    private float SkillUseTime;
    //技能CD
    private float SkillCd;
    //技能释放器
    private Magician useSkill;
    void Start()
    {
        //初始化计数器
        SkillUseTime = 0f;
        SkillCd = 0f;
        //没技能则隐藏技能遮罩
        if (SkillName == "")
            transform.Find("Mask").gameObject.SetActive(false);
        //获得技能释放器
        useSkill = GameObject.FindGameObjectWithTag("Player").GetComponent<Magician>();
    }

    void FixedUpdate()
    {
        //没赋值技能则返回
        if (SkillName == null)
            return;
        //技能释放
        if (Input.GetKeyDown(_keycode))
        {
            useSkill.UseEffect(SkillName);
        }

        //计算技能CD
        useSkill.SkillShadow(SkillName, out SkillUseTime, out SkillCd);
        float needTime = (Time.time - SkillUseTime) < 0f ? 0f : (Time.time - SkillUseTime);
        if (needTime > SkillCd)
        {
            if (skillcd.text.Length > 0)
            {
                skillcd.text = "";
            }
        }
        else
        {
            skillcd.text = string.Format("{0:N1}", SkillCd - needTime);
        }
        skillshadow.fillAmount = (1f - needTime / SkillCd) < 0.1f ? 0f : (1f - needTime / SkillCd);
    }

    public void GetSkillName(string name)
    {
        SkillName = name;
        transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite =
            Resources.Load<Sprite>("Skill/" + SkillName);
        transform.GetChild(0).gameObject.SetActive(true);
    }
}
