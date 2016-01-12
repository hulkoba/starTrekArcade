using UnityEngine;
using System.Collections;

public class HazardManager : MonoBehaviour {
    public GameObject Hazard;

    public PlayerHealth playerHealth;
    public float spawnWait = 2f; // wait time value

    public Transform[] spawnPoints;

    void Start() {
        // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
        InvokeRepeating ("Spawn", spawnWait, spawnWait);
    }

    void Spawn () {
        if(playerHealth.currentHealth <= 0f) {
            // ... exit the function.
            return;
        }

        // Find a random index between zero and one less than the number of spawn points.
        int spawnPointIndex = Random.Range (0, spawnPoints.Length);
        Instantiate (Hazard, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }
}
