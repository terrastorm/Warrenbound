using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class Fade : MonoBehaviour
{
    [SerializeField]
    private float delay = 1f;
    public AudioSource EndGameSound;
    public Animation FadeBlack;
    // Use this for initialization
    void Start()
    {
        AudioListener.volume = 1.0f;
        doFade();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void doFade()
    {
        GetComponent<Image>().CrossFadeAlpha(0.1f, 2.0f, false);
    }
    public IEnumerator DelayEndLevel(string EndLevel)
    {
        yield return new WaitForSeconds(2f);
        float elapsedTime = 0;
        float currentVolume = AudioListener.volume;
        while (elapsedTime < delay)
        {
            elapsedTime += Time.deltaTime;
            AudioListener.volume = Mathf.Lerp(currentVolume, 0, elapsedTime / delay);
            yield return null;
        }
        SceneManager.LoadScene("EndLevel");
    }
    /*public IEnumerator FadeToBlack()
    {
        yield return new WaitForSeconds(0f);
        FadeBlack.Play();
    }*/
}
