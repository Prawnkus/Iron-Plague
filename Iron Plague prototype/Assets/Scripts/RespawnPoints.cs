using UnityEngine;
using System.Collections;

public class RespawnPoints : MonoBehaviour {

    [HideInInspector]
    public bool hasPassed;

	void OnTriggerEnter(Collider col) 
    {
        if (col.gameObject.tag == "Player")
            hasPassed = true;
	}
}
