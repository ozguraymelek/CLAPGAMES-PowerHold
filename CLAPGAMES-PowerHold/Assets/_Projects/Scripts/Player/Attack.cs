using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [Header("References")] [SerializeField]
    private INVBehaviour invBehaviour;

    [Header("Settings")] 
    public float timer;
    public float attackRate;

    private void Update()
    {
        timer += Time.deltaTime;
        
    }

    public void PlayerAttack()
    {
        invBehaviour.animator.SetTrigger("Attack1");
        
    }
}
