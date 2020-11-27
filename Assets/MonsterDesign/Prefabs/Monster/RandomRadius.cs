using UnityEngine;

public class RandomRadius : MonoBehaviour
{
    /// <summary>
    /// 圆 范围生成
    /// </summary>
    /// <param name="resNumber">生成数量</param>
    /// <param name="resRadius">大圆外圈半径</param>
    /// <param name="noResRadius">内圆半径（不生成半径）</param>
    /// <param name="prefabs">要生成的物体</param>
    /// <param name="trans">transform</param>
    public RandomRadius(int resNumber,int resRadius,int noResRadius,GameObject prefabs,Transform trans)
    {
        for (int i = 0; i < resNumber; i++)
        {
            Vector2 p = Random.insideUnitCircle * resRadius;
            Vector2 pos = p.normalized * (noResRadius + p.magnitude);
            Vector3 pos1 = new Vector3(trans.position.x + pos.x, 0, trans.position.z + pos.y);
            //Vector3 pos2 =new Vector3(pos.x, 0, pos.y);
           
            Instantiate(prefabs, pos1, Quaternion.identity);
        }
    }
}