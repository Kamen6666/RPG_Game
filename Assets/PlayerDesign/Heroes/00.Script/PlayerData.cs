using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Hero {Magician,Werwolf,Archer,Knight }
[CreateAssetMenu(fileName="New Player",menuName="Player/New Player")]
public class PlayerData : ScriptableObject
{
    public Hero heroType;
    public int[] stateBase;
    public int[] stateGrowp;
    public int[] Exp;
    public void SetBaseToSQL()
    { 
        
    }
}
