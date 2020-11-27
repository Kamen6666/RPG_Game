using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRiding : MonoBehaviour
{
    public GameObject houseprefabs;
    private Animator hourseanim;
    private PlayerController pc;
    private Rigidbody rd;
    void Start()
    {
        pc = GetComponent<PlayerController>();
        rd = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)&&!pc.isRiding)
        {
            //关闭重力
            rd.useGravity = false;
            //关闭角色控制器
            pc.enabled = false;
            //修改骑马开关
            pc.isRiding = true;
            //创建坐骑
            transform.parent = Instantiate(houseprefabs, transform.position, transform.rotation).transform;

            hourseanim = transform.parent.GetComponent<Animator>();
            //打开开关
            pc.isRiding = true;
            //调整玩家位置
            transform.localPosition += new Vector3(0, 25f, 0f);
            //上调速度
            PlayerManager.instance.speed *= 5;
        }

        if (!pc.enabled && pc.isRiding)
        {
            //骑马控制开启
            HourseMove();
            //骑马旋转
            Player();
            if(isRotate)
                RotateView();
        }
    }
    private void HourseMove()
    {
        float v = Input.GetAxisRaw("Vertical");
        if (v == 0)
        {
            hourseanim.SetFloat("Speed_f", 0f);
        }
        else
        {
            hourseanim.SetFloat("Speed_f", 1f);
        }
    }
    private bool isRotate;
    //旋转jues
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
    void RotateView()
    {
        //获取鼠标在水平方向的滑动
        float mx = Input.GetAxis("Mouse X");
        //获取鼠标在垂直方向的滑动
        float my = Input.GetAxis("Mouse Y");
        transform.parent.RotateAround(transform.parent.position, Vector3.up, mx * Time.deltaTime * 90f);
    }
}
