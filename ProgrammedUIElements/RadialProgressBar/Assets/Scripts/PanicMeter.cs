using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PanicMeter : MonoBehaviour {

    public Transform panicBar;
    private float value;

	void Start () {
        value = 0;
        panicBar.GetComponent<Image>().fillAmount = value; ;
	}
	
	void Update ()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            value = value + 0.01f;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            value = value - 0.01f;
        }
        panicBar.GetComponent<Image>().fillAmount = value;
	}
}
