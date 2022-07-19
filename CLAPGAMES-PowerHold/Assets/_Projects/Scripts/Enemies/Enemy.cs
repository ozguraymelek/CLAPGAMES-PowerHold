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

public class Enemy : MonoBehaviour, IEnemy
{
    [Header("Reference")] [Space] [SerializeField]
    private EnemyType enemyType;

    [Header("Components")] private TMP_Text _textLevel;
    
    [Header("Settings")] [Space] public GameObject activeEnemy;
    [SerializeField] private int enemyLevel;

    [Header("Enemy Level Settings")] 
    [SerializeField] private int type1_Level;
    [SerializeField] private int type2_Level;
    [SerializeField] private int type3_Level;

    private void Start()
    {
        SetActiveEnemy();
        GetComponents();
        EnemyLevel(enemyType);
    }

    private void GetComponents()
    {
        _textLevel = activeEnemy.GetComponentInChildren<TMP_Text>();
    }
    
    private EnemyType SetActiveEnemy()
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
    
}
