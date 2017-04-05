using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineGlowBush : MonoBehaviour {

    public Animator Animation;   
    private Ray ray;
    private RaycastHit hit;
    private bool isPlaying = false;
    private Shader baseShader;

    void Start()
    {
        baseShader = GetComponent<Shader>();
        Animation = GetComponent<Animator>();
    }

    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //As long as the mouse hovers over object
        if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Bush"))
        {
            isPlaying = true;
            Animation.Play("OutlineGlowBush");           
            Animation.SetBool("IsPlaying", true);                 
        }
        else //As long as the mouse is not hovering over the object
        {
            Animation.SetBool("IsPlaying", false);            
        }

    }
}
