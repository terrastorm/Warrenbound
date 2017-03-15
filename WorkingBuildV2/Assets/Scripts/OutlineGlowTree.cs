using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineGlowTree : MonoBehaviour {



    //public Material RegularMaterial;

    //public Material OutlinedMaterial;
    public Animator anime;
    public AnimatorClipInfo TreeAnim;
    public AnimationClip Wiggle;
    public Animation WiggleAnim;
    private Animation BirdFly;
    public AudioSource TreeRuffle;
    //public AnimationClip OutlineGlowReverse;

    private Ray ray;
    private RaycastHit hit;
    private bool isPlaying = false;
    private Shader baseShader;

    void Start()
    {
        baseShader = GetComponent<Shader>();
        //OutlinedMaterial = new Material(Shader.Find("Outlined/Silhouetted Diffuse"));
        anime = GetComponent<Animator>();
        WiggleAnim = GetComponent<Animation>();
        BirdFly = GetComponent<Animation>();
        TreeRuffle = GetComponent<AudioSource>();
        // anime.AddClip(TreeVar1, "TreeVar1");
        //TreeAnim = 

    }

    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //As long as the mouse hovers over object
        if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Tree"))
        {
            Debug.Log("Over object");
            isPlaying = true;
            anime.Play("OutlineGlow");
            anime.Play("Wiggle");
            anime.SetBool("IsPlaying", true);
            //hit.collider.gamObject.renderer.material.shader = Shader.Find("Self-Illuminated Diffuse");
            //GetComponent<Renderer>().material = OutlinedMaterial; 
           if (Input.GetMouseButtonDown(0))
           {
                //GetComponent<Animation>().Play("Take 001");
           
                WiggleAnim.Play();
                TreeRuffle.Play();
            }
            else if (Input.GetMouseButtonUp(0))
           {
                WiggleAnim.Stop();
           }
        }
        else //As long as the mouse is not hovering over the object
        {
            Debug.Log("Not over object");
            anime.SetBool("IsPlaying", false);
            //Material material = new Material(Shader.Find("Standard"));
            //material.color = Color.white;
            //GetComponent<Renderer>().material = RegularMaterial;
        }

    }
}
