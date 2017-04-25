using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class ScreenShotNavigation : MonoBehaviour {

    public Texture[] screenShots;
    public RawImage rawImage;
    public Button next;
    public Button prev;

    public int imageIndex = 0;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		rawImage.texture = screenShots[imageIndex];
         if(imageIndex >= 10) {
            SceneManager.LoadScene("Title");
        }
	}

    public void NextPage() {
        imageIndex++;
    }

    public void PreviousPage() {
        imageIndex--;

        if (imageIndex < 0)
            imageIndex = 0;
    }
}
