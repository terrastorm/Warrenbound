using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineGlowTree : MonoBehaviour {
    public Animator anime;
    public AnimatorClipInfo TreeAnim;
    public AnimationClip Wiggle;
    public Animation WiggleAnim;
    public AudioSource TreeRuffle;

    private Ray ray;
    private RaycastHit hit;
    private bool isPlaying = false;
    private Shader baseShader;
    
    public float distractTimer = 4f;
    private float timer = 0f;
    public float waitTimer = 4f;
    private float wait = 0f;

    void Start()
    {
        baseShader = gameObject.GetComponent<Shader>();
        anime = gameObject.GetComponent<Animator>();
        WiggleAnim = gameObject.GetComponent<Animation>();
        TreeRuffle = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //As long as the mouse hovers over object
        if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Tree"))
        {
            isPlaying = true;
            anime.Play("OutlineGlow");
            anime.Play("Wiggle");
            anime.SetBool("IsPlaying", true);
           if (Input.GetMouseButton(0) && gameObject.tag != "Distract")
           {
                gameObject.tag = "Distract";
                WiggleAnim.Play();
                TreeRuffle.Play();
            }
        }
        else //As long as the mouse is not hovering over the object
        {
            anime.SetBool("IsPlaying", false);
        }

        // Timing how long to keep wolf distracted
        if (gameObject.tag == "Distract") {
            timer += Time.deltaTime;

            if (timer >= distractTimer) {
                gameObject.tag = "Wait";
                timer = 0;
            }
        }

        // Timing how long to wait before next distraction time
        if ( gameObject.tag == "Wait" ) {
            wait += Time.deltaTime;

            if ( wait >= waitTimer ) {
                gameObject.tag = "Tree";
                wait = 0;
            }
        }
    }
}
