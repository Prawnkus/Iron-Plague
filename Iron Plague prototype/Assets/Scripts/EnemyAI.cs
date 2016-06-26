using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

    private float speed, rotationSpeed;
    private bool canSeeTarget, isAimingAt, hasSeenPlayer;
    private RaycastHit hit, playerHit;
    private Vector3 direction = Vector3.zero;
    private Transform direct;

    private Rigidbody rb;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private Transform passiveWalkLocation1, passiveWalkLocation2;

    void Start()
    {
        speed = 5.0f;
        rotationSpeed = 3.0f;
        rb = transform.GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        direction = (passiveWalkLocation1.position - this.transform.position).normalized;
    }

    void FixedUpdate()
    {

        if (!IsTooFarAway(player.transform) && CanSeeTarget(player.transform))
            ShootAtTarget(player.transform);

        if(Physics.Raycast(this.transform.position, player.transform.position, out playerHit))
            if (playerHit.collider.gameObject.tag == "Player" && !IsTooFarAway(player.transform))
                hasSeenPlayer = true;

        if (hasSeenPlayer)
        {
            AimTowardsTarget(player.transform);
            if (IsTooFarAway(player.transform))
                rb.MovePosition(this.transform.position + this.transform.forward * speed * Time.fixedDeltaTime);
            else
                ShootAtTarget(player.transform);
        }
        else if (!hasSeenPlayer)
        {
            PassivePathing(direction, passiveWalkLocation1, passiveWalkLocation1);
            rb.MovePosition(this.transform.position + this.transform.forward * speed * Time.fixedDeltaTime);
            Debug.Log("MOVING - Passive");
            AimTowardsTarget(direct);
        }
    }


    private void ShootAtTarget(Transform target)
    {
        Debug.Log("Bam! Bam!");
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
        if (Angle(direction) < 10.0f && Physics.Raycast(this.transform.position, this.transform.forward, out hit))
            if (Vector3.Distance(this.transform.position, hit.collider.gameObject.transform.position) >= Vector3.Distance(this.transform.position, target.forward))
                canSee = true;
        return canSee;
    }

    private bool IsTooFarAway(Transform target)
    {
        bool _canMove = false;
        if (Vector3.Distance(this.transform.position, target.position) > 5.0f)
            _canMove = true;

        return _canMove;
    }

    public float Angle(Vector3 targetDirection)
    {
        return Vector3.Angle(this.transform.forward, targetDirection);
    }

    public Vector3 Direction(Transform target)
    {
        direction = (target.position - this.transform.position).normalized;
        direct.position = direction;
        return direction;
    }

    private void PassivePathing(Vector3 targetLocation, Transform location1, Transform location2)
    {
        if (Vector3.Distance(targetLocation, location1.position) < 1.0f)
            Direction(location2);
        else if (Vector3.Distance(targetLocation, location2.position) < 1.0f)
            Direction(location1);
    }
}