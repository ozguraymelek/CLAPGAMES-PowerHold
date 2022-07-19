using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour, IInteractible
{
    [Header("Scriptable Objects Reference")] [SerializeField]
    private PlayerSettings playerSettings;

    [Header("References")] [SerializeField]
    private INVBehaviour invBehaviour;
    
    [Header("Components")]
    public BoxCollider boxCollider;
    
    [Header("Settings")] public bool interacted = false;

    private void Start()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        INVEvents.OnInteractWithEnemy += OnInteractWithEnemy;
    }
    private void OnTriggerEnter(Collider other)
    {
        FindObjectOfType<INVEvents>().OnPlayerInteractWithEnemy(other);
    }

    public void OnInteractWithEnemy(Collider collider)
    {
        OnEnter(collider);
    }

    
    #region Implement
    
    public void OnEnter(Collider collider)
    {
        if (interacted == true) return;
        
        if (collider.GetComponent<IEnemy>() != null)
        {
            print("Enemy!");
            invBehaviour.ExploitEnemyLevel(collider.GetComponent<Enemy>());
            interacted = true;
        }
    }

    public void OnStay()
    {
        
    }

    public void OnExit()
    {
        
    }
    
    #endregion
    
}
