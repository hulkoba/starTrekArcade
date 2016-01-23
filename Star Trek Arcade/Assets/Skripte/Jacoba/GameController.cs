using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	int endScore;

	public GameObject Starbase;

	void Start() {

		// CREATE ONE STARBASE
		Vector3 pos = new Vector3(
			Random.Range(-66, 66),
			Random.Range(-66, 66),
			Random.Range(-66, 66));
		Instantiate (Starbase, pos, Quaternion.identity);
	}

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
