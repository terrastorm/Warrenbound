using UnityEngine;
using System.Collections;

public class AttackState: InterfaceEnemyState {
    private readonly StatePatternEnemy enemy;
    private float eatTimer;

    public AttackState(StatePatternEnemy statePatternEnemy) //Constructor
    {
        enemy = statePatternEnemy;
    }

    public void Update() {
        Eat();
    }

    public void Eat() {
        enemy.navMeshAgent.Stop();
        eatTimer += Time.deltaTime;

        if (eatTimer >= enemy.eatDuration) {
            ToAlertState();
        }
    }

    public void OnTriggerEnter(Collider other) //detects if player is hit
    {

    }

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
