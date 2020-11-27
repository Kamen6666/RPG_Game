
using UnityEngine;



public static class GameConst
{

    #region 标签
    public const string PLAYER_TAG = "Player";
    public const string MONSTER_TAG = "Monster";
    public const string BOSS_TAG = "Boss";
    #endregion
    #region UI参数
    public static bool Draging = false;
    #endregion
    #region 按键
    public const string HORIZONTAL = "Horizontal";
    public const string VERTICAL = "Vertical";
    public const string RUN = "Run";
    //public const string JUMP = "Jump";
    public const string DEFEND = "Defend";
    public const string ATTACK = "Attcak";
   // public const string SKILL01 = "Skill01";
   // public const string SKILL02 = "Skill02";
    public const string CASTSPELL = "CastSpell";
    #endregion

    #region 怪物数据
    //yoyou多少生命值  变狂暴改变颜色
    public const int MONSTER_YOYO_BOSS_KUANGBAO = 100;
    //yoyou多少生命值  生成小怪物
    public const int MONSTER_YOYO_BOSS_RESSAMLL = 60;
    //yoyou 变狂暴之后  改变速度
    public const float MONSTER_YOYO_BOSS_SPEEDUP = 1.5f;
    #endregion

    #region 怪物加载路径
    //yoyo本体
    public const string MONSTER_YOYO_YOYO = "Monster/YoYo/YoYo_Boss_Res";
    //yoyo boss
    public const string MONSTER_YOYO_YOYO_BOSS = "Monster/YoYo/YoYo_Boss_Res";
    //yoyo boss产生小怪
    public const string MONSTER_YOYO_YOYO_RES = "Monster/YoYo/YoYo_Boss_Res";

    #endregion
    #region 怪物材质球加载

    //yoyo绿色
    public const string MONSTER_YOYO_MAT_GREEN = "Monster/YoYo/Toon Ghost-Green";
    //yoyo橘色
    public const string MONSTER_YOYO_MAT_ORANGE = "Monster/YoYo/Toon Ghost-Orange";
    //yoyo蓝色
    public const string MONSTER_YOYO_MAT_BLUE = "Monster/YoYo/Toon Ghost-Blue";

    #endregion
    #region 动画参数
    public static int PLAYER_ANIMATOR_PARA_ATTACK;
    public static int PLAYER_ANIMATOR_PARA_RUN;
    public static int PLAYER_ANIMATOR_PARA_WALK;
    public static int PLAYER_ANIMATOR_PARA_DEFEND;
    //public static int PLAYER_ANIMATOR_PARA_JUMP;
    public static int PLAYER_ANIMATOR_PARA_TAKEDAMAGE;
    public static int PLAYER_ANIMATOR_PARA_DIE;
    public static int PLAYER_ANIMATOR_PARA_VICTORY;
    public static int PLAYER_ANIMATOR_PARA_UPSET;
    //public static int PLAYER_ANIMATOR_PARA_JUMPATTACK;
    public static int PLAYER_ANIMATOR_PARA_CASTSPELL;
    #endregion

    static GameConst()
    {
        PLAYER_ANIMATOR_PARA_ATTACK = Animator.StringToHash("Attack");//普通攻击
        PLAYER_ANIMATOR_PARA_RUN = Animator.StringToHash("Run");//加速跑
        PLAYER_ANIMATOR_PARA_WALK = Animator.StringToHash("Walk");
        PLAYER_ANIMATOR_PARA_DEFEND = Animator.StringToHash("Defend");//格挡
        //PLAYER_ANIMATOR_PARA_JUMP = Animator.StringToHash("Jump");//跳跃
        PLAYER_ANIMATOR_PARA_TAKEDAMAGE = Animator.StringToHash("TakeDamage");//挨打
        PLAYER_ANIMATOR_PARA_DIE = Animator.StringToHash("Die");//死亡
        PLAYER_ANIMATOR_PARA_VICTORY = Animator.StringToHash("Victory");//胜利
        PLAYER_ANIMATOR_PARA_UPSET = Animator.StringToHash("Upset");//失败

    }
        
}
