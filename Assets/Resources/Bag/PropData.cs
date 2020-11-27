
using System.Collections.Generic;


public enum PropItems_Enum
{
    空,
    #region 材料
    铁锭 = 1000,
    死灵之书,
    怪物秘典,
    蓝币,
    金币,
    邪恶的鱼眼,
    绿钻石,
    钻石,
    红钻石,
    蓝宝石,
    红宝石,
    普通钥匙,
    贵重的钥匙,
    铁矿石,
    #endregion


    #region 道具
    原素瓶,//喝下去便能恢复血量
    女神的祝福,//完全恢复血量，并治愈所有异常状态
    原素灰瓶,//喝下去便能恢专注值	
    秘藏的祝福,//完全恢复专注值
    苔藓球果实,//能暂时提升所有的抵抗力持续60秒
    红虫药丸,//能提升30%的攻击力
    返回骨片,//能返回最后休息的营火处，或是祭祀场的营火
    诱敌头盖骨,//投掷破碎后，能引诱周围的敌人接近
    杰克的酒,//可以恢复血量、暂时提升抵抗力恢复20%血量
    锈斑硬币,//握碎后，可以暂时提升寻宝能力,寻宝能力提升至1.5倍，持续60秒
    锈斑金币,//握碎后，可以暂时大幅提升寻宝能力,寻宝能力提升至2倍，持续60秒
    七色石,//放在地上会发出光辉，变成指标
    #endregion

    #region 装备

    #region 头部
    老者大帽子,//以结晶老者身份闻名的孪生老师，将其面容完全掩盖住的巨大帽子
    卡露拉三角帽子,//三角帽子是异端魔法师的象征，但卡露拉却没有丢弃它
    #endregion

    #region 胸甲
    宫廷魔法师长袍,//罪业之都的宫廷魔法师穿戴的长袍
    卡露拉长袍, //暗魔法的使用者卡露拉的长袍，仿佛可以感受到长途旅行及在监狱里度过的时间
    #endregion


    #region 手部
    魔法师手套,//罪业之都的宫廷魔法师穿戴的丝绸手套
    沙之咒术师手套,//过去生息于堆土塔的沙之咒术师手套，据说深红色的薄布带有魔力。
    #endregion


    #region 戒指
    老者戒指, //成为深渊监视者的法兰魔法师，必定是孤独的战士
    伫立龙徽戒指,//其象征不朽古龙的徽印与龙学院相当匹配，也传达出“伫立的龙是魔法师的理想外在”的含义
    #endregion

    #region 腿部
    宫廷魔法师长裤,//罪业之都的宫廷魔法师穿戴的靴子
    魔法师长裤//彼海姆龙学院的魔法师旅行服装，以黑色皮革长裤与耐穿的长靴组合而成
    #endregion

    #endregion
}

public class PropData
{
    private static PropData Instance;
   


    public static PropData GetPrpoData()
    {
        if (Instance == null)
        {
            Instance = new PropData();
        }
        return Instance;
    }


