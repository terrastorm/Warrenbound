using UnityEngine;
using System.Collections;
using System;

// Chase player
public class ChaseState : InterfaceEnemyState {
    
    private readonly StatePatternEnemy enemy;
    private Vector3 targetDir;

    public ChaseState( StatePatternEnemy statePatternEnemy ) //Constructor
    {
        enemy = statePatternEnemy;
    }

    public void Update() {
        Look();
        Chase();
    }

    /*======================State Functions======================*/

    private void Look() //Looks for player and detects if it is hit with the enemy eyes
    {
        RaycastHit hit;
        //Vector3 enemyToTarget = ((enemy.chaseTarget.position + enemy.offset) - enemy.eyes.transform.position);
        ////direction from the eyes to the target

        if ( Physics.Raycast(enemy.eyes.transform.position, enemy.eyes.transform.forward, out hit, enemy.sightRange, 9)
            && hit.collider.CompareTag("Player") ) {
            if ( Vector3.Distance(hit.transform.position, enemy.transform.position) <= enemy.killDist ) {
                hit.transform.gameObject.GetComponent<PlayerMovement>().KillPlayer();

                enemy.transform.LookAt(hit.transform);
                enemy.playerSelection.RemovePlayers();
                ToAttackState();
            }
            enemy.chaseTarget = hit.transform;
        } else if ( enemy.navMeshAgent.remainingDistance <= 1 ) //If didnt find anything
          {
            ToAlertState();
        }
    }

    private void Chase() {
        enemy.meshRendererFlag.material.color = Color.red;
        enemy.navMeshAgent.destination = enemy.chaseTarget.position;
        enemy.navMeshAgent.Resume();

    }

    /*======================Collision/Trigger======================*/

    public void OnTriggerEnter( Collider other ) {

    }

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
                    }
                }
            }
        }
    }

    /*======================Switch State======================*/

    public void ToPatrolState() // do nothing
    {

    }

    public void ToAlertState() // lost sight of player, start searching again
    {
        enemy.currentState = enemy.alertState;
        enemy.myAnimator.Play("Walk");
    }

    public void ToChaseState() // do nothing
    {

    }

    public void ToAttackState() {
        enemy.navMeshAgent.ResetPath();
        enemy.currentState = enemy.attackState;
        enemy.myAnimator.Play("Feast");
    }
}
