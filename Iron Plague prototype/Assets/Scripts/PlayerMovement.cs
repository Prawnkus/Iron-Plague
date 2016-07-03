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
    public bool isAlive;


	//private Rigidbody rb;

	private InputControl inputControl;

	private CharacterController controller;
    private RespawnSystem respawnSystem;

    private PlayerCam cam;

	private Vector3 moveDirection = Vector3.zero;


	void Start () {
        isAlive = true;
		//rb = GetComponent<Rigidbody> ();
        cam = GameObject.FindWithTag("MainCamera").GetComponent<PlayerCam>();
        respawnSystem = GameObject.Find("RespawnSystem").GetComponent<RespawnSystem>();
		GameObject gameManager = GameObject.Find ("GameManager");
		inputControl = gameManager.GetComponent<InputControl> ();

		controller = GetComponent<CharacterController> ();
	}

	void Update () {

        if (isAlive)
        {
            if (controller.isGrounded)
            {
                moveDirection = new Vector3(inputControl.lh, 0, inputControl.lv);
                moveDirection = transform.TransformDirection(moveDirection);
                moveDirection *= playerSpeed;
                if (Input.GetKeyDown(KeyCode.Space))
                    moveDirection.y = jumpSpeed;
            }

            transform.rotation = Quaternion.Euler(0.0f, cam.currYRot, 0.0f);

            moveDirection.y -= playerGravity * Time.deltaTime;

            controller.Move(moveDirection * Time.deltaTime);
        }
	}

    void FixedUpdate()
    {
        if (hasTakenDamage)
        {
            if (health <= 0)
            {
                DeathSequence();
            }
            else
            {
                health -= dmgToTake;
                hasTakenDamage = false;
            }
        }
    }

    public void TakeDamage(bool state, float dmg)
    {
        hasTakenDamage = state;
        dmgToTake = dmg;
    }

    private void DeathSequence()
    {
        Quaternion fallRot = Quaternion.Euler(270, this.transform.rotation.y, 0.0f);

        if (this.transform.rotation != fallRot)
        {
            isAlive = false;
            this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, fallRot, 150 * Time.fixedDeltaTime);
        }
        else
        {
            respawnSystem.Respawn(this.transform);
            isAlive = true;
        }
    }
}


