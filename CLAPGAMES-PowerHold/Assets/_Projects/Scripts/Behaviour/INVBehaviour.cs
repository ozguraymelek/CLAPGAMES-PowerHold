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
    public TMP_Text text_level;
    public TMP_Text text_PopUp;
    public GameObject playerCanvas;
    
    [Header("Settings")] [Space]
    public float playerSpeed;
    public bool isPlayerDead = false;
    public float sensitivity;
    [SerializeField] private float boundX;
    public bool isPressing = false;
    private float _screenWidthMultiplier;
    [SerializeField] private float rotateDuration;
    public bool canAttack = false;

    [Header("Settings Interaction")] public float speedDecreaseFactor;
    public float speedIncreaseFactor;

    [Header("Lists")] [SerializeField] private List<Enemy> enemies=new List<Enemy>();
    
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

        sword.boxCollider.enabled = false;

        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1f);
        Sword.Instance.interacted = false;
        Cut.Instance.cuttedd = false;
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
                sword.boxCollider.enabled = true;
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
        
        print("2");
        sword.boxCollider.enabled = true;
    }

    public void DisableSwordCollider()
    {
        print("1");
        sword.boxCollider.enabled = false;
        // sword.interacted = false;
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
            // CARROT
            case EnemyTypes.CarrotType1:
                Text_PopUp_Plus(enemy);
                playerSettings.playerLevel += enemy.type1_Level;
                text_level.text = $"LEVEL  " + playerSettings.playerLevel;
                break;
            case EnemyTypes.CarrotType2:
                Text_PopUp_Plus(enemy);
                playerSettings.playerLevel += enemy.type2_Level;
                text_level.text = $"LEVEL  " + playerSettings.playerLevel;
                break;
            case EnemyTypes.CarrotBoss:
                Text_PopUp_Plus(enemy);
                playerSettings.playerLevel += enemy.boss_Level;
                text_level.text = $"LEVEL  " + playerSettings.playerLevel;
                break;
            
            // TOMATO
            case EnemyTypes.TomatoType1:
                Text_PopUp_Plus(enemy);
                playerSettings.playerLevel += enemy.type1_Level;
                text_level.text = $"LEVEL  " + playerSettings.playerLevel;
                break;
            case EnemyTypes.TomatoType2:
                Text_PopUp_Plus(enemy);
                playerSettings.playerLevel += enemy.type2_Level;
                text_level.text = $"LEVEL  " + playerSettings.playerLevel;
                break;
            case EnemyTypes.TomatoBoss:
                Text_PopUp_Plus(enemy);
                playerSettings.playerLevel += enemy.boss_Level;
                text_level.text = $"LEVEL  " + playerSettings.playerLevel;
                break;
            
            // HAMBURGER
            case EnemyTypes.HamburgerType1:
                Text_PopUp_Plus(enemy);
                playerSettings.playerLevel += enemy.type1_Level;
                text_level.text = $"LEVEL  " + playerSettings.playerLevel;
                break;
            case EnemyTypes.HamburgerType2:
                Text_PopUp_Plus(enemy);
                playerSettings.playerLevel += enemy.type2_Level;
                text_level.text = $"LEVEL  " + playerSettings.playerLevel;
                break;
            case EnemyTypes.HamburgerBoss:
                Text_PopUp_Plus(enemy);
                playerSettings.playerLevel += enemy.boss_Level;
                text_level.text = $"LEVEL  " + playerSettings.playerLevel;
                break;
            
            // HOTDOG
            case EnemyTypes.HotdogType1:
                Text_PopUp_Plus(enemy);
                playerSettings.playerLevel += enemy.type1_Level;
                text_level.text = $"LEVEL  " + playerSettings.playerLevel;
                break;
            case EnemyTypes.HotdogType2:
                Text_PopUp_Plus(enemy);
                playerSettings.playerLevel += enemy.type2_Level;
                text_level.text = $"LEVEL  " + playerSettings.playerLevel;
                break;
            case EnemyTypes.HotdogBoss:
                Text_PopUp_Plus(enemy);
                playerSettings.playerLevel += enemy.boss_Level;
                text_level.text = $"LEVEL  " + playerSettings.playerLevel;
                break;
        }
    }
    #endregion

    #region Pop Ups

    public void Text_PopUp_Plus(Enemy enemy)
    {
        switch (enemy.SetActiveEnemy())
        {
            // CARROT
            case EnemyTypes.CarrotType1:
                text_PopUp.text = $"+" + enemy.type1_Level;
                break;
            case EnemyTypes.CarrotType2:
                text_PopUp.text = $"+" + enemy.type2_Level;
                break;
            case EnemyTypes.CarrotBoss:
                text_PopUp.text = $"+" + enemy.boss_Level;
                break;
            
            // TOMATO
            case EnemyTypes.TomatoType1:
                text_PopUp.text = $"+" + enemy.type1_Level;
                break;
            case EnemyTypes.TomatoType2:
                text_PopUp.text = $"+" + enemy.type2_Level;
                break;
            case EnemyTypes.TomatoBoss:
                text_PopUp.text = $"+" + enemy.boss_Level;
                break;
            
            // HAMBURGER
            case EnemyTypes.HamburgerType1:
                text_PopUp.text = $"+" + enemy.type1_Level;
                break;
            case EnemyTypes.HamburgerType2:
                text_PopUp.text = $"+" + enemy.type2_Level;
                break;
            case EnemyTypes.HamburgerBoss:
                text_PopUp.text = $"+" + enemy.boss_Level;
                break;
            
            // HOTDOG
            case EnemyTypes.HotdogType1:
                text_PopUp.text = $"+" + enemy.type1_Level;
                break;
            case EnemyTypes.HotdogType2:
                text_PopUp.text = $"+" + enemy.type2_Level;
                break;
            case EnemyTypes.HotdogBoss:
                text_PopUp.text = $"+" + enemy.boss_Level;
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
                    
                    text_PopUp.transform.localPosition = new Vector3(0f, .5f, 0f);
                    text_PopUp.transform.localScale = Vector3.zero;
    
                    text_PopUp.color = Color.white;
    
                });
            });
        });
    }
    
    public void Text_PopUp_Minus(Enemy enemy)
    {
        switch (enemy.SetActiveEnemy())
        {
            // CARROT
            case EnemyTypes.CarrotType1:
                text_PopUp.text = $"-" + enemy.type1_Level;
                break;
            case EnemyTypes.CarrotType2:
                text_PopUp.text = $"-" + enemy.type2_Level;
                break;
            case EnemyTypes.CarrotBoss:
                text_PopUp.text = $"-" + enemy.boss_Level;
                break;
            
            // TOMATO
            case EnemyTypes.TomatoType1:
                text_PopUp.text = $"-" + enemy.type1_Level;
                break;
            case EnemyTypes.TomatoType2:
                text_PopUp.text = $"-" + enemy.type2_Level;
                break;
            case EnemyTypes.TomatoBoss:
                text_PopUp.text = $"-" + enemy.boss_Level;
                break;
            
            // HAMBURGER
            case EnemyTypes.HamburgerType1:
                text_PopUp.text = $"-" + enemy.type1_Level;
                break;
            case EnemyTypes.HamburgerType2:
                text_PopUp.text = $"-" + enemy.type2_Level;
                break;
            case EnemyTypes.HamburgerBoss:
                text_PopUp.text = $"-" + enemy.boss_Level;
                break;
            
            // HOTDOG
            case EnemyTypes.HotdogType1:
                text_PopUp.text = $"-" + enemy.type1_Level;
                break;
            case EnemyTypes.HotdogType2:
                text_PopUp.text = $"-" + enemy.type2_Level;
                break;
            case EnemyTypes.HotdogBoss:
                text_PopUp.text = $"-" + enemy.boss_Level;
                break;
        }
    
        text_PopUp.transform.DOScale(Vector3.one, .5f).OnComplete(() =>
        {
            text_PopUp.transform.DOLocalMoveY(.9f, .5f);
            
            text_PopUp.transform.DOPunchScale(Vector3.one*.5f, .5f).OnComplete(() =>
            {
                text_PopUp.DOColor(Color.red, .3f).OnComplete(() =>
                {
                    text_PopUp.text = null;
                    
                    text_PopUp.transform.localPosition = new Vector3(0f, .5f, 0f);
                    text_PopUp.transform.localScale = Vector3.zero;
    
                    text_PopUp.color = Color.white;
    
                });
            });
        });
    }
    
    #endregion

    #region Interact With Enemy

    public void InteractWithEnemy()
    {
        float _initSpeed = playerSpeed;

        StartCoroutine(Pushback(_initSpeed));
    }
    
    IEnumerator Pushback(float _initSpeed)
    {
        bool a = true;
        
        // sword.interacted = true;
        
        animator.SetTrigger("Hitted");
        
        while (a == true)
        {
            playerSpeed -= Time.deltaTime * speedDecreaseFactor;

            if (playerSpeed <= -_initSpeed)
            {
                StartCoroutine(Pushup(_initSpeed));

                a = false;
            }

            yield return null;
        }
    }

    IEnumerator Pushup(float _initSpeed)
    {
        bool a = true;

        while (a == true)
        {
            playerSpeed += Time.deltaTime * speedIncreaseFactor;

            if (playerSpeed >= _initSpeed)
            {
                playerSpeed = _initSpeed;
                a = false;
            }

            yield return null;
        }
    }

    #endregion

    #region Player Die

    public void DisablePlayerCollider()
    {
        _capsuleCollider.enabled = false;
    }

    public void DisableJSwordCollider()
    {
        sword.boxCollider.enabled = false;
    }

    public void UnsubscribeMoveForward()
    {
        INVEvents.OnUpdate -= MoveForward;
    }

    public void SetPlayer()
    {
        animator.SetTrigger("Die");
        isPlayerDead = true;
        playerSettings.playerLevel = 1;
        text_level.text = $"LEVEL " + playerSettings.playerLevel;
    }

    public void DeactivateAllWorldSpaceCanvas()
    {
        playerCanvas.SetActive(false);

        foreach (var enemy in enemies)
        {
            enemy.activeEnemy.GetComponentInChildren<Canvas>().gameObject.SetActive(false);
        }
    }
    #endregion
}
