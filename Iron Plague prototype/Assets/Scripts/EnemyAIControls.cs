using UnityEngine;
using System.Collections;

public class EnemyAIControls : MonoBehaviour {

    private float rotationSpeed, shootTimer, delay;
    private float dmgTimer, dmgDelay, dmgToTake;
    private bool canSeeTarget, isAimingAt;
    private RaycastHit hit, playerHit;
    private Vector3 direction = Vector3.zero;
    private Transform direct;
	public AudioClip shootSound;
	public AudioSource enemyAudio;

    [HideInInspector]
    public float health = 100.0f;
    [HideInInspector]
    public bool hasSeenTarget, hasTakenDamage;

    private NavMeshAgent agent;
    private Renderer rend;
    public Color baseColor;

    [SerializeField]
    private Transform player, destination1, destination2;
    [SerializeField]
    private GameObject weapon, projectile, baseObj;
    private PlayerHUD hud;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = this.transform.GetComponent<NavMeshAgent>();
        rend = GetComponent<Renderer>();
        hud = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerHUD>();
        MoveToTarget(destination1.position);
		enemyAudio = GetComponent<AudioSource> ();

        baseColor = Color.gray;
        delay = 0.25f;
        dmgDelay = 0.085f;
        shootTimer = delay;
        rotationSpeed = 230f;

    }

    void FixedUpdate()
    {
        if (!hud.menu)
        {
            if (hasSeenTarget)
            {
                // When the enemy has seen the player, it will chase it until death.
                if (Physics.Raycast(this.transform.position, Direction(player), out playerHit))
                {
                    if (DistanceFromTarget(player.position) < 1000.0f)
                        if (playerHit.collider.gameObject.tag == player.tag)
                            canSeeTarget = true;
                        else if (playerHit.collider.gameObject.tag != player.tag)
                            canSeeTarget = false;
                }

                if (DistanceFromTarget(player.position) <= 60.0f && canSeeTarget)
                {
                    if (!agent.SetDestination(this.transform.position))
                        StopMoving();
                    else
                        AimTowardsTarget(player);
                }
                else if (DistanceFromTarget(player.position) > 40.0f || !canSeeTarget)
                    MoveToTarget(player.position);
            }
            else
            {
                // Passive pratrol pathing
                PassivePathing(destination1, destination2);
                if (Physics.Raycast(this.transform.position, Direction(player), out playerHit))
                {
                    if (DistanceFromTarget(player.position) < 40.0f)
                        if (playerHit.collider.gameObject.tag == player.tag)
                            hasSeenTarget = true;
                }
            }
            // Death
            if (hasTakenDamage)
            {
                if (dmgTimer >= dmgDelay)
                {
                    health -= dmgToTake;
                    hasTakenDamage = false;
                    rend.material.color = baseColor;
                }
                else
                {
                    dmgTimer += Time.fixedDeltaTime;
                    rend.material.color = Color.red;
                }
            }
            else
                dmgTimer = 0.0f;

            if (health <= 0.0f)
            {
                hud.IncreaseCharger((int)Random.Range(4, 8));
                hud.enemyKill += 1;
                Destroy(baseObj);
            }
        }
    }

    public float DistanceFromTarget(Vector3 target)
    {
        return Vector3.Distance(target, this.transform.position);
    }

    private void StopMoving()
    {
        agent.destination = this.transform.position;
    }

    private void MoveToTarget(Vector3 target)
    {
        agent.destination = target;
    }

    public Vector3 Direction(Transform target)
    {
        return (target.position - this.transform.position).normalized;
    }

    private void PassivePathing(Transform location1, Transform location2)
    {
        if (DistanceFromTarget(location1.position) < 1f)
            MoveToTarget(destination2.position);
        if (DistanceFromTarget(location2.position) < 1f)
            MoveToTarget(destination1.position);
    }

    private void AimTowardsTarget(Transform target)
    {
        if (Vector3.Angle(this.transform.forward, Direction(target)) <= 6.5f)
        {
            Shoot();
            Precision(target, 2.5f);
        }
        else
            Precision(target, 1);
    }

    private void Precision(Transform _target, float divided)
    {
        Quaternion rotation = Quaternion.LookRotation(Direction(_target));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, Time.fixedDeltaTime * (rotationSpeed / divided));
    }

    private void Shoot()
    {
        if (shootTimer >= delay)
        {
            GameObject clone = null;
            clone = Instantiate(projectile, weapon.transform.position, weapon.transform.rotation) as GameObject;
            clone.GetComponent<ShootingProjectile>().shotByPlayer = false;
            clone.GetComponent<Rigidbody>().AddForce(clone.transform.forward * 2500.0f);
            shootTimer = 0.0f;

			enemyAudio.clip = shootSound;
			enemyAudio.Play ();
        }
        else
            shootTimer += Time.fixedDeltaTime;
    }

    public void TakeDamage(bool state, float dmg)
    {
        hasTakenDamage = state;
        dmgToTake = dmg;
    }
}
