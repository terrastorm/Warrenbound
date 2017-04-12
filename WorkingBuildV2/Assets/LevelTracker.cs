using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTracker : MonoBehaviour {

    public int lastScene = 0;
    Scene currentScene;

    public static LevelTracker instance = null;

    void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(this);
    }

	// Use this for initialization
	void Start () {
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKey(KeyCode.R)){
            currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.buildIndex);
        }
        //Debug.Log(currentScene.buildIndex);
    }

    void OnLevelWasLoaded () {
        if (SceneManager.GetActiveScene().buildIndex != 0 && SceneManager.GetActiveScene().buildIndex != 4) { //if the scene is not the title or end screen
            Debug.Log(SceneManager.GetActiveScene().buildIndex);
            lastScene = SceneManager.GetActiveScene().buildIndex;
            Debug.Log(name + " " + lastScene);
        }
    }
}
