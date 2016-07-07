using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BloodVision : MonoBehaviour {

    private Image red;
    public bool shouldBleed = false;
    private float bleedValue = 0f;

    void Start()
    {
        red = this.GetComponent<Image>();
        red.GetComponent<CanvasRenderer>().SetAlpha(200f);
    }

    void FixedUpdate()
    {

        //bleedValue = red.GetComponent<CanvasRenderer>().GetAlpha();
        /*if (shouldBleed == true && bleedValue == 0)
        {
            Debug.Log("Start bleeding");
            bleedValue = 200.8f;
            red.GetComponent<CanvasRenderer>().SetAlpha(200.8f);
            shouldBleed = false;
        }*/
        /*if(bleedValue > 0)
        {
            Debug.Log("Bleed!"+bleedValue);
            bleedValue -= 0.5f;
            red.GetComponent<CanvasRenderer>().SetAlpha(bleedValue);
        }*/
    }
	public void Bleed()
    {
        Debug.Log("Start bleeding");
        red.CrossFadeAlpha(255f, 0.1f, false);
        red.CrossFadeAlpha(0f, 1.5f, false);
    }
}
