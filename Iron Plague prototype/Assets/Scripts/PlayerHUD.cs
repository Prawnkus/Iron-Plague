using UnityEngine;
using System.Collections;

public class PlayerHUD : MonoBehaviour {

	private int health = 100;
	string stringHealth;
	private float chargeAmount;
	private float chargeSpeed = 30f;
	public Texture2D currTex;
	public Texture2D chargeDoneTex;
	public Texture2D chargingTex;


	// Use this for initialization
	void Start () {
		chargeAmount = 500;
	}

	void Update(){
		bool qPressed = GameObject.Find ("GameManager").GetComponent<InputControl> ().q;
		if(qPressed && chargeAmount == 500f){
			chargeAmount = 0;
			//Activate ability
		}
		if (chargeAmount < 500) {
			chargeAmount += chargeSpeed * Time.deltaTime;
			currTex = chargingTex;
		} else if (chargeAmount >= 500) {
			chargeAmount = 500;
			currTex = chargeDoneTex;
		}
	}

	void OnGUI(){
		stringHealth = health.ToString ();
		stringHealth = GUI.TextField (new Rect(10,10,100,25),"HEALTH: " + stringHealth);

		GUI.DrawTexture(new Rect (10, 30, chargeAmount, 25), currTex);
		GUI.Box(new Rect(10, 30, 500, 25), "Ability charge");
	}
}
