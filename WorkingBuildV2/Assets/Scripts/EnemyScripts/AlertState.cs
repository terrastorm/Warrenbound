using UnityEngine;
using System.Collections;
using System;

// Search for players near enemy
public class AlertState : InterfaceEnemyState {

    private readonly StatePatternEnemy enemy;
    private float searchTimer;
    private Vector3 targetDir;

    public AlertState( StatePatternEnemy statePatternEnemy ) //Constructor
    {
        enemy = statePatternEnemy;
    }

    public void Update() {
        Search();
    }

    /*======================State Functions======================*/

    private void Search() {
        enemy.meshRendererFlag.material.color = Color.yellow;
        enemy.navMeshAgent.ResetPath();
        searchTimer += Time.deltaTime;

        if ( searchTimer >= enemy.searchingDuration ) {
            ToPatrolState();
        }
    }

    /*======================Collision/Trigger======================*/

    // ON TRIGGER STAY, NOT COLLISION, NEED FIXING
    public void OnTriggerStay( Collider other ) {
        // Check to see if player is within view distance
        if ( other.gameObject.CompareTag("Player") ) {
            // Check if Player is not hiding
            if ( other.gameObject.layer != 8 ) {
                // Direction of player from enemy
                targetDir = other.transform.position - enemy.transform.position;
                // Check to see if player is within view range 
                if ( Vector3.Angle(targetDir, enemy.transform.forward) <= 75 ) {
                    enemy.chaseTarget = other.transform;

                    // Check to see if hit.transform is close enough to attack
                    // If so attack player
                    if ( Vector3.Distance(other.transform.position, enemy.transform.position) <= enemy.killDist ) {
                        other.transform.gameObject.GetComponent<PlayerMovement>().KillPlayer();
                        enemy.transform.LookAt(other.transform);
                        ToAttackState();
                    } else { // Else chase seen player
                    ToChaseState();
                    }
                }
            }
        }
    }

    /*======================Switch State======================*/

    public void ToPatrolState() // player was not seen, continue patrolling area
    {
        enemy.currentState = enemy.patrolState;
        searchTimer = 0f; //Saw the player and reset the timer
        enemy.myAnimator.Play("Walk");
    }

    public void ToAlertState() // do nothing
    {
        Debug.Log("Can't transition to same state");
    }

    public void ToChaseState() // player seen, chase player
    {
        enemy.currentState = enemy.chaseState;
        searchTimer = 0f; //Saw the player and reset the timer
        enemy.myAnimator.Play("Run");
    }

    public void ToAttackState() {
        enemy.playerSelection.RemovePlayers();
        enemy.navMeshAgent.ResetPath();
        enemy.currentState = enemy.attackState;
        searchTimer = 0f; //Saw the player and reset the timer
        enemy.myAnimator.Play("Feast");
    }
}
