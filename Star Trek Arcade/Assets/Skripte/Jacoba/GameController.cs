using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text;
using System.IO;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

	public GameObject Asteroid;
	public GameObject Enemy;
	private Transform Enterprise;

	private float range = 20f;

	public int hazardCount;
    public float spawnWait = 2f; // wait time value
    public float waveWait;

	//GameOverScreen
	public GameObject GameOverScreen;
	public GameObject InnerHUDObject;
	public InputField playerName;
	public Text scoreShow;
	private int endScore;
	public Button submitButton;
	private List<KeyValuePair<int,KeyValuePair<string,string>>> highscoreList = new List<KeyValuePair<int,KeyValuePair<string,string>>> ();
	public string fileName;
	private string path;
	bool theEnd = false;

	void Start() {
		Enterprise = GameObject.Find("Enterprise").transform;
		//StartCoroutine (SpawnWaves ());
		fileName = "test.txt";
		path = Application.dataPath+@"/Skripte/Highscore/scores/"+fileName;
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
		InnerHUDObject.SetActive (false);
		
		DestroyAll("Enemy");
		DestroyAll ("Asteroid");
		endScore = ScoreManager.score;
		scoreShow.text = "Score: "+endScore;
		GameOverScreen.SetActive (true);
		theEnd = true;
		playerName.ActivateInputField ();
		playerName.Select();
		submitButton.onClick.AddListener (() => WriteScore ());
	}

	private void DestroyAll(string type){
		GameObject [] gameObjects = GameObject.FindGameObjectsWithTag (type);
		
		for(var i = 0 ; i < gameObjects.Length ; i ++) {
			Destroy(gameObjects[i]);
		}
	}

	void WriteScore(){
		if (playerName.text != "") {
			SortedList<int,KeyValuePair<string,string>> helperList = new SortedList<int, KeyValuePair<string,string>> ();		
			try {
				string line;
				StreamReader theReader = new StreamReader (path, Encoding.Default);
				int position = 1;
				using (theReader) {
					do {
						line = theReader.ReadLine ();				
						if (line != null) {
							string[] stringArray = line.Split (':');
							helperList.Add (int.Parse (stringArray [1]), new KeyValuePair<string, string> (stringArray [0], stringArray [2]));
							position++;
						}
					} while (line != null);
					// Done reading, close the reader and return true to broadcast success    
					theReader.Close ();
					helperList.Add (endScore, new KeyValuePair<string, string> (playerName.text, "new"));
					for (int i = 0; i < 7; i++) {
						highscoreList.Add (new KeyValuePair<int,KeyValuePair<string,string>> (helperList.Keys [helperList.Count - 1], new KeyValuePair<string,string> (helperList.Values [helperList.Count - 1].Key, helperList.Values [helperList.Count - 1].Value)));
						helperList.RemoveAt (helperList.Count - 1);
					}
				}
			} catch {
				Debug.Log ("error in fileload in LOADING");
			}
			string[] stringLine = new string[7];
			for (int i = 0; i < 7; i++) {
				stringLine [i] = highscoreList [i].Value.Key + ":" + highscoreList [i].Key.ToString () + ":" + highscoreList [i].Value.Value;
			}
			
			System.IO.File.WriteAllLines (path, stringLine);
		} else {
			Debug.Log ("EMPTY INPUTFIELD!");
		}
	}

	void OnGUI(){
		if (theEnd) {
			GUI.FocusControl("NicknameInput");
		}
	}

}
