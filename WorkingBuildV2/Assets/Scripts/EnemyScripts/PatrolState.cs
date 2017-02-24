using UnityEngine;
using System.Collections;
using System;

// Patrol predefined area
public class PatrolState : InterfaceEnemyState {
    private readonly StatePatternEnemy enemy;
    private int nextWayPoint;
    private Vector3 targetDir;

    public PatrolState( StatePatternEnemy statePatternEnemy ) //Constructor
    {
        enemy = statePatternEnemy;
    }

    public void Update() {
        //Look();
        Patrol();
    }

    /*======================State Functions======================*/

    //private void Look() //Looks for player directly in front of enemy and detects if it is hit with ray from enemy eyes
    //{
    //    RaycastHit hit;

    //    if (Physics.Raycast(enemy.eyes.transform.position, enemy.eyes.transform.forward, out hit, enemy.sightRange, 9)
    //        && hit.collider.CompareTag("Player")) {
    //        enemy.chaseTarget = hit.transform;
    //        // check to see if hit.transform is close enough to attack
    //        // if so attack player
    //        if (Vector3.Distance(hit.transform.position, enemy.transform.position) <= enemy.killDist) {
    //            hit.transform.gameObject.GetComponent<PlayerMovement>().KillPlayer();

    //            enemy.transform.LookAt(hit.transform);
    //            enemy.playerSelection.RemovePlayers();
    //            ToAttackState();
    //        }
    //        // else chase seen player
    //        else ToChaseState();
    //    }
    //}

    void Patrol() //Patrols the different waypoints
    {
        enemy.meshRendererFlag.material.color = Color.green;
        // show that enemy has not spotted player
        enemy.navMeshAgent.destination = enemy.wayPoints[nextWayPoint].position;
        // set destination of enemy navmesh
        enemy.navMeshAgent.Resume();
        // move the enemy player with the navmesh

        if ( enemy.navMeshAgent.remainingDistance <= enemy.navMeshAgent.stoppingDistance
            && !enemy.navMeshAgent.pathPending ) {
            // check to see if enemy is close to its destination and has another point to follow
            nextWayPoint = (nextWayPoint + 1) % enemy.wayPoints.Length;
            // iterate through waypoint positions
        }
    }

    /*======================Collision/Trigger======================*/

    public void OnTriggerEnter( Collider other ) //detects if player is hit
    {
        // if the enemy hits the player, don't we want to attack?
        if ( other.gameObject.CompareTag("Player") ) {
            other.GetComponent<PlayerMovement>().KillPlayer();
            ToAttackState();
        }
    }

    // ON TRIGGER STAY, NOT COLLISION, NEED FIXING
    void InterfaceEnemyState.OnTriggerStay( Collider other ) {
        Debug.Log("Kill me please");
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
                } else { // Else chase seen player
                    ToChaseState();
                }
            }
        }
    }

    /*======================Switch State======================*/

    public void ToPatrolState() // do nothing
    {
        Debug.Log("Can't transition to same state");
    }

    public void ToAlertState() //  serch for player
    {
        enemy.currentState = enemy.alertState;
        enemy.myAnimator.Play("Walk");
    }

    public void ToChaseState() // chase after player
    {
        enemy.myAnimator.Play("Run");
        enemy.currentState = enemy.chaseState;
        enemy.myAnimator.Play("Run");
    }

    public void ToAttackState() {
        enemy.playerSelection.RemovePlayers();
        enemy.navMeshAgent.ResetPath();
        enemy.currentState = enemy.attackState;
        enemy.myAnimator.Play("Feast");
    }
}
