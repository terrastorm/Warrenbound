using UnityEngine;
using System.Collections;

// Search for players near enemy
public class AlertState : InterfaceEnemyState {

    private readonly StatePatternEnemy enemy;
    private float searchTimer;

    public AlertState(StatePatternEnemy statePatternEnemy) //Constructor
    {
        enemy = statePatternEnemy;
    }

    public void Update()
    {
        Look();
        Search();
    }

    private void Look() //Looks for player directly in front of enemy and detects if it is hit with ray from enemy eyes
    {
        RaycastHit hit;

        if (Physics.Raycast(enemy.eyes.transform.position, enemy.eyes.transform.forward, out hit, enemy.sightRange, 9)
            && hit.collider.CompareTag("Player")) {
            enemy.chaseTarget = hit.transform;
            // check to see if hit.transform is close enough to attack
            // if so attack player
            if (Vector3.Distance(hit.transform.position, enemy.transform.position) <= enemy.killDist) {
                hit.transform.gameObject.GetComponent<PlayerMovement>().KillPlayer();

                enemy.transform.LookAt(hit.transform);
                enemy.playerSelection.RemovePlayers();
                ToAttackState();
            }
            // else chase seen player
            ToChaseState();
        }

    }

    private void Search() {
        enemy.meshRendererFlag.material.color = Color.yellow;
        enemy.navMeshAgent.Stop();
        enemy.transform.Rotate(0, enemy.searchingTurnSpeed * Time.deltaTime, 0);
        searchTimer += Time.deltaTime;

        if (searchTimer >= enemy.searchingDuration) {
            ToPatrolState();
        }
    }

    public void OnTriggerEnter(Collider other) // is this pointless?
    {
        // if the enemy hits the player, don't we want to attack?
        if (other.gameObject.CompareTag("Player")) {
            other.GetComponent<PlayerMovement>().KillPlayer();
            ToAttackState();
        }
    }

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
        enemy.navMeshAgent.Stop();
        enemy.currentState = enemy.attackState;
        searchTimer = 0f; //Saw the player and reset the timer
        enemy.myAnimator.Play("Feast");
    }
}
