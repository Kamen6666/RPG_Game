using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (h != 0 || v != 0)
        {
            //输入方向
            Vector3 p_forward = new Vector3(h, 0, v);
            //修正方向
            Vector3 newPoint = Quaternion.AngleAxis(animator.transform.Find("Camera/Follow").eulerAngles.y, Vector3.up) * p_forward;
            /*
            //方向向量
            Vector3 dir = new Vector3(h, 0, v);
            //向量转换成四元数
            Quaternion targetdir = Quaternion.LookRotation(dir);
            //转过去
            animator.transform.rotation = Quaternion.Lerp(animator.transform.rotation, targetdir, 1f);
            */
            if (Input.GetKey(KeyCode.LeftShift))
            {
                //走路速度
                animator.transform.position += newPoint * PlayerManager.instance.speed * 0.5f * Time.deltaTime;
            }
            else
            {
                //跑步速度
                animator.transform.position += newPoint * PlayerManager.instance.speed * Time.deltaTime;
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
