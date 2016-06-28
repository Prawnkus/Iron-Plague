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
        damage = -25.0f;
    }

    void OnCollisionEnter(Collision col)
    {

        if (col.gameObject.tag == opponentTag)
        {
            if (opponentTag == "enemy")
                col.gameObject.GetComponent<EnemyAIControls>().health -= damage;
            Debug.Log(opponentTag + " Hit");
        }
        if (col.gameObject.tag == "Environment")
        {
            Debug.Log("Nothing Hit");
        }
        Destroy(this.gameObject);
    }
}
