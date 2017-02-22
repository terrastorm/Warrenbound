using UnityEngine;
using System.Collections;

public class PlaneScroll : MonoBehaviour
{
    public float speed;
    public static PlaneScroll current;

    //float pos = 0;

    void Start()
    {
        current = this;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 offset = new Vector2(Time.time * speed, 0);
        GetComponent<Renderer>().material.mainTextureOffset = offset;
    }    
}