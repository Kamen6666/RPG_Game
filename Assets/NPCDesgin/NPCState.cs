using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCState : MonoBehaviour
{
    public NPCData npcData;

    public string npcName;

    public NPCSort sort;

    public List<PropItems_Enum> props;

    private void Awake()
    {
        Refesh();
    }

    public void Refesh()
    {
        npcName = npcData.name;
        sort = npcData.npcSort;
        props = npcData.props;
    }
}
