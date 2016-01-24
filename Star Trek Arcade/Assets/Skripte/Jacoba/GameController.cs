using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	int endScore;

	public bool frozen;

	public GameObject Starbase;

	void Start() {
		frozen = false;

		//create a starbase (2nd minute)
		CreateStarbase();
		InvokeRepeating ("CreateStarbase", 120f, 120f);
	}

	void CreateStarbase() {

		if(GameObject.Find("Starbase(Clone)") == null) {
			// CREATE ONE STARBASE :: 66=Boundary size
			Vector3 pos = new Vector3(
				Random.Range(-66, 66),
				Random.Range(-66, 66),
				Random.Range(-66, 66));

			Instantiate (Starbase, pos, Quaternion.identity);
		}
	}

	public void EndSequence(){
		frozen = true;
	//	DestroyAll("Enemy");
	//	DestroyAll ("Asteroid");
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
