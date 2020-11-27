using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBall : MonoBehaviour
{
    //设置最大半径
    public float MaxRadius;
    //碰撞体
    private SphereCollider BallCollider;
    //特效粒子控制器
    private ParticleSystem _particleSystem;
    void Awake()
    {
        //获取组件
        BallCollider = GetComponent<SphereCollider>();
        _particleSystem = transform.GetChild(0).GetComponent<ParticleSystem>();
        MaxRadius = 8f;
    }
    void Start()
    {
        //初始化检测器半径
        BallCollider.radius = 0f;
        //设置子物体初始大小
        transform.GetChild(0).localScale = Vector3.one * MaxRadius * 0.5f;
        //启动协成
        StartCoroutine(AttackEnemy());
    }

    IEnumerator AttackEnemy()
    {
        //循环次数
        int count = 5;
        //启动前延迟
        yield return new WaitForSeconds(0.5f);
        while (count-- > 0)
        {
            //展开特效
            _particleSystem.Play();
            //吸怪
            BallCollider.radius = MaxRadius;
            yield return new WaitForSeconds(0.5f);
            //关闭特效
            _particleSystem.Stop();
            //重置碰撞体位置
            BallCollider.radius = 0f;
            yield return new WaitForSeconds(1f);
        }
        Destroy(gameObject);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.transform.position = transform.position;
        }
    }
}
