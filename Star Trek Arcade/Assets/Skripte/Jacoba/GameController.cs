using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text;
using System.IO;

public class GameController : MonoBehaviour {

	public GameObject Asteroid;
	public GameObject Enemy;
	private Transform Enterprise;

	private float range = 20f;

	public int hazardCount;
    public float spawnWait = 2f; // wait time value
    public float waveWait;

	private int endScore;

	void Start() {
		Enterprise = GameObject.Find("Enterprise").transform;
		//StartCoroutine (SpawnWaves ());
	}

	//spawning the hazards in game
	/*IEnumerator SpawnWaves () {
		// short pause at gamestart
		yield return new WaitForSeconds (spawnWait);
        while (true) {
            for (int i = 0; i < hazardCount; i++) {
				// create a hazard(Asteroid or enemy) in a random position in given range
				Quaternion spawnRotation = Quaternion.identity;
				// instantiate with no rotation

				var chooser = Random.Range(0,2);
				if(chooser < 1){
					Vector3 spawnPosition = new Vector3(Random.Range(Enterprise.position.x -range, Enterprise.position.x + range), 0, Random.Range(Enterprise.position.z -range, Enterprise.position.z + range));
					Instantiate(Enemy, spawnPosition, spawnRotation);
				} else{
					Vector3 spawnPosition = new Vector3(Random.Range(Enterprise.position.x -range, Enterprise.position.x + range), Random.Range(Enterprise.position.y -range, Enterprise.position.y + range), Random.Range(Enterprise.position.z -range, Enterprise.position.z + range));

					GameObject asteroid = Instantiate(Asteroid, spawnPosition, spawnRotation) as GameObject;
					float scale = Random.Range(1,8);
					asteroid.transform.localScale = new Vector3(scale, scale, scale);
				}

                yield return new WaitForSeconds (spawnWait);
            }
            yield return new WaitForSeconds (waveWait);
        }
	}*/

	public void EndSequence(){
		
		DestroyAll("Enemy");
		DestroyAll ("Asteroid");
		endScore = ScoreManager.score;
		PlayerPrefs.SetInt ("endScore", endScore);
		Application.LoadLevel (2);
	}

	private void DestroyAll(string type){
		GameObject [] gameObjects = GameObject.FindGameObjectsWithTag (type);
		
		for(var i = 0 ; i < gameObjects.Length ; i ++) {
			Destroy(gameObjects[i]);
		}
	}

}
