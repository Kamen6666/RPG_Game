using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Attract : MonoBehaviour
{
    private Animator animator;
    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void OnEnable()
    {
        animator.SetTrigger("Victory");
    }
    void OnMouseDown()
    {
        animator.SetTrigger("Attack");
    }

}
