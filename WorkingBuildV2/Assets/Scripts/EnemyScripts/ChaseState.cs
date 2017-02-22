using UnityEngine;
using System.Collections;

// Chase player
public class ChaseState : InterfaceEnemyState {

    private readonly StatePatternEnemy enemy;

    public ChaseState(StatePatternEnemy statePatternEnemy) //Constructor
    {
        enemy = statePatternEnemy;
    }

    public void Update()
    {
        Look();
        Chase();
    }

    private void Look() //Looks for player and detects if it is hit with the enemy eyes
    {
        RaycastHit hit;
        //Vector3 enemyToTarget = ((enemy.chaseTarget.position + enemy.offset) - enemy.eyes.transform.position);
        ////direction from the eyes to the target

        if (Physics.Raycast(enemy.eyes.transform.position, enemy.eyes.transform.forward, out hit, enemy.sightRange, 9)
            && hit.collider.CompareTag("Player")) {
            if (Vector3.Distance(hit.transform.position, enemy.transform.position) <= enemy.killDist) {
                hit.transform.gameObject.GetComponent<PlayerMovement>().KillPlayer();
                
                enemy.transform.LookAt(hit.transform);
                enemy.playerSelection.RemovePlayers();
                ToAttackState();
            }
            enemy.chaseTarget = hit.transform;
        }
        else if (enemy.navMeshAgent.remainingDistance <= 1) //If didnt find anything
        {
            ToAlertState();
        }
    }

    private void Chase() {
        enemy.meshRendererFlag.material.color = Color.red;
        enemy.navMeshAgent.destination = enemy.chaseTarget.position;
        enemy.navMeshAgent.Resume();

    }

    public void OnTriggerEnter(Collider other)
    {
        // if the enemy hits the player, don't we want to attack?
        if (other.gameObject.CompareTag("Player")) {
            other.GetComponent<PlayerMovement>().KillPlayer();
            ToAttackState();
        }
    }

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
        enemy.navMeshAgent.Stop();
        enemy.currentState = enemy.attackState;
        enemy.myAnimator.Play("Feast");
    }

    
}
