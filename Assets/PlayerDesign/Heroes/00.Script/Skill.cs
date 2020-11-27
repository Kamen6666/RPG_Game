using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//角色类型
public enum PlayerType {a_Magician, b_Werwolf,c_Archer,d_Knight };
//作用类型
public enum ApplyType {hp,mp,atk,def,speed,single,multi,none};
//作用属性
public enum ApplyProperty {hp,mp,atk,def,speed};
//释放类型
public enum ReieaseType {自身,敌人,点,前方}
[System.Serializable]
public class Skill
{
    public int SkillNum;//技能编号
    public string SkillName;//技能名称
    public string SkillIcon;//技能图标名称
    public string SkillIntrduce;//技能介绍
    public float SkillCd;//技能冷却时间
    public PlayerType playerType;//适用角色
    public int level;//需求的等级
    public int needmp;//需要消耗的法力值

    public Skill(int skillNum, string skillName, string skillIcon, string skillIntrduce, PlayerType playerType, int level, int needmp)
    {
        this.SkillNum = skillNum;
        SkillName = skillName;
        SkillIcon = skillIcon;
        SkillIntrduce = skillIntrduce;
        this.playerType = playerType;
        this.level = level;
        this.needmp = needmp;
    }
}
