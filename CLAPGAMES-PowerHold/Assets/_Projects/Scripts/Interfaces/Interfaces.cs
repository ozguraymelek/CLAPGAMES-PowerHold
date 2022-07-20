using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    void EnemyLevel(EnemyType enemyType);
}

public interface IInteractible
{
    void OnEnter(Collider collider);
    void OnStay(Collider collider);
    void OnExit(Collider collider);
}
