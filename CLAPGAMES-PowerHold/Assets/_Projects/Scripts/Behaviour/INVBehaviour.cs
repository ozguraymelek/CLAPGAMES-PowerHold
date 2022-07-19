using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class INVBehaviour : MonoBehaviour
{
    [Header("Scriptable Objects Reference")] [SerializeField]
    private PlayerSettings playerSettings;
    
    [Space]
    [Header("Reference")] [SerializeField] private Attack attack;
    [SerializeField] private Sword sword;
    
    [Header("Components")] [Space] private CapsuleCollider _capsuleCollider;
    public Animator animator;
    [SerializeField] private TMP_Text text_level;
    [SerializeField] private TMP_Text text_PopUp;
    
    [Header("Settings")] [Space]
    public float playerSpeed;
    public float sensitivity;
    [SerializeField] private float boundX;
    public bool isPressing = false;
    private float _screenWidthMultiplier;
    [SerializeField] private float rotateDuration;
    public bool canAttack = false;

    private void Start()
    {
        SetPlayerLevel2Start();
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
        if (canAttack)
        {
            if (attack.timer >= attack.attackRate)
            {
                attack.PlayerAttack();
                attack.timer = 0;
            }
        }
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
        text_level.transform.rotation = Quaternion.Euler(0f,-transform.rotation.y,0f);
        text_PopUp.transform.rotation = Quaternion.Euler(0f, -transform.rotation.y, 0f);
        
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
            // text_level.transform.rotation = Quaternion.Euler(0f,-transform.rotation.y,0f);
            playerSpeed = 5;
            animator.SetBool("IsStarted", true);
            StartCoroutine(AttackRate());
        });
    }

    private IEnumerator AttackRate()
    {
        yield return new WaitForSeconds(1f);
        canAttack = true;
    }

    #region Animation Events - [Sword Turn 1]
    
    public void EnableSwordCollider()
    {
        sword.boxCollider.enabled = true;
    }

    public void DisableSwordCollider()
    {
        sword.boxCollider.enabled = false;
    }
    
    #endregion
    
    
    public void SetPlayerLevel2Start()
    {
        text_level.text = $"LEVEL  " + playerSettings.playerLevel;
    }

    public void ExploitEnemyLevel(Enemy enemy)
    {
        switch (enemy.SetActiveEnemy())
        {
            case EnemyType.Type1:
                Text_PopUp(enemy);
                playerSettings.playerLevel += enemy.type1_Level;
                text_level.text = $"LEVEL  " + playerSettings.playerLevel;
                break;
            case EnemyType.Type2:
                Text_PopUp(enemy);
                playerSettings.playerLevel += enemy.type2_Level;
                text_level.text = $"LEVEL  " + playerSettings.playerLevel;
                break;
            case EnemyType.Type3:
                Text_PopUp(enemy);
                playerSettings.playerLevel += enemy.type3_Level;
                text_level.text = $"LEVEL  " + playerSettings.playerLevel;
                break;
        }
    }
    #endregion

    #region Pop Ups

    public void Text_PopUp(Enemy enemy)
    {
        switch (enemy.SetActiveEnemy())
        {
            case EnemyType.Type1:
                text_PopUp.text = $"+" + enemy.type1_Level;
                break;
            case EnemyType.Type2:
                text_PopUp.text = $"+" + enemy.type2_Level;
                break;
            case EnemyType.Type3:
                text_PopUp.text = $"+" + enemy.type3_Level;
                break;
        }

        text_PopUp.transform.DOScale(Vector3.one, 1.5f).OnComplete(() =>
        {
            text_PopUp.transform.DOLocalMoveY(.9f, 1f);
            
            text_PopUp.transform.DOPunchScale(Vector3.one*.5f, 1f).OnComplete(() =>
            {
                text_PopUp.DOColor(Color.green, .3f).OnComplete(() =>
                {
                    text_PopUp.text = null;
                    text_PopUp.transform.localScale = Vector3.zero;
                });
            });
        });
        
        
    }
    

    #endregion
}
