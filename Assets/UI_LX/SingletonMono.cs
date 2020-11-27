using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonMono<T> : MonoBehaviour where T:MonoBehaviour
{
    private static T instance;
   
    public static T Instance
    {
        get
        {
            if (instance == null)
            { 
                //Debug.Log(typeof(T).Name);
                Instantiate(Resources.Load<GameObject>("Prefab/" + typeof(T).Name));
            }
            return instance;
        }

    }

    protected virtual void Awake()
    {
        instance = this as T;
    }
}
