using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T: Singleton<T>
{
    private static Singleton<T> instance;

    private void Awake()
    {
        instance = this;
    }

    public static T Instance
    {
        get => (T)instance;
    }
}
