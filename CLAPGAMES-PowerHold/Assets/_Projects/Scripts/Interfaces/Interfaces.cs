using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    void EnemyLevel(EnemyTypes enemyType);
}

public interface ICancut
{
    void Slice(Collider other);
}
public interface IInteractible
{
    void OnEnter(Collider other);
    void OnStay(Collider other);
    void OnExit(Collider collider);
}
