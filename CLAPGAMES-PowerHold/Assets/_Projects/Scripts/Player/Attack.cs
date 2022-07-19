using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [Header("References")] [SerializeField]
    private INVBehaviour invBehaviour;

    [Header("Settings")] 
    [SerializeField] private float attackRate;
    [SerializeField] private float timer;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= attackRate)
        {
            PlayerAttack();
            timer = 0;
        }
    }

    private void PlayerAttack()
    {
        invBehaviour.animator.SetTrigger("Attack1");
        
    }
}
