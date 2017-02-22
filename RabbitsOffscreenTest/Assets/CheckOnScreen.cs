using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckOnScreen : MonoBehaviour {

    Camera camera;

    public Transform target; //A rabbit
    public bool inFrustrum;
    public GameObject image;
    public Canvas canvas;
    RectTransform imageRT;
    GameObject imageCopy;

    // Use this for initialization
    void Start () {
        //Create the image and set it to be under the canvas, and disable it(for when this script is attached to each rabbit) -- code
        imageCopy = Instantiate<GameObject>(image);
        imageCopy.transform.position = new Vector3(0, 0, 0);
        imageCopy.SetActive(false);
        imageCopy.transform.parent = canvas.transform;
        camera = Camera.main;
        imageRT = imageCopy.GetComponent<RectTransform>();
    }
	
	// Update is called once per frame
	void Update () {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);

        if (GeometryUtility.TestPlanesAABB(planes, GetComponent<Renderer>().bounds))//Test if object is inside the camera view or not
            inFrustrum = true;
        else
            inFrustrum = false;

        Vector3 screenPos = camera.WorldToScreenPoint(target.position); //get the screen position of the rabbit

        if (inFrustrum)
            imageCopy.SetActive(false); //makes the marker disapear
        else
        {
            imageCopy.SetActive(true); //makes the marker reapear

            if (screenPos.x < 0)//
                imageRT.transform.position = new Vector3(0, screenPos.y, 0);
            else if(screenPos.x > Screen.width)//
                imageRT.transform.position = new Vector3(Screen.width, screenPos.y, 0);
            else if (screenPos.y < 0)//
                imageRT.transform.position = new Vector3(screenPos.x, 0, 0);
            else if(screenPos.y > Screen.height)//
                imageRT.transform.position = new Vector3(screenPos.x, Screen.height, 0);
            else if (screenPos.y < 0 & screenPos.x < 0)//
                imageRT.transform.position = new Vector3(0, 0, 0);
        }
            


       //Debug.Log("target is " + screenPos.x + " pixels from the left");
       //Debug.Log("target is " + screenPos.y + " pixels from the bottom");

    }
}
