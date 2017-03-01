using UnityEngine;
using System.Collections;

public interface InterfaceEnemyState {
    void Update();

    void OnTriggerStay( Collider other );

    void ToPatrolState();

    void ToAlertState();

    void ToChaseState();

    void ToAttackState();
}
