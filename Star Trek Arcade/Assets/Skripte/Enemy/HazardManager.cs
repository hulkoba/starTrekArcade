using UnityEngine;
using System.Collections;

public class HazardManager : MonoBehaviour {
    public GameObject Hazard;

    public bool scalable;

    public PlayerHealth playerHealth;
    public float spawnWait = 2f; // wait time value

    public Transform[] spawnPoints;

    void Start() {
        // Call the Spawn function after a delay of the spawnTime and then
        // continue to call after the same amount of time.
        InvokeRepeating ("Spawn", spawnWait, spawnWait);
    }

    void Spawn () {
        if(playerHealth.currentHealth <= 0f) {
            return;
        }

        // Find a random index between zero and one less than the number of spawn points.
        int spawnPointIndex = Random.Range (0, spawnPoints.Length);

        if(scalable == false) {
            Instantiate (Hazard, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        } else {
            GameObject asteroid = Instantiate(Hazard, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation) as GameObject;
            float scale = Random.Range(1,8);
            asteroid.transform.localScale = new Vector3(scale, scale, scale);
        }
    }
}
