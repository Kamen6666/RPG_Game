using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuWu : MonoBehaviour
{
    public int Atk;
    private ParticleSystem particle;
    private MeshRenderer mesh;
    private SphereCollider collider;
    private GameObject playerPos;
    private float speed = 10;
    private float verticalSpeed;
    private float g = 9.8f;
    private float time = 0;

    private void Awake()
    {
        collider = GetComponent<SphereCollider>();

        mesh = GetComponent<MeshRenderer>();
        particle = transform.GetChild(0).GetComponent<ParticleSystem>();

    }


    private void Start()
    {
        playerPos = GameObject.FindWithTag("Player");

        float tmepDistance = Vector3.Distance(transform.position, playerPos.transform.position);
        float tempTime = tmepDistance / speed;
        float riseTime, downTime;
        riseTime = downTime = tempTime / 2;
        verticalSpeed = g * riseTime;
        transform.LookAt(playerPos.transform);

    }
    private void Update()
    {

        time += Time.deltaTime;
        float test = verticalSpeed - g * time;
        transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
        transform.Translate(transform.up * test * Time.deltaTime*3, Space.World);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Plane"))
        {
            mesh.enabled = false;
            collider.radius = 2;
            //掉落到地面上隐藏mesh
            //mesh.enabled = false;
            //播放粒子特效
            particle.Play();
            //销毁物体
            Destroy(gameObject, 5);
            enabled = false;
        }
       
    }
    private float timeCount;
    private void OnTriggerStay(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            timeCount += Time.deltaTime;
            if (timeCount>=1)
            {
                timeCount = 0;
                PlayerManager.instance.CrtHp -= Atk;
            }
            
        }
    }
    private void OnCollisionEnter(Collision collision)
    {

        //掉落到地面上隐藏mesh
        //mesh.enabled = false;
        //播放粒子特效
        //particle.Play();
        //销毁物体
        //Destroy(gameObject, 5);
       // enabled = false;

    }
}
