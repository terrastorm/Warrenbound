using UnityEngine;
using System.Collections;

public class StatePatternEnemy : MonoBehaviour {

    public float searchingTurnSpeed = 120f;             // How fast enemy can turn around while patrolling
    public float searchingDuration = 4f;                // Time before enemy stops searching for player and patrols again
    public float killDist;                              // Distance player can be before they are killed by wolf
    public float eatDuration = 3f;                      // Time before enemy goes back to searching after attacking player
    public float sightRange = 20f;                      // How far enemy can see in front of them
    public Transform[] wayPoints;                       // Points to patrol
    public Transform eyes;                              // Point where raycast starts
    public Vector3 offset = new Vector3(0, 0.5f, 0);    // Distance between eyes and player height
    public MeshRenderer meshRendererFlag;               // Object to show current state of enemy
    public Animator myAnimator;                         // Animator for wolf, used for playing animations
    public AudioSource Feast;                           //Used to play the feasting noise
    public AudioSource ChaseMusic;                      //used to play the chase music

    [HideInInspector] public Transform chaseTarget;                 // Player object to chase
    [HideInInspector] public InterfaceEnemyState currentState;      // Keep track of what enemy is currently doing
    [HideInInspector] public ChaseState chaseState;                 // Functions to chase player
    [HideInInspector] public AlertState alertState;                 // Functions to look for player
    [HideInInspector] public PatrolState patrolState;               // Functions to patrol area
    [HideInInspector] public AttackState attackState;               // Functions for attacking player
    [HideInInspector] public UnityEngine.AI.NavMeshAgent navMeshAgent;             // Move the enemy
    [HideInInspector] public PlayerSelection playerSelection;       // Delete rabbits from list when dead

    private void Awake()
    {
        chaseState = new ChaseState(this);
        alertState = new AlertState(this);
        patrolState = new PatrolState(this);
        attackState = new AttackState(this);
        playerSelection = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerSelection>();
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    // Use this for initialization
    void Start ()
    {
        currentState = patrolState;
        myAnimator.Play("Walk");
	}
	
	// Update is called once per frame
	void Update ()
    {
        currentState.Update();
	}
    
    private void OnTriggerEnter(Collider other)
    {
        currentState.OnTriggerEnter(other);
    }

    /*public void Feasting() 
    {
        Feast.Play();
    }
    public void PanicMusic()
    {
        ChaseMusic.Play();
   }*/
}
