using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public enum StateEnum { 站立, 移动, 攻击, 受伤, 死亡, 骑乘 };
public class PlayerController : MonoBehaviour
{
    //玩家状态
    public StateEnum P_state;
    //死亡画布
    public Image death_plane;
    //上马
    public bool isRiding;
    //鼠标射线碰撞物
    private RaycastHit MouseRayHit;
    //移动速度
    private float p_MoveSpeed;
    //跟随角色的摄像头
    private Camera MainCamera;
    //动画组件
    private Animator p_ain;
    //刚体组件
    private Rigidbody p_rd;
    //移动停止开关
    private bool isStop = false;
    //鼠标是否指向怪物
    private Transform tagetEnemy;
    void Start()
    {
        //初始速度
        p_MoveSpeed = 3f;
        //获取动画控制器
        p_ain = GetComponent<Animator>();
        p_rd = GetComponent<Rigidbody>();
        //初始化
        tagetEnemy = transform;
        //获取移动速度
        p_MoveSpeed = PlayerManager.instance.speed;
        //跟随摄像头
        MainCamera = transform.Find("Camera/Follow").GetComponent<Camera>();
    }
    //摄像头距离玩家的距离
    float nearPlayer; 
    void Update()
    {
        //镜头拉前拉后
        float msw = Input.GetAxisRaw("Mouse ScrollWheel");
        //Debug.Log(msw);
        if (msw != 0)
        {
            nearPlayer = MainCamera.transform.localPosition.y;
            nearPlayer = msw < 0 ? ++nearPlayer : --nearPlayer;
            nearPlayer = Mathf.Clamp(nearPlayer, 2, 10);
            if (nearPlayer > 9f)
            {
                MainCamera.transform.localPosition = new Vector3(0, nearPlayer, 0);
                //看向玩家
                MainCamera.transform.localEulerAngles = new Vector3(90, 0, 0);
            }
            else
            {
                MainCamera.transform.localPosition = new Vector3(0, nearPlayer, nearPlayer * (-1));
                MainCamera.transform.localEulerAngles = new Vector3(30, 0, 0);
            }
        }
        if (PlayerManager.instance.CrtHp == 0)
        {
            PlayerManager.instance.CrtMp = 0;
            if (P_state != StateEnum.死亡)
                PlayerDeath();
            return;
        }
        //方向移动
        Move();
        //是否旋转
        Player();
        
        if (isRotate)
            RotateView();
    }
    //方向移动
    private void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        if (h != 0 || v != 0)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                p_ain.SetBool(Animator.StringToHash("Run"), false);
            }
            else
            {
                p_ain.SetBool(Animator.StringToHash("Run"), true);
                p_ain.SetBool(Animator.StringToHash("Walk"), true);
            }
        }
        else
        {
            p_ain.SetBool(Animator.StringToHash("Walk"), false);
            p_ain.SetBool(Animator.StringToHash("Run"), false);
        }
    }
    //摄像头旋转开关
    private bool isRotate;
    //是否进入旋转状态判断
    private void Player()
    {
        if (Input.GetMouseButtonDown(1))
        {
            isRotate = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            isRotate = false;
        }
    }
    //旋转摄像头视角
    void RotateView()
    {
        //获取鼠标在水平方向的滑动
        float mx = Input.GetAxis("Mouse X");
        //摄像头旋转
        transform.RotateAround(transform.position, Vector3.up, mx * Time.deltaTime * 90f);
    }
    //受伤
    public void PlayerHurt(float power)
    {
        p_ain.SetTrigger("Take Damage");
        p_rd.velocity = (MainCamera.transform.position - transform.position).normalized * power;
        //p_rd.velocity = new Vector3(5, 5, 5);
    }
    //镜头抖动
    public void ShakeCamera()
    {
        StartCoroutine(shakecamera());
    }
    //抖动协成
    IEnumerator shakecamera()
    {
        int i = 8;
        while (--i > 0)
        {
            MainCamera.transform.localEulerAngles = new Vector3(30f, (i % 3) - 1, (i % 3) + 1);
            yield return new WaitForSeconds(0.1f);
        }
        MainCamera.transform.localEulerAngles = new Vector3(30f, 0f, 0f);
    }
    //死亡
    private void PlayerDeath()
    {
        //进入死亡状态
        P_state = StateEnum.死亡;
        //进入死亡动画
        p_ain.SetBool("Death", true);
        //关闭操作
        p_ain.SetBool(Animator.StringToHash("Walk"), false);
        p_ain.SetBool(Animator.StringToHash("Run"), false);
        StartCoroutine(DeathAnimation());
    }
    //死亡协成
    IEnumerator DeathAnimation()
    {
        yield return new WaitForSeconds(2f);
        float a = 0.3f;
        death_plane.color = new Color(0, 0, 0, a);
        death_plane.gameObject.SetActive(true);
        while (death_plane.color.a < 0.9f)
        {
            a = a + 0.05f;
            death_plane.color = new Color(0, 0, 0, a);
            yield return new WaitForSeconds(0.2f);
        }
        death_plane.transform.GetChild(0).gameObject.SetActive(true);
    }
    //复活回到出生点
    public void FUHUO()
    {
        //出生点
        transform.position = new Vector3(180, 1f, 190);
        transform.eulerAngles = new Vector3(0, 90, 0);
        //恢复状态
        PlayerManager.instance.CrtHp = PlayerManager.instance.maxHp;
        PlayerManager.instance.CrtMp = PlayerManager.instance.maxMp;
        //离开死亡状态
        P_state = StateEnum.站立;
        //离开死亡动画
        p_ain.SetBool("Death", false);
    }
}
