using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerCount : MonoBehaviour {

    //private GameObject[] playerArray;
    public int playerTotalCount;
    public int playerCurrentCount;
    public int playerGoal;

    public Text countText;
    public int rabbitTally;

    // Use this for initialization

    void Start() {
        playerTotalCount = GameObject.FindGameObjectsWithTag("Player").Length;
        playerCurrentCount = playerTotalCount;
        SetCountText();
    }
	
	// Update is called once per frame
	void Update () {
        rabbitTally = GameObject.Find("RabbitHoleCollider").GetComponent<RabbitHole>().rabbitTally;

        playerCurrentCount = GameObject.FindGameObjectsWithTag("Player").Length;

        SetCountText();

        //Controlls scene switching
        if (playerCurrentCount <= 0)
        {
            if (rabbitTally < playerGoal)
                SceneManager.LoadScene("EndLevel");
            else if (SceneManager.GetActiveScene().buildIndex == 1 && rabbitTally >= playerGoal)
                SceneManager.LoadScene("Level2");
            else if(SceneManager.GetActiveScene().buildIndex == 2 && rabbitTally >= playerGoal)
                SceneManager.LoadScene("BeatGame");
            //else if (SceneManager.GetActiveScene().buildIndex == 3 && rabbitTally >= playerGoal)
            //    SceneManager.LoadScene("BeatGame");

            GameObject.Find("RabbitHoleCollider").GetComponent<RabbitHole>().rabbitTally = 0;
        }
    }

    void SetCountText()
    {
        countText.text = playerCurrentCount.ToString() + " / " + playerTotalCount.ToString();
    }
}




