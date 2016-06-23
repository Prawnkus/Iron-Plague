using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

    private bool canSeeTarget, isAimingAt;

    private Rigidbody rb;

    void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
    }


    private void ShootAtTarget(Transform target)
    {

    }

    private void AimTowardsTarget(Transform target)
    {

    }

    private void MoveToLocation(Vector3 selectedLocation)
    {

    }
}