using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum NPCSort
{
    商店类=2000,
    装备类,
    任务
}
[CreateAssetMenu(fileName = "New NPC", menuName = "NPC/New NPC")]
public class NPCData: ScriptableObject
{
    
    [Header("NPC名称")]
    public string NPCName;
    [Header("NPC种类")]
    public NPCSort npcSort;

    public List<PropItems_Enum> props;
}
