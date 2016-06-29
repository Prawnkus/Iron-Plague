using UnityEngine;
using System.Collections;

public class PlayerShoot : MonoBehaviour {

    private float shootTimer, delay;

    [SerializeField]
    private GameObject Projectiles, weapon;

	void Start () {
        delay = 0.1f;
        shootTimer = 0.099f;
	}
	
	void Update () {
        if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0))
        {
            if (shootTimer >= delay)
            {
                GameObject clone = null;
                clone = Instantiate(Projectiles, weapon.transform.position, weapon.transform.rotation) as GameObject;
                clone.GetComponent<ShootingProjectile>().shotByPlayer = true;
                clone.GetComponent<Rigidbody>().AddForce(clone.transform.forward * 3000.0f);
                shootTimer = 0.0f;
            }
            else
                shootTimer += Time.deltaTime;
        }
	}
}
