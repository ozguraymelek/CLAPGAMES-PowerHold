using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    void EnemyLevel(EnemyType enemyType);
}

public interface ISword
{
    void OnEnter();
    void OnStay();
    void OnExit();
}
