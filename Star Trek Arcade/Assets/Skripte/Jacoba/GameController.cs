using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject hazard;
	private float range = 15f;

	public int hazardCount;
    public float spawnWait; // wait time value
    public float startWait;
    public float waveWait;

	void Start() {
		//
		StartCoroutine (SpawnWaves ());
	}

	//spawning the hazards in game
	IEnumerator SpawnWaves () {

		// short pause at gamestart
		yield return new WaitForSeconds (startWait);
        while (true) {
            for (int i = 0; i < hazardCount; i++) {
				// create a hazard(Asteroid or enemy) in a random position in given range
				Vector3 spawnPosition = new Vector3(Random.Range(-range, range), Random.Range(-range, range),Random.Range(-range, range) );
				// instantiate with no rotation
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate(hazard, spawnPosition, spawnRotation);

                yield return new WaitForSeconds (spawnWait);
            }
            yield return new WaitForSeconds (waveWait);
        }
	}
}
