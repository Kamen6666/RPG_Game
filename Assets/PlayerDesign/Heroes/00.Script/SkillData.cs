using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skill/New Skill")]
public class SkillData : ScriptableObject
{
    public  List<Skill> skills = new List<Skill>();
}
