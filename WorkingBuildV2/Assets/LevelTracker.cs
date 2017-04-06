using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTracker : MonoBehaviour {

    public int lastScene;
    public Scene currentScene;

	// Use this for initialization
	void Start () {
        if (SceneManager.GetActiveScene().buildIndex != 0 || SceneManager.GetActiveScene().buildIndex != 4) //if the scene is not the title or end screen
            lastScene = (SceneManager.GetActiveScene().buildIndex);
    }

    void Awake() {
        DontDestroyOnLoad(transform.gameObject);
    }

    // Update is called once per frame
    void Update () {

        if (Input.GetKey(KeyCode.R)){
            currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.buildIndex);
        }
        //Debug.Log(currentScene.buildIndex);
    }
}
