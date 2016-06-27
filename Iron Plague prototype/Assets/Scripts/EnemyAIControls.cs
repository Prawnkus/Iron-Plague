using UnityEngine;
using System.Collections;

public class EnemyAIControls : MonoBehaviour {

    private float speed, rotationSpeed;
    private bool canSeeTarget, isAimingAt, hasSeenTarget;
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
            PassivePathing(destination1, destination2);
            if (Physics.Raycast(this.transform.position, Direction(player), out playerHit))
            {
                if (DistanceFromTarget(player.position) < 25.0f)
                    if (playerHit.collider.gameObject.tag == player.tag)
                        hasSeenTarget = true;
            }
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

    private void RayTowardsTarget(Transform target, bool _seesTarget)
    {
        if (Physics.Raycast(this.transform.position, Direction(target), out playerHit))
        {
            if (DistanceFromTarget(player.position) < 25.0f)
                if (playerHit.collider.gameObject.tag == target.tag)
                    _seesTarget = true;
        }
        else
            _seesTarget = false;
    }

    private void AimTowardsTarget(Transform target)
    {
        if (Vector3.Angle(this.transform.forward, Direction(player)) >= 10.0f)
        {
            //float angle = Mathf.Atan2(Direction(target).y, Direction(target).x) * Mathf.Rad2Deg;
            //Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            agent.updateRotation = true;
            Quaternion rotation = Quaternion.LookRotation(Direction(player));
            //this.transform.rotation = Quaternion.Slerp(this.transform.rotation, rotation, Time.fixedDeltaTime * rotationSpeed);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, Time.fixedDeltaTime * rotationSpeed);
            Debug.Log("Rotating");
        }
        else
            agent.updateRotation = false;
    }
}
