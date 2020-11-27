using UnityEngine;
using Fungus;

[CommandInfo("LxScripts","LoadShopItem","加载商店列表")]
public class LoadShopItem : Command
{
    [SerializeField]
    bool ShopListEnabled;
    [SerializeField]
    GameObject Seller;

    public override void OnEnter()
    {
        Seller.GetComponent<NpcLoadData>().enabled = ShopListEnabled;
        Continue();
    }
}


