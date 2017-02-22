using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class ChangeSceneManager : MonoBehaviour {

    GameObject[] GOpauseMenu;
    GameObject[] GOutility;
    public static ChangeSceneManager Instance;
    public AudioSource menuExit;
    public AudioSource button;
    public AudioSource hover;
    public AudioSource TitleMusic;
    // Use this for initialization
    void Start () {
        AudioListener.volume=1.0f;
        TitleMusic.Play();
        // Make sure game is running at start
        Time.timeScale = 1;
        // Collection of all the game objects used for the pause menu
        GOpauseMenu = GameObject.FindGameObjectsWithTag("PauseMenu");
        GOutility = GameObject.FindGameObjectsWithTag("Utility");
        HidePauseMenu();
    }
	
	// Update is called once per frame
	void Update () {
        // Press the escape button to pause the game
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name != "Title") {
            menuExit.Play();
            // Check to see if game is unpaused
            if (Time.timeScale == 1) {
                // Pause game time
                Time.timeScale = 0;
                ShowPauseMenu();
            // Check to see if game is paused
            } else if (Time.timeScale == 0) {
                // Unpause game time
                Time.timeScale = 1;
                HidePauseMenu();
            }
        }
	}

    void HidePauseMenu () {
        foreach (GameObject g in GOpauseMenu) {
            g.SetActive(false);
        }

        foreach(GameObject g in GOutility) {
            g.SetActive(true);
        }
    }

    void ShowPauseMenu () {
        foreach (GameObject g in GOpauseMenu) {
            g.SetActive(true);
        }

        foreach (GameObject g in GOutility) {
            g.SetActive(false);
        }
    }

    public void ReloadGame () {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadLevel (string name) {
        Time.timeScale = 1;
        SceneManager.LoadScene(name);
    }

    public void QuitGame () {
        Application.Quit();
    }

    public void ButtonSound()
    {
        button.Play();
    }

    public void HoverSound()
    {
        hover.Play();
    }
    public void LevelMusic()
    {
        TitleMusic.Play();
    }
}

