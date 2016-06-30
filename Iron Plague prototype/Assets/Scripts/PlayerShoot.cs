using UnityEngine;
using System.Collections;

public class PlayerShoot : MonoBehaviour {

    private float shootTimer, delay, powerSpeed;
    private float powerTimer, powerDelay;
    private bool hasPowerUp, hasPowerUpReady;

    [SerializeField]
    private GameObject Projectiles, weapon, powerWeapon, activePowerPosition, passivePowerPosition;
    private PlayerHUD hud;

	void Start () {
        hud = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerHUD>();
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
        clone.GetComponent<Rigidbody>().AddForce(clone.transform.forward * 3000.0f);
    }

    private void Reload(GameObject _weapon)
    {
        
    }

    private void ActivatePowerUp()
    {
        if (hud.activeAbility && powerTimer <= powerDelay)
        {
            if (powerWeapon.transform.position == activePowerPosition.transform.position && powerWeapon.transform.rotation == activePowerPosition.transform.rotation)
            {
                hasPowerUp = true;
                Debug.Log(powerTimer);
                powerTimer += Time.deltaTime;
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
            powerWeapon.transform.position = Vector3.MoveTowards(powerWeapon.transform.position, passivePowerPosition.transform.position, powerSpeed * Time.deltaTime);
            Quaternion lookrotation = Quaternion.LookRotation(passivePowerPosition.transform.forward);
            Debug.Log("Time's Up!");
            powerWeapon.transform.rotation = Quaternion.RotateTowards(powerWeapon.transform.rotation, lookrotation, (powerSpeed * 75) * Time.deltaTime);
            if (powerWeapon.transform.position == passivePowerPosition.transform.position && powerWeapon.transform.rotation == passivePowerPosition.transform.rotation)
            {
                powerTimer = 0.0f;
                hud.chargeAmount = 0.0f;
            }
        }
    }
}
