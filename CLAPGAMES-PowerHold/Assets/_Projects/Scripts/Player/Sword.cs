using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour,ISword
{
    private void OnTriggerEnter(Collider other)
    {
        OnEnter();
    }

    public void OnEnter()
    {
        
    }

    public void OnStay()
    {
        
    }

    public void OnExit()
    {
        
    }
}
