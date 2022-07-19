using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class INVBehaviour : MonoBehaviour
{
    [Header("Components")] [Space] private CapsuleCollider _capsuleCollider;
    
    [Header("Settings")] [Space]
    public float playerSpeed;
    public float sensitivity;
    [SerializeField] private float boundX;
    [SerializeField] private bool isPressing = false;

    private void Awake()
    {
        GetComponents();
        SubscribeEvents();
    }

    private void OnDestroy()
    {
        UnsubscribeEvents();
    }

    private void SubscribeEvents()
    {
        
    }
    
    private void UnsubscribeEvents()
    {
        
    }
    
    private void GetComponents()
    {
        _capsuleCollider = GetComponent<CapsuleCollider>();
    }

    #region Behavioral Functions

    private void MoveForward()
    {
        transform.position += Vector3.forward * Time.deltaTime * playerSpeed;
    }

    #endregion
}
