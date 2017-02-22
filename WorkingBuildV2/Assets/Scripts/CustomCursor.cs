using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class CustomCursor : MonoBehaviour
{
    public Texture2D cursorTexture;
    public Texture2D cursorTexture2;
    public AudioSource ButtonClick;

    private int cursorWidth = 32;
    private int cursorHeight = 32; 

    void Start()
    {
        Cursor.visible = false;
    }
    void OnGUI()
    {   
        GUI.DrawTexture(new Rect(Input.mousePosition.x, Screen.height - Input.mousePosition.y, cursorWidth, cursorHeight), cursorTexture);
        if (Input.GetMouseButtonDown(0))
        {
            //Cursor.SetCursor(cursorTexture2, hotSpot, cursorMode);
            //ButtonClick.Play();
        }
    }

}
