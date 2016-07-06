using UnityEngine;
using System.Collections;

public class RespawnSystem : MonoBehaviour {

    [SerializeField]
    private RespawnPoints[] spawnPoints;
    private int selectedSpawn;

    public void Respawn(Transform target)
    {
        for (int i = spawnPoints.Length - 1; i >= 0; i--)
        {
            if (spawnPoints[i].hasPassed)
            {
                selectedSpawn = i;
                break;
            }
        }
        target.position = spawnPoints[selectedSpawn].transform.position;
    }
}
