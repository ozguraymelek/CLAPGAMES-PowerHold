using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

#region Enums

public enum EnemyTypes
{
    CarrotType1 = 1,
    CarrotType2 = 2,
    CarrotBoss = 3,
    
    TomatoType1 = 4,
    TomatoType2 = 5,
    TomatoBoss = 6,
    
    HamburgerType1 = 7,
    HamburgerType2 = 8,
    HamburgerBoss = 9,
    
    HotdogType1 = 10,
    HotdogType2 = 11,
    HotdogType3 = 12
}

#endregion

public abstract class Enemy : MonoBehaviour, IEnemy,  IInteractible
{
    [Header("Scriptable Objects Reference")] [SerializeField]
    protected PlayerSettings playerSettings;
    
    [Header("Reference")] [Space]
    [SerializeField] protected EnemyTypes enemyType;
    [SerializeField] protected INVBehaviour invBehaviour;

    [Header("Components")] protected TMP_Text _textLevel;
    
    [Header("Settings")] [Space]
    public GameObject activeEnemy;
    public bool interacted = false;
    
    [Header("Enemy Level Settings")] 
    public int type1_Level;
    public int type2_Level;
    public int boss_Level;

    private void Start()
    {
        // SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        
    }

    #region Abstract

    #region Enemy Abstract Functions
    
    /// <summary>
    ///     implemented IEnemy Interface
    /// </summary>
    /// <param name="enemyType"></param>
    public abstract void EnemyLevel(EnemyTypes enemyType);

    public abstract void SetTextLevel(EnemyTypes enemyType);

    public abstract EnemyTypes SetActiveEnemy();

    public abstract void ExploitPlayerLevel();
    
    #endregion

    #region Interaction Functions
    
    public abstract void OnEnemyInteractWithPlayer(Collider collider);
    public abstract void OnEnemyStillInteractWithPlayer(Collider collider);
    public abstract void OnEnemyNotInteractWitPlayerAnymore(Collider collider);
    
    #endregion

    #region Implemented Abstract Functions - Implemented IInteractible Interface
    
    public abstract void OnEnter(Collider collider);

    public abstract void OnStay(Collider collider);

    public abstract void OnExit(Collider collider);
    

    #endregion
    
    #endregion

    
    
  
}
