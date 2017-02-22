using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerCount : MonoBehaviour {

    //private GameObject[] playerArray;
    public int playerTotalCount;
    public int playerCurrentCount;

    public Text countText;

    // Use this for initialization

    void Start() {
        playerTotalCount = GameObject.FindGameObjectsWithTag("Player").Length;
        playerCurrentCount = playerTotalCount;

        SetCountText();
    }
	
	// Update is called once per frame
	void Update () {
        playerCurrentCount = GameObject.FindGameObjectsWithTag("Player").Length;
        SetCountText();
        if (playerCurrentCount <= 0) {
            SceneManager.LoadScene("EndLevel");
        }
    }

    void SetCountText()
    {
        countText.text = playerCurrentCount.ToString() + " / " + playerTotalCount.ToString();
    }
}




