using UnityEngine;
using System.Collections;

public class ProjectileDeath : MonoBehaviour {

    private float deathTime, delay = 0.1f;

	void FixedUpdate () {
        if (deathTime >= delay)
            Destroy(this.gameObject);
        else
            deathTime += Time.fixedDeltaTime;
	}
}
