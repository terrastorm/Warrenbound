using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RabbitHole : MonoBehaviour {

    private PlayerSelection playerSelection;

    public int rabbitTally;

    public static RabbitHole instance = null;


	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this.gameObject);
        playerSelection = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerSelection>();

        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            rabbitTally += 1;
            col.tag = "Survived";
            playerSelection.RemovePlayers();
            Destroy(col.gameObject);
        }
    }

    void OnLevelWasLoaded()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            transform.position = new Vector3(62, 2, 179);
        }
    }

}
