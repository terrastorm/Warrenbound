using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeEndLevel : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        doFade();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void doFade()
    {
        GetComponent<Image>().CrossFadeAlpha(0.1f, 2.0f, false);
        //im.GetComponent<CanvasRenderer>().SetAlpha(0f);
        //im.CrossFadeAlpha(1f, .1f, false);
    }
}
