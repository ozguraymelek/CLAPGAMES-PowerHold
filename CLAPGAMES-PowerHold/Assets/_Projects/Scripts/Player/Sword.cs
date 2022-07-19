using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour,ISword
{
    [Header("Settings")] public bool interacted = false;
    private void OnTriggerEnter(Collider other)
    {
        OnEnter(other);
    }

    public void OnEnter(Collider collider)
    {
        if (interacted == true) return;
        
        if (collider.GetComponent<IEnemy>() != null)
        {
            print("Enemy!");
            interacted = true;
        }
    }

    public void OnStay()
    {
        
    }

    public void OnExit()
    {
        
    }
}
