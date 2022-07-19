using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class INVBehaviour : MonoBehaviour
{
    [Header("Components")] [Space] private CapsuleCollider _capsuleCollider;
    public Animator animator;
    
    [Header("Settings")] [Space]
    public float playerSpeed;
    public float sensitivity;
    [SerializeField] private float boundX;
    [SerializeField] private bool isPressing = false;
    private float _screenWidthMultiplier;
    [SerializeField] private float rotateDuration;

    private void Start()
    {
        SubscribeEvents();
        GetComponents();

        _screenWidthMultiplier = 1.0f / Screen.width;
    }

    private void OnDestroy()
    {
        UnsubscribeEvents();
    }

    private void SubscribeEvents()
    {
        INVEvents.OnStart += OnStart;
        INVEvents.OnInteractWithEnemy += OnInteractWithEnemy;
        INVEvents.OnJump += OnJump;
        INVEvents.OnHold += OnHold;
        INVEvents.OnRelease += OnRelease;

        INVInputs.PointerMoved += OnPointerMoved;
        INVInputs.PointerPressed += OnPointerPressed;
        INVInputs.PointerRemoved += OnPointerRemoved;
    }

    private void OnPointerRemoved(Vector3 obj)
    {
        isPressing = false;
    }

    private void OnPointerPressed(Vector3 obj)
    {
        isPressing = true;
        OnHold();
    }

    private void OnPointerMoved(Vector3 mouseMovementDirection)
    {
        
    }

    private void OnRelease()
    {
        
    }

    private void OnHold()
    {
        animator.SetTrigger("Attack1");
    }

    private void OnJump()
    {
        
    }

    //TODO: karakterimizin leveli yükselecek ve düşmanlar ölecek.
    private void OnInteractWithEnemy()
    {
        
    }

    private void OnStart()
    {
        INVEvents.OnUpdate += MoveForward;
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

    private void BasicMoveX(Vector3 mouseMovementDirection)
    {
        Vector3 mouseToWorldDirection = new Vector3(mouseMovementDirection.x * _screenWidthMultiplier, 0f, 0f);
        Vector3 addVector = mouseToWorldDirection * Time.deltaTime * sensitivity;

        transform.position += addVector;

        Transform thisTransform = transform;

        if (thisTransform.position.x < -boundX)
            thisTransform.position = new Vector3(-boundX, thisTransform.position.y, thisTransform.position.z);
        
        if (thisTransform.position.x > boundX)
            thisTransform.position = new Vector3(boundX, thisTransform.position.y, thisTransform.position.z);
    }

    public void PlayerTurnToForwardAxis()
    {
        transform.root.DORotate(Vector3.zero, rotateDuration).OnComplete(() =>
        {
            playerSpeed = 5;
            animator.SetBool("IsStarted", true);
        });
    }

    #endregion
}
