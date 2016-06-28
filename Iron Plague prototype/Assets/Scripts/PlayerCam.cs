using UnityEngine;
using System.Collections;

public class PlayerCam : MonoBehaviour {

	private float yRotation, xRotation, currYRot, currXRot, yRotVelocity, xRotVelocity;
	[SerializeField]
	private float sensitivity = 5;

	private Rigidbody rb;

	void Start(){
		rb = GetComponent<Rigidbody> ();
	}

	void Update () {
	
		yRotation += Input.GetAxis("Mouse X") * sensitivity;
		xRotation -= Input.GetAxis ("Mouse Y") * sensitivity;

		xRotation = Mathf.Clamp (xRotation, -90, 90);

		currYRot = Mathf.SmoothDamp (currYRot,yRotation, ref yRotVelocity, 0.1f);
		currXRot = Mathf.SmoothDamp (currXRot,xRotation, ref xRotVelocity, 0.1f);

		transform.rotation = Quaternion.Euler (xRotation, yRotation, 0);

	}
}
