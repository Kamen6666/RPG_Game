using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New EquipItem", menuName = "EquipItem/New EquipItem")]
public class EquipItemData : ScriptableObject
{
    public List<EquipmentData.EquipmentType> keys;
    public List<PropItems_Enum> values;

}
