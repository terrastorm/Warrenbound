using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour {
    public UnityEngine.AI.NavMeshAgent navMeshAgent;
    public AudioSource SneakSound;
    public AudioSource RunSound;
    public AudioSource DeathSound;

    [HideInInspector]
    public RaycastHit dest; // Destination that units will follow

    public Animator myAnimator;
    [HideInInspector]
    public bool stopMovement = false;

    private bool hiding = false;


    // Use this for initialization
    void Start () {
        // Get reference for areas that units can travel
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    // Update is called once per frame
    void Update () {
        // Stopping the rabbits from moving
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance &&
            (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f) && stopMovement) {
            myAnimator.Play("Idle");    // Start playing idle animation
            stopMovement = false;       // Stop this block of code from running until needed again
            SneakSound.Stop();
        }

        //if (Input.GetMouseButtonDown(1)) { 
        //    navMeshAgent.speed = 4;
        //    navMeshAgent.acceleration = 8;
        //    SneakMusic();
        //}
    }

    // Used to start moving rabbits toward destination when set with PlayerSelection script
    public void MoveUnit() {
        if (hiding) {
            gameObject.layer = 0;
        }
        myAnimator.Play("Sneak");               // Start playing sneak animation
        PlaySound(SneakSound);
        navMeshAgent.destination = dest.point;  // Set destination that rabbit will follow
        navMeshAgent.Resume();                  // Start moving the rabbit towards the point
        stopMovement = true;
    }

    //public void MoveUnitSprint() {
    //    RunMusic();
    //    navMeshAgent.speed = 8;
    //    navMeshAgent.acceleration = 8;
    //    navMeshAgent.destination = dest.point;
    //    navMeshAgent.Resume();
    //    myAnimator.Play("Sprint");
    //}

    public void KillPlayer() {
        navMeshAgent.velocity = Vector3.zero;
        navMeshAgent.Stop();
        myAnimator.Play("Death");
        transform.tag = "Dead";
        transform.Find("Selected marker").gameObject.SetActive(false);
        GetComponent<BoxCollider>().isTrigger = true;
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        GetComponent<PlayerMovement>().enabled = false;
    }

    void OnCollisionEnter (Collision col) {
        //if (col.transform.tag == "Bush") {
        //    navMeshAgent.velocity = Vector3.zero;
        //    navMeshAgent.Stop();
        //    gameObject.layer = 2;
        //    hiding = true;
        //    Debug.Log(gameObject.layer);
        //}
    }

    //public void SneakMusic () {
    //    SneakSound.Play();
    //}
    public void RunMusic () {
        RunSound.Play();
    }
    public void DeathMusic () {
        DeathSound.Play();
    }

    public void PlaySound (AudioSource sound)
    {
        sound.Play();
    }
}
