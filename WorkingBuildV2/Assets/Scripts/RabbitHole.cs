using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RabbitHole : MonoBehaviour {

    private PlayerSelection playerSelection;

    public int rabbitTally;


	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this.gameObject);
        playerSelection = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerSelection>();
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

    void OnLevelWasLoaded(int Level)
    {
        //if(Level == 2)
        //{
        //    playerSelection = null;
        //    GameObject.Find("Canvas").GetComponent<EndLevelScript>().endTally = rabbitTally;
        //    if (rabbitTally > 0)
        //        GameObject.Find("Congrats").GetComponent<Text>().text = "Congratulations";
        //    else
        //        GameObject.Find("Congrats").GetComponent<Text>().text = "Try Again";
        //}
    }

}
