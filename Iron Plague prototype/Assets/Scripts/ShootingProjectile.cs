using UnityEngine;
using System.Collections;

public class ShootingProjectile : MonoBehaviour {

    [HideInInspector]
    public bool shotByPlayer;
    [HideInInspector]
    public float damage;
    [SerializeField]
    private ParticleSystem partiles;
    private string controllerTag, opponentTag;

    void Start()
    {
        if (!transform.GetComponent<Rigidbody>())
            Debug.Log("Missing Rigidbody on projectiles!");

        if (shotByPlayer)
        {
            controllerTag = "Player";
            opponentTag = "Enemy";
            partiles.startColor = Color.cyan;
        }
        else
        {
            controllerTag = "Enemy";
            opponentTag = "Player";
            partiles.startColor = Color.red;
        }
        damage = 15.0f;
    }

    void OnCollisionEnter(Collision col)
    {

        if (col.gameObject.tag == opponentTag)
        {
            if (opponentTag == "Enemy")
            {
                col.gameObject.GetComponent<EnemyAIControls>().TakeDamage(true, damage);
            }
            else if (opponentTag == "Player")
                col.gameObject.GetComponent<PlayerMovement>().health -= damage;
        }

        Destroy(this.gameObject);
    }
}
