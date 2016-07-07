using UnityEngine;
using System.Collections;

public class PlayerCam : MonoBehaviour {

	private float yRotation, xRotation, currXRot, yRotVelocity, xRotVelocity;
    [HideInInspector]
    public float currYRot;
	[SerializeField]
	private float sensitivity = 0f;

	private Rigidbody rb;

    private PlayerMovement player;
    private PlayerHUD hud;


	void Start(){
		rb = GetComponent<Rigidbody> ();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        hud = GetComponent<PlayerHUD>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
	}

	void Update () {

        if (!hud.menu || player.isAlive)
        {
            yRotation += Input.GetAxis("Mouse X") * 1.80f;
            xRotation -= Input.GetAxis("Mouse Y") * 1.80f;

            xRotation = Mathf.Clamp(xRotation, -90, 90);

            currYRot = Mathf.SmoothDamp(currYRot, yRotation, ref yRotVelocity, 0.09f);
            currXRot = Mathf.SmoothDamp(currXRot, xRotation, ref xRotVelocity, 0.09f);

            transform.rotation = Quaternion.Euler(this.transform.rotation.x + currXRot, this.transform.rotation.y + currYRot, this.transform.rotation.z);
        }
        else if (hud.menu)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
            transform.rotation = Quaternion.RotateTowards(this.transform.rotation, player.transform.rotation, 75.0f * Time.deltaTime);

	}
}
