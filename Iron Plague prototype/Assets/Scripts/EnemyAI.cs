using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

    private bool canSeeTarget, isAimingAt;
    private RaycastHit hit, playerHit;
    private Vector3 direction;

    private Rigidbody rb;
    private GameObject player;

    void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        direction = player.transform.position - this.transform.position;

        if (Angle(direction) < 10.0f)
            canSeeTarget = true;

        if (!IsTooFarAway(player.transform) && CanSeeTarget(player.transform))
            ShootAtTarget(player.transform);

        if(Physics.Raycast(this.transform.position, player.transform.position, out playerHit))
        {
            if (playerHit.collider.gameObject.tag == "Player")
                AimTowardsTarget(player.transform);
            else if (playerHit.collider.gameObject.tag != "Player")
            {
                // Move around to a random location
            }
        }
    }


    private void ShootAtTarget(Transform target)
    {
        
    }

    private void AimTowardsTarget(Transform target)
    {
        
    }

    private void MoveToLocation(Vector3 selectedLocation)
    {
        if (IsTooFarAway(player.transform))
        {

        }
    }

    private bool CanSeeTarget(Transform target)
    {
        bool canSee = false;
        if (Angle(direction) < 10.0f && Physics.Raycast(this.transform.position, this.transform.forward, out hit))
            if (Vector3.Distance(this.transform.position, hit.collider.gameObject.transform.position) >= Vector3.Distance(this.transform.position, target.transform.forward))
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
}