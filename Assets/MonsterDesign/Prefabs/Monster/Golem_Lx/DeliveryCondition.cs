using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCondition : MonoBehaviour
{
    //起点位置
    Vector3 StartPoint;
    //终点位置
    public Vector3 EndPoint;
    //看向的目标
    public GameObject Target;
    public bool isArrived;
    void OnEnable()
    {
        isArrived = false;
      //  StartPoint = new Vector3(-184f, 7f, 162f);
       // transform.position = StartPoint;
    }
    // Update is called once per frame
    void Update()
    {
        if (isArrived)
        {
            return;
        }
        //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(Target.transform.position - transform.position), 10f * Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position, EndPoint, 0.02f);
        if (Vector3.Distance(transform.position,EndPoint)<2f)
        {
            isArrived = true;
        }
    }
}
