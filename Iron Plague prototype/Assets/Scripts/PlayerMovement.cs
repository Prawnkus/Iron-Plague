using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	[SerializeField]
	private float playerSpeed;
	[SerializeField]
	private float playerGravity = 20.0f;

	//private Rigidbody rb;

	private InputControl inputControl;

	private CharacterController controller;

	private Vector3 moveDirection = Vector3.zero;


	void Start () {

		//rb = GetComponent<Rigidbody> ();
		GameObject gameManager = GameObject.Find ("GameManager");
		inputControl = gameManager.GetComponent<InputControl> ();

		controller = GetComponent<CharacterController> ();
	}

	void Update () {
		
		if (controller.isGrounded) {
			moveDirection = new Vector3 (inputControl.lh, 0, inputControl.lv);
			moveDirection = transform.TransformDirection (moveDirection);
			moveDirection *= playerSpeed;
		}

		//rb.velocity = moveDirection * playerSpeed;
		//rb.AddForce ((Vector3.right * inputControl.lh * playerSpeed) * 100 * Time.deltaTime);
		//rb.AddForce ((Vector3.forward * inputControl.lv * playerSpeed) * 100 * Time.deltaTime);


		moveDirection.y -= playerGravity * Time.deltaTime;

		controller.Move (moveDirection * Time.deltaTime);

		}

	}

