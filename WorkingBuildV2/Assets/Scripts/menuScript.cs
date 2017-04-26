using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class menuScript : MonoBehaviour {

	public Canvas quitMenu;
	public Button startText;
	public Button exitText;
    public Animation background;
    public Animation acamera;
    public Animation clouds;
    [SerializeField] private float delay = 1f;
    public AudioSource Title;
    public AudioSource ButtonClick;
    public AudioSource ButtonHover;
    // Use this for initialization
    void Start () 
	{
		quitMenu = quitMenu.GetComponent<Canvas> ();
		startText = startText.GetComponent<Button> ();
		exitText = exitText.GetComponent<Button> ();
        quitMenu.enabled = false;
    }

    public void HowTo() {
        SceneManager.LoadScene("TutorialScene");
    }

	public void ExitPress()
	{
		quitMenu.enabled = true;
		startText.enabled = false;
		exitText.enabled = false;
	}

	public void NoPress()
	{
		quitMenu.enabled = false;
		startText.enabled = true;
		exitText.enabled = true;
	}

    public void ExitGame()
	{
		Application.Quit ();
	}
    
    public void FadeMe()
    {
        StartCoroutine (DoFade());
    }

    IEnumerator DoFade()
    {   
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime / .7f;
            yield return null;
        }
        canvasGroup.interactable = false;
        yield return null;
    }

    public void StartLevel()
    {
        AudioListener.volume = 1.0f;
        StartCoroutine(DelayBackground());
        StartCoroutine(DelayCamera());
        //Debug.Log("play pls = " + acamera.IsPlaying("CameraZoom"));    
        StartCoroutine(DelaySceneLoad("Level1"));
        StartCoroutine(DelayClouds());
    }

    public IEnumerator DelaySceneLoad(string Level1)
    {
        yield return new WaitForSeconds(4f);
        float elapsedTime = 0;
        float currentVolume = AudioListener.volume;
        while (elapsedTime < delay)
        {
            elapsedTime += Time.deltaTime;
            AudioListener.volume = Mathf.Lerp(currentVolume, 0, elapsedTime / delay);
            yield return null;
        }
        SceneManager.LoadScene("Level1");
    }
    public IEnumerator DelayBackground()
    {
        yield return new WaitForSeconds(3f);
        background.Play();
    }

    public IEnumerator DelayClouds()
    {
        yield return new WaitForSeconds(1f);
        clouds.Play();
    }

    public IEnumerator DelayCamera()
    {
        yield return new WaitForSeconds(0);
        acamera.Play();
    }
    public void HoverSound()
    {
        ButtonHover.Play();
    }
    public void CLickSound()
    {
        ButtonClick.Play();
    }
}




