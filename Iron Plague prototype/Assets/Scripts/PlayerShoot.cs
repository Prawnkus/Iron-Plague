using UnityEngine;
using System.Collections;

public class PlayerShoot : MonoBehaviour {

    private float shootTimer, delay, powerSpeed;
    private float powerTimer, powerDelay;
	private bool hasPowerUp, hasPowerUpReady, powerUpHasPlayed;
	private AudioSource weaponAudio;
	private AudioSource powerUpAudio;
	public AudioClip gunSound;

    [SerializeField]
    private GameObject Projectiles, weapon, powerWeapon, activePowerPosition, passivePowerPosition;
    private PlayerHUD hud;

	void Start () {
        hud = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerHUD>();
		powerUpAudio = GameObject.Find("SwapAudioSource").GetComponent<AudioSource>();
		weaponAudio = GetComponent<AudioSource> ();
        delay = 0.1f;
        powerSpeed = 2.0f;
        powerTimer = 0.0f;
        powerDelay = 8.0f;
        shootTimer = 0.099f;
	}
	
	void Update () 

    {
        if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0))
        {
            if (shootTimer >= delay)
            {
                Shoot(weapon);
                if (hasPowerUp)
                    Shoot(powerWeapon);
                shootTimer = 0.0f;
            }
            else
                shootTimer += Time.deltaTime;
        }
        ActivatePowerUp();
	}

    private void Shoot(GameObject _weapon)
    {
        GameObject clone = null;
        clone = Instantiate(Projectiles, _weapon.transform.GetChild(0).position, _weapon.transform.GetChild(0).rotation) as GameObject;
        clone.GetComponent<ShootingProjectile>().shotByPlayer = true;
        clone.GetComponent<Rigidbody>().AddForce(clone.transform.forward * 5000.0f);
		playSound(gunSound);
    }

    private void Reload(GameObject _weapon)
    {
    }

	private void playSound (AudioClip sound)
	{
		weaponAudio.clip = sound;
		weaponAudio.Play ();
	}
    private void ActivatePowerUp()
    {
        if (hud.activeAbility && powerTimer <= powerDelay)
        {
            if (powerWeapon.transform.position == activePowerPosition.transform.position && powerWeapon.transform.rotation == activePowerPosition.transform.rotation)
            {
                hasPowerUp = true;
                powerTimer += Time.deltaTime;
				//play weaponSwapSound
				if(!powerUpAudio.isPlaying && !powerUpHasPlayed){
					powerUpAudio.Play ();
					powerUpHasPlayed = true;
				}

            }
            else if (!hasPowerUp)
            {
                powerWeapon.transform.position = Vector3.MoveTowards(powerWeapon.transform.position, activePowerPosition.transform.position, powerSpeed * Time.deltaTime);
                Quaternion lookrotation = Quaternion.LookRotation(activePowerPosition.transform.forward);
                powerWeapon.transform.rotation = Quaternion.RotateTowards(powerWeapon.transform.rotation, lookrotation, (powerSpeed * 75) * Time.deltaTime);
            }
        }
        else if (powerTimer >= powerDelay)
        {
            hasPowerUp = false;
			powerUpHasPlayed = false;
            powerWeapon.transform.position = Vector3.MoveTowards(powerWeapon.transform.position, passivePowerPosition.transform.position, powerSpeed * Time.deltaTime);
            Quaternion lookrotation = Quaternion.LookRotation(passivePowerPosition.transform.forward);
            hud.SetChargeAmount(0.0f);
            powerWeapon.transform.rotation = Quaternion.RotateTowards(powerWeapon.transform.rotation, lookrotation, (powerSpeed * 75) * Time.deltaTime);
            if (powerWeapon.transform.position == passivePowerPosition.transform.position && powerWeapon.transform.rotation == passivePowerPosition.transform.rotation)
            {
                powerTimer = 0.0f;
            }
        }
    }
}