    public static Dictionary<PropItems_Enum, PropItem> Props;
    private PropData()
    {
        Props = new Dictionary<PropItems_Enum, PropItem>();
        #region   添加材料

        Props.Add(PropItems_Enum.铁锭, new PropItem(PropItems_Enum.铁锭, "一块普普通通的铁", ItemType.Material, 100, 50, "铁锭"));

        Props.Add(PropItems_Enum.死灵之书, new PropItem(PropItems_Enum.死灵之书, "死者之书，散发着某种特殊的气息", ItemType.Material, 2, 1000, "死灵之书"));

        Props.Add(PropItems_Enum.怪物秘典, new PropItem(PropItems_Enum.怪物秘典, "怪物之间相传的某种书籍，", ItemType.Material, 5, 500, "怪物秘典"));

        Props.Add(PropItems_Enum.蓝币, new PropItem(PropItems_Enum.蓝币, "一枚常见的破损蓝币", ItemType.Material, 20, 200, "蓝币"));

        Props.Add(PropItems_Enum.金币, new PropItem(PropItems_Enum.金币, "稀有物品，可能是", ItemType.Material, 10, 400, "金币"));

        Props.Add(PropItems_Enum.邪恶的鱼眼, new PropItem(PropItems_Enum.邪恶的鱼眼, "邪恶的鱼眼，", ItemType.Material, 50, 40, "邪恶的鱼眼"));

        Props.Add(PropItems_Enum.绿钻石, new PropItem(PropItems_Enum.绿钻石, "绿宝石，散发蓝色光芒", ItemType.Material, 5, 400, "绿钻石"));

        Props.Add(PropItems_Enum.钻石, new PropItem(PropItems_Enum.钻石, "普通钻石，品质并没有那么好", ItemType.Material, 20, 200, "钻石"));

        Props.Add(PropItems_Enum.红钻石, new PropItem(PropItems_Enum.红钻石, "稀有物品红钻石", ItemType.Material, 5, 500, "红钻石"));

        Props.Add(PropItems_Enum.蓝宝石, new PropItem(PropItems_Enum.蓝宝石, "稀有物品蓝宝石", ItemType.Material, 5, 600, "蓝宝石"));

        Props.Add(PropItems_Enum.红宝石, new PropItem(PropItems_Enum.红宝石, "稀有物品红宝石", ItemType.Material, 5, 600, "红宝石"));

        Props.Add(PropItems_Enum.普通钥匙, new PropItem(PropItems_Enum.普通钥匙, "普通钥匙，可能是某个平民掉落", ItemType.Material, 20, 10, "普通钥匙"));

        Props.Add(PropItems_Enum.贵重的钥匙, new PropItem(PropItems_Enum.贵重的钥匙, "贵重钥匙，可以开启某些特殊门", ItemType.Material, 10, 50, "贵重的钥匙"));

        Props.Add(PropItems_Enum.铁矿石, new PropItem(PropItems_Enum.铁矿石, "铁矿石，用于制作武器", ItemType.Material, 50, 40, "铁矿石"));
        #endregion

        #region  添加道具

        Props.Add(PropItems_Enum.原素瓶, new PropItem(PropItems_Enum.原素瓶, "恢复50点生命值", ItemType.Consumable, 10, 120, "原素瓶"));

        Props.Add(PropItems_Enum.女神的祝福, new PropItem(PropItems_Enum.女神的祝福, "恢复全部生命值", ItemType.Consumable, 1, 400, "女神的祝福"));

        Props.Add(PropItems_Enum.原素灰瓶, new PropItem(PropItems_Enum.原素灰瓶, "恢复50点魔法值", ItemType.Consumable, 10, 120, "原素灰瓶"));

        Props.Add(PropItems_Enum.秘藏的祝福, new PropItem(PropItems_Enum.秘藏的祝福, "恢复全部魔法值", ItemType.Consumable, 1, 400, "秘藏的祝福"));

        Props.Add(PropItems_Enum.苔藓球果实, new PropItem(PropItems_Enum.苔藓球果实, "召唤一只绿色的随从", ItemType.Consumable, 10, 200, "苔藓球果实"));

        Props.Add(PropItems_Enum.红虫药丸, new PropItem(PropItems_Enum.红虫药丸, "召唤一只红色的随从", ItemType.Consumable, 1, 300, "红虫药丸"));

        Props.Add(PropItems_Enum.返回骨片, new PropItem(PropItems_Enum.返回骨片, "能返回出生地", ItemType.Consumable, 10, 50, "返回骨片"));

        Props.Add(PropItems_Enum.诱敌头盖骨, new PropItem(PropItems_Enum.诱敌头盖骨, "召唤一只紫色的随从", ItemType.Consumable, 10, 200, "诱敌头盖骨"));

        Props.Add(PropItems_Enum.锈斑硬币, new PropItem(PropItems_Enum.锈斑硬币, "获得少量的经验", ItemType.Consumable, 10, 100, "锈斑硬币"));

        Props.Add(PropItems_Enum.锈斑金币, new PropItem(PropItems_Enum.锈斑金币, "获得大量的经验", ItemType.Consumable, 5, 200, "锈斑金币"));

        Props.Add(PropItems_Enum.七色石, new PropItem(PropItems_Enum.七色石, "放在地上会发出光辉，变成指标", ItemType.Consumable, 20, 10, "七色石"));
        #endregion

        #region 装备

        Props.Add(PropItems_Enum.伫立龙徽戒指, new EquipmentData(PropItems_Enum.伫立龙徽戒指,
            "其象征不朽古龙的徽印与龙学院相当匹配，也传达出“伫立的龙是魔法师的理想外在”的含义", ItemType.Equipment, 1, 300,
            "伫立龙徽戒指", 3, 0, 0, 0, 30, EquipmentData.EquipmentType.Ring, EquipmentData.PlayerType.a_Magician));

        Props.Add(PropItems_Enum.老者戒指, new EquipmentData(PropItems_Enum.老者戒指,
    "成为深渊监视者的法兰魔法师，必定是孤独的战士", ItemType.Equipment, 1, 300,
    "老者戒指", 3, 30, 0, 0, 0, EquipmentData.EquipmentType.Ring, EquipmentData.PlayerType.a_Magician));

        Props.Add(PropItems_Enum.老者大帽子, new EquipmentData(PropItems_Enum.老者大帽子,
    "以结晶老者身份闻名的孪生老师，将其面容完全掩盖住的巨大帽子", ItemType.Equipment, 1, 100,
    "老者大帽子", 1, 0, 5, 0, 10, EquipmentData.EquipmentType.Head, EquipmentData.PlayerType.a_Magician));

        Props.Add(PropItems_Enum.卡露拉三角帽子, new EquipmentData(PropItems_Enum.卡露拉三角帽子,
    "以泪石为名的珍贵大宝石戒指,传说那是女神夸特所流下的哀悼眼泪", ItemType.Equipment, 1, 100,
    "卡露拉三角帽子", 1, 0, 3, 0, 15, EquipmentData.EquipmentType.Head, EquipmentData.PlayerType.a_Magician));

        Props.Add(PropItems_Enum.魔法师手套, new EquipmentData(PropItems_Enum.魔法师手套,
    "罪业之都的宫廷魔法师穿戴的丝绸手套", ItemType.Equipment, 1, 200,
    "魔法师手套", 2, 10, 0, 0, 10, EquipmentData.EquipmentType.Hand, EquipmentData.PlayerType.a_Magician));

        Props.Add(PropItems_Enum.沙之咒术师手套, new EquipmentData(PropItems_Enum.沙之咒术师手套,
    "过去生息于堆土塔的沙之咒术师手套，据说深红色的薄布带有魔力", ItemType.Equipment, 1, 200,
    "沙之咒术师手套", 2, 7, 2, 0, 15, EquipmentData.EquipmentType.Hand, EquipmentData.PlayerType.a_Magician));

        Props.Add(PropItems_Enum.宫廷魔法师长袍, new EquipmentData(PropItems_Enum.宫廷魔法师长袍,
   "过去生息于堆土塔的沙之咒术师手套，据说深红色的薄布带有魔力", ItemType.Equipment, 1, 500,
   "宫廷魔法师长袍", 1, 0, 10, 20, 20, EquipmentData.EquipmentType.Chest, EquipmentData.PlayerType.a_Magician));

        Props.Add(PropItems_Enum.卡露拉长袍, new EquipmentData(PropItems_Enum.卡露拉长袍,
   "过去生息于堆土塔的沙之咒术师手套，据说深红色的薄布带有魔力", ItemType.Equipment, 1, 500,
   "卡露拉长袍", 1, 0, 7, 10, 30, EquipmentData.EquipmentType.Chest, EquipmentData.PlayerType.a_Magician));


        Props.Add(PropItems_Enum.宫廷魔法师长裤, new EquipmentData(PropItems_Enum.宫廷魔法师长裤,
  "罪业之都的宫廷魔法师穿戴的靴子", ItemType.Equipment, 1, 300,
  "宫廷魔法师长裤", 1, 0, 5, 10, 5, EquipmentData.EquipmentType.Leg, EquipmentData.PlayerType.a_Magician));

        Props.Add(PropItems_Enum.魔法师长裤, new EquipmentData(PropItems_Enum.魔法师长裤,
  "彼海姆龙学院的魔法师旅行服装，以黑色皮革长裤与耐穿的长靴组合而成", ItemType.Equipment, 1, 300,
  "魔法师长裤", 1, 0, 3, 5, 10, EquipmentData.EquipmentType.Leg, EquipmentData.PlayerType.a_Magician));
        #endregion
    }
}
