using UnityEngine;
using System.Collections;

public class InputControl : MonoBehaviour {

    public float lv, lh, rv, rh, ls, rs;
	public int inputDevice = 0;
	float delay = 0.0f;

	void Update () {
		//Toggle input method
		// 0=Keyboard
		// 1=Xbox controller
		if(Input.GetKey("t") && Time.time > delay){
			delay = Time.time + 0.5f;
			inputDevice++;
			if(inputDevice==2){
				inputDevice=0;
			}
			switch(inputDevice){
			case 0:
				Debug.Log ("Keyboard is now active input device");
				break;
			case 1:
				Debug.Log ("Xbox Controller is now active input device");
				break;
			}
		}

        //Assigning input reads to variables
        lh = Input.GetAxis("Horizontal");
        lv = Input.GetAxis("Vertical");
        if (inputDevice == 0) {
			//rh = Input.GetAxis ("Keyboard_J+L");
			//rv = Input.GetAxis ("Keyboard_K+I");
            
		}
		if (inputDevice == 1) {
			rh = Input.GetAxis ("Xbox_RAnalogH");
			rv = Input.GetAxis ("Xbox_RAnalogV");
			ls = Input.GetAxis ("Xbox_LShoulder");
			rs = Input.GetAxis ("Xbox_RShoulder");
			//Debug.Log (RS);
		}
	}
}
