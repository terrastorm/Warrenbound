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
    // Use this for initialization
    public void Start()
    {
        textEndTally.text = "Score: " + endTally.ToString();
        AudioListener.volume = 1.0f;
        StartCoroutine(DelayAudio());
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
        
        //if(SceneManager.GetActiveScene().buildIndex == 1)
            SceneManager.LoadScene("Tutorial Level");
       // if (SceneManager.GetActiveScene().buildIndex == 2)
           // SceneManager.LoadScene("Level2");
        //if (SceneManager.GetActiveScene().buildIndex == 3)
        //    SceneManager.LoadScene("Level3")
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
