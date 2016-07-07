using UnityEngine;
using System.Collections;

public class ShootingProjectile : MonoBehaviour {

    [HideInInspector]
    public bool shotByPlayer;
    [HideInInspector]
    public float damage;
    [SerializeField]
    private ParticleSystem partiles, deathParticle;
    private string opponentTag = "Player";
    private float deathTime;

    void Start()
    {
        if (!transform.GetComponent<Rigidbody>())
            Debug.Log("Missing Rigidbody on projectiles!");
        damage = 20.0f;
        partiles.startColor = Color.red;
        deathParticle.startColor = Color.red;
        if (shotByPlayer)
        {
            opponentTag = "Enemy";
            partiles.startColor = Color.cyan;
            deathParticle.startColor = Color.cyan;
        }
    }

    void FixedUpdate()
    {
        if (deathTime > 4.5f)
            Destroy(this.gameObject);
        else
            deathTime += Time.fixedDeltaTime;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Enemy" && opponentTag == "Enemy")
            col.gameObject.GetComponent<EnemyAIControls>().TakeDamage(true, 35);
        else if (col.gameObject.tag == "Player" && opponentTag == "Player")
            col.gameObject.GetComponent<PlayerMovement>().TakeDamage(true, 10);

        Death();
        Destroy(this.gameObject);
    }

    void Death()
    {
        GameObject explosion = null;
        explosion = Instantiate(deathParticle, this.transform.position, this.transform.rotation) as GameObject;
    }
}
