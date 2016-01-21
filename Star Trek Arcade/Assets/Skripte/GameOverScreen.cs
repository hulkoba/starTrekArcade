using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text;
using System.IO;
using System.Collections.Generic;

public class GameOverScreen : MonoBehaviour {
	
	public InputField playerName;
	public Text scoreShow;
	public Button submitButton;
	public Button restartButton;

	private List<KeyValuePair<int,KeyValuePair<string,string>>> highscoreList = new List<KeyValuePair<int,KeyValuePair<string,string>>> ();

	public string fileName;

	private string path;
	private int endScore;
	private bool nameEnterd = false;

	// Use this for initialization
	void Start () {
		submitButton.onClick.AddListener (() => SubmitMainmenu ());
		restartButton.onClick.AddListener (() => RestartGame ());

		fileName = "score.txt";
		path = Application.dataPath+@"/Skripte/Highscore/scores/"+fileName;

		endScore = PlayerPrefs.GetInt ("endScore");
		scoreShow.text = "Score: "+endScore;
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
			nameEnterd = true;
		} else {
			Debug.Log ("EMPTY INPUTFIELD!");
		}
	}

	void RestartGame(){
		WriteScore ();
		if (nameEnterd) {
			Application.LoadLevel (1);
		}
	}

	void SubmitMainmenu(){
		WriteScore ();
		if (nameEnterd) {
			Application.LoadLevel (2);
		}
	}
}
