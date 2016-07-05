using UnityEngine;
using System.Collections;

public class ShootingProjectile : MonoBehaviour {

    [HideInInspector]
    public bool shotByPlayer;
    [HideInInspector]
    public float damage;
    [SerializeField]
    private ParticleSystem partiles;
    private string opponentTag = "Player";

    void Start()
    {
        if (!transform.GetComponent<Rigidbody>())
            Debug.Log("Missing Rigidbody on projectiles!");
        damage = 15.0f;
        partiles.startColor = Color.red;
        if (shotByPlayer)
        {
            opponentTag = "Enemy";
            partiles.startColor = Color.cyan;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Enemy" && opponentTag == "Enemy")
            col.gameObject.GetComponent<EnemyAIControls>().TakeDamage(true, damage);
        else if (col.gameObject.tag == "Player" && opponentTag == "Player")
            col.gameObject.GetComponent<PlayerMovement>().TakeDamage(true, damage);

        Destroy(this.gameObject);
    }
}
