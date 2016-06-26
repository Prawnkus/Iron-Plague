using UnityEngine;
using System.Collections;

public class EnemyAIControls : MonoBehaviour {

    private float speed, rotationSpeed;
    private bool canSeeTarget, isAimingAt, hasSeenPlayer;
    private RaycastHit hit, playerHit;
    private Vector3 direction = Vector3.zero;
    private Transform direct;

    private NavMeshAgent agent;
    [SerializeField]
    private Transform player;
    [SerializeField]
    private Transform destination1, destination2;

    void Start()
    {
        agent = this.transform.GetComponent<NavMeshAgent>();
        if (DistanceFromTarget(destination1.position) < DistanceFromTarget(destination2.position))
            MoveToTarget(destination2.position);
        else
            MoveToTarget(destination1.position);
    }

    void FixedUpdate()
    {
        if (hasSeenPlayer)
        {
            if (DistanceFromTarget(player.position) < 15.0f)
                StopMoving();
            else
                MoveToTarget(player.position);
        }
        else
        {
            PassivePathing(destination1, destination2);
            if (Physics.Raycast(this.transform.position, Direction(player), out playerHit) && DistanceFromTarget(player.position) < 25.0f)
                if (playerHit.collider.gameObject.tag == "Player")
                    hasSeenPlayer = true;
        }
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

    public float Angle(Vector3 targetDirection)
    {
        return Vector3.Angle(this.transform.forward, targetDirection);
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
        if (!CanSeeTarget(target))
        {
            float angle = Mathf.Atan2(Direction(target).y, Direction(target).x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, rotation, Time.fixedDeltaTime * rotationSpeed);
        }
    }

    private bool CanSeeTarget(Transform target)
    {
        bool canSee = false;
        if (Angle(Direction(player)) < 10.0f && Physics.Raycast(this.transform.position, this.transform.forward, out hit))
            if (DistanceFromTarget(hit.collider.gameObject.transform.position) >= DistanceFromTarget(target.forward))
                canSee = true;
        return canSee;
    }
}
