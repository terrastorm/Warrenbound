using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineGlowBush : MonoBehaviour {

    public Animation BushGlow;
    public Animator anime;   
    private Ray ray;
    private RaycastHit hit;
    private bool isPlaying = false;
    private Shader baseShader;

    void Start()
    {
        baseShader = GetComponent<Shader>();
        anime = GetComponent<Animator>();
        BushGlow = GetComponent<Animation>();
    }

    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //As long as the mouse hovers over object
        if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Tree"))
        {
            BushGlow.Play();
            //Debug.Log("Over object");
            isPlaying = true;
            //anime.Play("OutlineGlowBush");           
            anime.SetBool("IsPlaying", true);                 
        }
        else //As long as the mouse is not hovering over the object
        {
            BushGlow.Stop();
            //Debug.Log("Not over object");
            //anime.SetBool("IsPlaying", false);            
        }

    }
}
