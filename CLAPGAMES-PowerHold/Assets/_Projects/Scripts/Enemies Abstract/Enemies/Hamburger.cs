using TMPro;
using UnityEngine;

public class Hamburger : Enemy
{
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
    
    public override void EnemyLevel(EnemyTypes enemyType)
    {
        switch (SetActiveEnemy())
        {
            case EnemyTypes.HamburgerType1:
                SetTextLevel(enemyType);
                break;
            case EnemyTypes.HamburgerType2:
                SetTextLevel(enemyType);
                break;
            case EnemyTypes.HamburgerBoss:
                SetTextLevel(enemyType);
                break;
        }
    }

    public override void SetTextLevel(EnemyTypes enemyType)
    {
        switch (enemyType)
        {
            case EnemyTypes.HamburgerType1:
                _textLevel.text = type1_Level.ToString();
                break;
            case EnemyTypes.HamburgerType2:
                _textLevel.text = type2_Level.ToString();
                break;
            case EnemyTypes.HamburgerBoss:
                _textLevel.text = boss_Level.ToString();
                break;
        }
    }

    public override EnemyTypes SetActiveEnemy()
    {
        if (transform.GetChild(0).gameObject.activeInHierarchy)
        {
            activeEnemy = transform.GetChild(0).gameObject;
            enemyType = (EnemyTypes)7;
        }
        else if (transform.GetChild(1).gameObject.activeInHierarchy)
        {
            activeEnemy = transform.GetChild(1).gameObject;
            enemyType = (EnemyTypes)8;
        }
        
        else if (transform.GetChild(2).gameObject.activeInHierarchy)
        {
            activeEnemy = transform.GetChild(2).gameObject;
            enemyType = (EnemyTypes)9;
        }

        return enemyType;
    }

    public override void ExploitPlayerLevel()
    {
        switch (SetActiveEnemy())
        {
            case EnemyTypes.HamburgerType1:
                invBehaviour.Text_PopUp_Minus(this);
                playerSettings.playerLevel -= type1_Level;
                invBehaviour.text_level.text = $"LEVEL  " + playerSettings.playerLevel;
                break;
            case EnemyTypes.HamburgerType2:
                invBehaviour.Text_PopUp_Minus(this);
                playerSettings.playerLevel -= type2_Level;
                invBehaviour.text_level.text = $"LEVEL  " + playerSettings.playerLevel;
                break;
            case EnemyTypes.HamburgerBoss:
                invBehaviour.Text_PopUp_Minus(this);
                playerSettings.playerLevel -= boss_Level;
                invBehaviour.text_level.text = $"LEVEL  " + playerSettings.playerLevel;
                break;
        }
    }

    #region Overrided Abstract Functions

    public override void OnEnter(Collider collider)
    {
        // if (collider.GetComponentInChildren<Sword>().interacted == true) return;
        
        if (collider.GetComponent<INVBehaviour>() != null)
        {
            print("Player !");

            ExploitPlayerLevel();
            invBehaviour.InteractWithEnemy();
            interacted = true;
        }
    }

    public override void OnStay(Collider collider)
    {
        if (collider.GetComponent<INVBehaviour>() != null)
        {
            if (playerSettings.playerLevel < 1)
            {
                //Player Dead
                FindObjectOfType<INVEvents>().OnPlayerFail();
            }
        }
    }

    public override void OnExit(Collider collider)
    {
        
    }

    #endregion

    #region Interaction Overrided
    
    public override void OnEnemyInteractWithPlayer(Collider collider)
    {
        OnEnter(collider);
    }
    
    public override void OnEnemyStillInteractWithPlayer(Collider collider)
    {
        OnStay(collider);
    }
    
    public override void OnEnemyNotInteractWitPlayerAnymore(Collider collider)
    {
        OnExit(collider);
    }
    
    #endregion

    #region Interaction Triggers

    private void OnTriggerEnter(Collider other)
    {
        OnEnemyInteractWithPlayer(other);
    }

    private void OnTriggerStay(Collider other)
    {
        OnEnemyStillInteractWithPlayer(other);
    }

    private void OnTriggerExit(Collider other)
    {
        OnEnemyNotInteractWitPlayerAnymore(other);
    }

    #endregion
}
