using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum EnemyType
{
    Type1 = 1,
    Type2 = 2,
    Type3 = 3
}

public class Enemy : MonoBehaviour, IEnemy,IInteractible
{
    [Header("Scriptable Objects Reference")] [SerializeField]
    private PlayerSettings playerSettings;
    
    [Header("Reference")] [Space] [SerializeField]
    private EnemyType enemyType;
    [SerializeField] private INVBehaviour invBehaviour;

    [Header("Components")] private TMP_Text _textLevel;
    
    [Header("Settings")] [Space] public GameObject activeEnemy;
    [SerializeField] private int enemyLevel;
    public bool interacted = false;

    [Header("Enemy Level Settings")] 
    public int type1_Level;
    public int type2_Level;
    public int type3_Level;

    private void Start()
    {
        SubscribeEvents();
        SetActiveEnemy();
        GetComponents();
        EnemyLevel(enemyType);
    }

    private void GetComponents()
    {
        _textLevel = activeEnemy.GetComponentInChildren<TMP_Text>();
    }
    private void SubscribeEvents()
    {
        
    }

    public EnemyType SetActiveEnemy()
    {
        if (transform.GetChild(0).gameObject.activeInHierarchy)
        {
            activeEnemy = transform.GetChild(0).gameObject;
            enemyType = (EnemyType)1;
        }
        else if (transform.GetChild(1).gameObject.activeInHierarchy)
        {
            activeEnemy = transform.GetChild(1).gameObject;
            enemyType = (EnemyType)2;
        }
        
        else if (transform.GetChild(2).gameObject.activeInHierarchy)
        {
            activeEnemy = transform.GetChild(2).gameObject;
            enemyType = (EnemyType)3;
        }

        return enemyType;
    }

    private void SetTextLevel(EnemyType enemyType)
    {
        switch (enemyType)
        {
            case EnemyType.Type1:
                _textLevel.text = type1_Level.ToString();
                break;
            case EnemyType.Type2:
                _textLevel.text = type2_Level.ToString();
                break;
            case EnemyType.Type3:
                _textLevel.text = type3_Level.ToString();
                break;
        }
    }

    public void EnemyLevel(EnemyType enemyType)
    {
        switch (SetActiveEnemy())
        {
            case EnemyType.Type1:
                SetTextLevel(enemyType);
                break;
            case EnemyType.Type2:
                SetTextLevel(enemyType);
                break;
            case EnemyType.Type3:
                SetTextLevel(enemyType);
                break;
        }
    }

    public void ExploitPlayerLevel()
    {
        switch (SetActiveEnemy())
        {
            case EnemyType.Type1:
                playerSettings.playerLevel -= type1_Level;
                invBehaviour.text_level.text = $"LEVEL  " + playerSettings.playerLevel;
                break;
            case EnemyType.Type2:
                playerSettings.playerLevel -= type2_Level;
                invBehaviour.text_level.text = $"LEVEL  " + playerSettings.playerLevel;
                break;
            case EnemyType.Type3:
                playerSettings.playerLevel -= type3_Level;
                invBehaviour.text_level.text = $"LEVEL  " + playerSettings.playerLevel;
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (interacted == true) return;
        
        if (other.GetComponent<INVBehaviour>() != null)
        {
            print("Player !");
            ExploitPlayerLevel();
            interacted = true;
        }

        OnPlayerInteractWithEnemy(other);
    }

    private void OnPlayerInteractWithEnemy(Collider collider)
    {
        OnEnter(collider);
    }
    
    public void OnEnter(Collider collider)
    {
        
        
    }

    public void OnStay()
    {
        
    }

    public void OnExit()
    {
        
    }
}
