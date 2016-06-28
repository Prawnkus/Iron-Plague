using UnityEngine;
using System.Collections;

public class EnemyAIControls : MonoBehaviour {

    private float rotationSpeed, shootTimer, delay;
    private bool canSeeTarget, isAimingAt;
    private RaycastHit hit, playerHit;
    private Vector3 direction = Vector3.zero;
    private Transform direct;
    [HideInInspector]
    public float health = 100.0f;
    [HideInInspector]
    public bool hasSeenTarget;

    private NavMeshAgent agent;
    [SerializeField]
    private Transform player;
    [SerializeField]
    private Transform destination1, destination2;
    [SerializeField]
    private GameObject weapon;
    [SerializeField]
    private GameObject projectile;

    void Start()
    {
        delay = 1.3f;
        shootTimer = delay;
        rotationSpeed = 150f;
        agent = this.transform.GetComponent<NavMeshAgent>();
        if (DistanceFromTarget(destination1.position) < DistanceFromTarget(destination2.position))
            MoveToTarget(destination2.position);
        else
            MoveToTarget(destination1.position);
    }

    void FixedUpdate()
    {
        if (hasSeenTarget)
        {
            // When the enemy has seen the player, it will chase it until death.
            if (Physics.Raycast(this.transform.position, Direction(player), out playerHit))
            {
                if (DistanceFromTarget(player.position) < 25.0f)
                    if (playerHit.collider.gameObject.tag == player.tag)
                        canSeeTarget = true;
                    else if (playerHit.collider.gameObject.tag != player.tag)
                        canSeeTarget = false;
            }

            if (DistanceFromTarget(player.position) <= 15.0f && canSeeTarget)
            {
                if (!agent.SetDestination(this.transform.position))
                    StopMoving();
                AimTowardsTarget(player);
            }
            else if (DistanceFromTarget(player.position) > 15.0f || !canSeeTarget)
                MoveToTarget(player.position);
        }
        else
        {
            // Passive pratrol pathing
            PassivePathing(destination1, destination2);
            if (Physics.Raycast(this.transform.position, Direction(player), out playerHit))
            {
                if (DistanceFromTarget(player.position) < 25.0f)
                    if (playerHit.collider.gameObject.tag == player.tag)
                        hasSeenTarget = true;
            }
        }
        // Death
        if (health <= 0.0f)
            Destroy(this);
    }

    public float DistanceFromTarget(Vector3 target)
    {
        return Vector3.Distance(this.transform.position, target);
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
        if (DistanceFromTarget(location1.position) < 1.0f)
            MoveToTarget(destination2.position);
        else if (DistanceFromTarget(location2.position) < 1.0f)
            MoveToTarget(destination1.position);
    }

    private void AimTowardsTarget(Transform target)
    {
        if (Vector3.Angle(this.transform.forward, Direction(player)) >= 8.5f)
        {
            agent.updateRotation = true;
            Quaternion rotation = Quaternion.LookRotation(Direction(player));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, Time.fixedDeltaTime * rotationSpeed);
            shootTimer = delay / 2.0f;
        }
        else
        {
            agent.updateRotation = false;
            Shoot();
        }
    }

    private void Shoot()
    {
        if (shootTimer >= delay)
        {
            GameObject clone = null;
            clone = Instantiate(projectile, weapon.transform.position, weapon.transform.rotation) as GameObject;
            clone.GetComponent<ShootingProjectile>().shotByPlayer = false;
            clone.GetComponent<Rigidbody>().AddForce(clone.transform.forward * 1000f);
            shootTimer = 0.0f;
        }
        else
            shootTimer += Time.fixedDeltaTime;
    }
}
