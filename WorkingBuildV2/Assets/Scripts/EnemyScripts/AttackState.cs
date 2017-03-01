using UnityEngine;
using System.Collections;
using System;

public class AttackState : InterfaceEnemyState {
    private readonly StatePatternEnemy enemy;
    private float eatTimer;

    public AttackState( StatePatternEnemy statePatternEnemy ) //Constructor
    {
        enemy = statePatternEnemy;
    }

    public void Update() {
        Eat();
    }

    /*======================State Functions======================*/

    public void Eat() {
        enemy.navMeshAgent.ResetPath();
        eatTimer += Time.deltaTime;

        if ( eatTimer >= enemy.eatDuration ) {
            ToAlertState();
        }
    }

    /*======================Collision/Trigger======================*/

    public void OnTriggerEnter( Collider other ) //detects if player is hit
    {

    }

    public void OnTriggerStay( Collider other ) {
    }

    /*======================Switch State======================*/

    public void ToPatrolState() // do nothing
    {

    }

    public void ToAlertState() //  serch for player
    {
        eatTimer = 0;
        enemy.currentState = enemy.alertState;
        enemy.myAnimator.Play("Walk");
    }

    public void ToChaseState() // chase after player
    {

    }

    public void ToAttackState() {

    }
}
