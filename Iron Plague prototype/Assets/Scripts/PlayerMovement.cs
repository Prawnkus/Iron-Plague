using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	[SerializeField]
	private float playerSpeed;
	[SerializeField]
	private float playerGravity = 20.0f;
    private float jumpSpeed = 8.5f;
    [HideInInspector]
    public float health = 100.0f, dmgToTake;
    [HideInInspector]
    public bool hasTakenDamage;


	//private Rigidbody rb;

	private InputControl inputControl;

	private CharacterController controller;

    private PlayerCam cam;

	private Vector3 moveDirection = Vector3.zero;


	void Start () {

		//rb = GetComponent<Rigidbody> ();
        cam = GameObject.FindWithTag("MainCamera").GetComponent<PlayerCam>();
		GameObject gameManager = GameObject.Find ("GameManager");
		inputControl = gameManager.GetComponent<InputControl> ();

		controller = GetComponent<CharacterController> ();
	}

	void Update () {
		
		if (controller.isGrounded) {
			moveDirection = new Vector3 (inputControl.lh, 0, inputControl.lv);
			moveDirection = transform.TransformDirection (moveDirection);
			moveDirection *= playerSpeed;
            if (Input.GetKeyDown(KeyCode.Space))
                moveDirection.y = jumpSpeed;
		}

        transform.rotation = Quaternion.Euler(0.0f, cam.currYRot, 0.0f);

		moveDirection.y -= playerGravity * Time.deltaTime;

		controller.Move (moveDirection * Time.deltaTime);

	}

    public void TakeDamage(bool state, float dmg)
    {
        hasTakenDamage = state;
        dmgToTake = dmg;
    }

}


