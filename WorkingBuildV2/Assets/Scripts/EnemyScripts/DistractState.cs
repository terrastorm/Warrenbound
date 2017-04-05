using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistractState : InterfaceEnemyState {

    private readonly StatePatternEnemy enemy;
    private float distractTimer;

    public DistractState( StatePatternEnemy statePatternEnemy ) //Constructor
    {
        enemy = statePatternEnemy;
    }
	
	public void Update () {
        Distract();
    }

    /*======================State Functions======================*/

    void Distract() {
        enemy.meshRendererFlag.material.color = Color.red;
        distractTimer += Time.deltaTime;

        if ( distractTimer >= enemy.distractDuration ) {
            enemy.chaseTarget = null;
            enemy.navMeshAgent.ResetPath();
            ToAlertState();
        }
    }

    /*======================Collision/Trigger======================*/

    public void OnTriggerStay(Collider other) {

    }

    /*======================Switch State======================*/

    public void ToPatrolState() // player was not seen, continue patrolling area
    {
    }

    public void ToAlertState() // do nothing
    {
        enemy.currentState = enemy.alertState;
        enemy.myAnimator.Play("Walk");
    }

    public void ToChaseState() // player seen, chase player
    {
    }

    public void ToAttackState() {
    }

    public void ToDistractState() {

    }
}
