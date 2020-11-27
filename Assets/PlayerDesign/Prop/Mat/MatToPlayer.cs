using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatToPlayer : MonoBehaviour
{
    //控制玩家是否可以吸取周围的金币
    private bool isMagnet = true;

    // Update is called once per frame
    void Update()
    {
        //如果玩家碰到吸铁石的话 就检测玩家周围的所有带有碰撞器的游戏对象
        if (isMagnet)
        {
            //检测以玩家为球心半径是5的范围内的所有的带有碰撞器的游戏对象
            Collider[] colliders = Physics.OverlapSphere(this.transform.position, 4);
            foreach (var item in colliders)
            {
                //如果是金币
                if (item.tag.Equals("Mat"))
                {
                    //让金币的开始移动
                    item.GetComponent<MatMoveController>().isCanMove = true;
                }
            }

        }
    }
}
