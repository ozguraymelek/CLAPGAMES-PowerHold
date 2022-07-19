using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    void EnemyLevel(EnemyType enemyType);
}

public interface ISword
{
    void OnEnter(Collider collider);
    void OnStay();
    void OnExit();
}
