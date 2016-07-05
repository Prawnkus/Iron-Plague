using UnityEngine;
using System.Collections;

public class RespawnPoints : MonoBehaviour {

    public bool hasPassed;

	void OnTriggerEnter(Collider col) 
    {
        if (col.gameObject.tag == "Player")
            hasPassed = true;
	}
}
