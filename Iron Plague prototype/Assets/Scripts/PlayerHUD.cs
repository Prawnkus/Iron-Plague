using UnityEngine;
using System.Collections;

public class PlayerHUD : MonoBehaviour {

	private int health = 100;
	string stringHealth;

	public int clipAmmoLeft;
	public int clipAmmoCap = 30;

	private float chargeAmount;
	private float chargeSpeed = 30f;

	public Texture2D currTex;
	public Texture2D chargeDoneTex;
	public Texture2D chargingTex;
	public Texture crosshairCenter;

	public float crosshairSize = 20f;


	// Use this for initialization
	void Start () {
		chargeAmount = 500;
		clipAmmoLeft = clipAmmoCap;
	}

	void Update(){

		//Ability charge bar
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
		//Crosshair
		GUI.DrawTexture (new Rect((Screen.width/2) - (crosshairSize/2), (Screen.height/2) - (crosshairSize/2),crosshairSize,crosshairSize), crosshairCenter);

		//Health meter
		stringHealth = health.ToString ();
		stringHealth = GUI.TextField (new Rect(10,10,100,25),"HEALTH: " + stringHealth);

		//Ammo counter
		GUI.TextField(new Rect (Screen.width-120, Screen.height-45,100,25), clipAmmoLeft + "/" + clipAmmoCap);

		//Ability charge bar
		GUI.DrawTexture(new Rect (10, 30, chargeAmount, 25), currTex);
		GUI.Box(new Rect(10, 30, 500, 25), "Ability charge");
	}
}
