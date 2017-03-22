using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour {
    public UnityEngine.AI.NavMeshAgent navMeshAgent;
    public AudioSource SneakSound;
    public AudioSource RunSound;
    public AudioSource DeathSound;
    public AudioSource FeastSound;

    [HideInInspector]
    public RaycastHit dest; // Destination that units will follow

    public Animator myAnimator;
    [HideInInspector]
    public bool stopMovement = false;


    // Use this for initialization
    void Start () {
        // Get reference for areas that units can travel
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        // Stopping the rabbits from moving
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance &&
            (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f) && stopMovement)
        {
            myAnimator.Play("Idle");    // Start playing idle animation
            stopMovement = false;       // Stop this block of code from running until needed again
            SneakSound.Stop();
        }

        // if (Input.GetMouseButtonDown(1)) {
        // navMeshAgent.speed = 4;
        // navMeshAgent.acceleration = 8;
        // PlaySound(SneakSound);


    }

    // Used to start moving rabbits toward destination when set with PlayerSelection script
    public void MoveUnit() {
        myAnimator.Play("Sneak");               // Start playing sneak animation
        PlaySound(SneakSound);
        navMeshAgent.destination = dest.point;  // Set destination that rabbit will follow
        stopMovement = true;
    }

    //public void MoveUnitSprint() {
        //PlaySound(RunSound);
       // navMeshAgent.speed = 8;
       // navMeshAgent.acceleration = 8;
        //navMeshAgent.destination = dest.point;
        //navMeshAgent.Resume();
       // myAnimator.Play("Sprint");
   // }


    public void KillPlayer() {
        navMeshAgent.velocity = Vector3.zero;
        navMeshAgent.ResetPath();
        myAnimator.Play("Death");
        PlaySound(DeathSound);
        PlaySound(FeastSound);
        transform.tag = "Dead";
        transform.Find("Selected marker").gameObject.SetActive(false);
        GetComponent<BoxCollider>().isTrigger = true;
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        GetComponent<PlayerMovement>().enabled = false;
    }

    //==============================HIDING==============================
    void OnTriggerEnter( Collider col ) {
        if ( col.transform.tag == "Bush" ) {
            navMeshAgent.ResetPath();
            gameObject.layer = 8;
            Debug.Log(gameObject.layer);
        }
    }

    void OnTriggerExit( Collider col ) {
        if ( col.transform.tag == "Bush" ) {
            gameObject.layer = 1;
        }
    }

    //==============================SOUND==============================
    public void PlaySound (AudioSource sound)
    {
        sound.Play();
    }
}
