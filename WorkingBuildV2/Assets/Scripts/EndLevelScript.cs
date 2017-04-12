using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class EndLevelScript : MonoBehaviour
{
    public AudioSource EndlevelMusic;
    public AudioSource ButtonClick;
    public AudioSource ButtonHover;
    public Button MainMenu;
    public Button Exit;
    public Button Reload;
    public int endTally;
    public Text textEndTally;

    private LevelTracker levelTracker;

    // Use this for initialization
    public void Start()
    {
        textEndTally.text = "Score: " + endTally.ToString();
        AudioListener.volume = 1.0f;
        StartCoroutine(DelayAudio());

        levelTracker = GameObject.Find("LevelTracker").GetComponent<LevelTracker>();
    }

    public IEnumerator DelayAudio()
    {
        yield return new WaitForSeconds(.5f);
    }
    public void HoverSound()
    {
        ButtonHover.Play();
    }
    public void CLickSound()
    {
        ButtonClick.Play();
    }
    public void loadTitle()
    {
        SceneManager.LoadScene("Title");
    }
    public void ReloadGame()
    {
        
        if(levelTracker.lastScene == 1) 
            SceneManager.LoadScene("Level1");
        if (levelTracker.lastScene == 2)
            SceneManager.LoadScene("Level2");
        //if (levelTracker.lastScene == 3)
            //SceneManager.LoadScene("Level3");

    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
