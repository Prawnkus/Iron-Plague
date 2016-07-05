using UnityEngine;
using System.Collections;

public class PlayerHUD : MonoBehaviour {

	private int health;
	string stringHealth;

	public int clipAmmoLeft;
	public int clipAmmoCap = 30;

    public bool qPressed, activeAbility;
    [HideInInspector]
	private float chargeAmount;
	private float chargeSpeed = 30f;

	public Texture2D currTex;
	public Texture2D chargeDoneTex;
	public Texture2D chargingTex;
    public Texture2D activeTex;
	public Texture2D crosshairCenter;
    private Texture2D currHealth;

	private float crosshairSize;
    private float chargerGainTimer;
    private PlayerMovement player;


	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
		chargeAmount = 0;
		clipAmmoLeft = clipAmmoCap;
        crosshairSize = 55.0f;
	}

	void Update(){

		//Ability charge bar
		qPressed = GameObject.Find ("GameManager").GetComponent<InputControl> ().q;
		if(qPressed && chargeAmount == 500f){
			//chargeAmount = 0;
            activeAbility = true;
            currTex = activeTex;
			//Activate ability
		}
		else if (chargeAmount < 500) {
            activeAbility = false;
			
			currTex = chargingTex;
		} 
        else if (chargeAmount > 500) {
			chargeAmount = 500;
			currTex = chargeDoneTex;
		}
	}

	void OnGUI(){
		//Crosshair
		GUI.DrawTexture (new Rect((Screen.width/2) - (crosshairSize/2), (Screen.height/2) - (crosshairSize/2),crosshairSize,crosshairSize), crosshairCenter);

		//Health meter

        stringHealth = player.health.ToString();
        if (player.health > 50f)
            currHealth = chargeDoneTex;
        else if (player.health < 50f && player.health > 25f)
            currHealth = activeTex;
        else
            currHealth = chargingTex;
        GUI.DrawTexture(new Rect(10, 10, 100, 25), currHealth);
        stringHealth = GUI.TextField(new Rect(10, 10, 100, 25), "HEALTH: " + stringHealth);

		//Ammo counter
		GUI.TextField(new Rect (Screen.width-120, Screen.height-45,100,25), clipAmmoLeft + "/" + clipAmmoCap);

		//Ability charge bar
        GUI.DrawTexture(new Rect(Screen.width / 2 - 250, Screen.height - 30, chargeAmount, 25), currTex);
		GUI.Box(new Rect(Screen.width/2 -250, Screen.height - 30, 500, 25), "Ability charge");
	}

    public void IncreaseCharger(float f)
    {
        chargeAmount += f;
    }

    public void SetChargeAmount(float f)
    {
        chargeAmount = f;
    }
}
