using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform taget;
    public Vector3 dis;
    public Vector3 mousePoint;
    void Start()
    {
        //寻找玩家
        taget = GameObject.FindGameObjectWithTag(GameConst.PLAYER_TAG).transform;
        //获取玩家方向
        dis = taget.forward;
        //设置初始位置
        float x = (taget.position + Vector3.Project(taget.forward, dis) * 9f).x;
        float y = 9f;
        float z = (taget.position + Vector3.Project(taget.forward, dis) * 9f).z;
        transform.position = new Vector3(x, y, z);
        transform.LookAt(taget);
        //看向玩家
        transform.LookAt(taget);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            isRotate = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            isRotate = false;
        }
        if(isRotate)
            RotateView();

        followPlayer();
    }
    private bool isRotate;
    void RotateView()
    {
        //获取鼠标在水平方向的滑动
       float mx =  Input.GetAxis("Mouse X");
        //获取鼠标在垂直方向的滑动
       float my =  Input.GetAxis("Mouse Y");

        transform.RotateAround(taget.position, Vector3.up, mx * Time.deltaTime * 90f);
        //transform.eulerAngles = Vector3.Lerp(transform.position, taget.position - transform.position, 90f);
    }
    void followPlayer()
    {
        //设置初始位置
        float x = (taget.position + dis * 9f).x;
        float y = 9f;
        float z = (taget.position + dis * 9f).z;
        transform.position = new Vector3(x, y, z);
        transform.LookAt(taget);
    }
}
