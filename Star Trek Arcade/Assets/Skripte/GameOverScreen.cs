using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text;
using System.IO;
using System.Collections.Generic;

public class GameOverScreen : MonoBehaviour {
	
	public InputField playerNameField;
	public Text scoreShow;
	public Button submitButton;
	public Button restartButton;

	private List<KeyValuePair<string,KeyValuePair<string,string>>> highscoreList = new List<KeyValuePair<string,KeyValuePair<string,string>>> ();

	public string fileName;

	private string path;
	private int endScore;
	private string playerName;
	private bool changed = false;

	// Use this for initialization
	void Start () {
		submitButton.onClick.AddListener (() => SubmitMainmenu ());
		restartButton.onClick.AddListener (() => RestartGame ());

		fileName = "score.txt";
		path = Application.dataPath+@"/"+fileName;

		endScore = PlayerPrefs.GetInt ("endScore");
		scoreShow.text = "Score: "+endScore;
	}

	void RestartGame(){
		saveHighscoreInit ();
		Application.LoadLevel (1);

	}

	void SubmitMainmenu(){
		saveHighscoreInit ();
		Application.LoadLevel (0);
	}



	void readerOfFile(string filePath){
		string line;
		StreamReader theReader = new StreamReader (path, Encoding.Default);
		using (theReader) {
			do {
				line = theReader.ReadLine ();
				if (line != null) {
					string[] stringArray = line.Split (':');
					if(stringArray.Length == 3){
						changed = false;
						KeyValuePair<string,KeyValuePair<int,string>> helper = new KeyValuePair<string,KeyValuePair<int,string>>(stringArray[0],new KeyValuePair<int,string>(int.Parse(stringArray[1]),stringArray[2]));
						ListSorting(helper);
					}
				}
			} while (line != null);
			// Done reading, close the reader and return true to broadcast success    
			theReader.Close ();
		}
	}

	void writerOfFile(){
		KeyValuePair<string,KeyValuePair<int,string>> helper = new KeyValuePair<string,KeyValuePair<int,string>>(playerName,new KeyValuePair<int,string>(endScore,"new"));
		ListSorting(helper);
		string[] stringLine = new string[7];
		int position = 0;
		foreach (var listItems in highscoreList) {
			if(position >= 7){
				break;
			}
			else{
				stringLine [position] = ""+listItems.Key+":"+listItems.Value.Key+":"+listItems.Value.Value;
				position++;
			}
		}
		System.IO.File.WriteAllLines(path, stringLine);
	}

	void ListSorting(KeyValuePair<string,KeyValuePair<int,string>> values){
		if (highscoreList.Count == 0) {
			highscoreList.Add(new KeyValuePair<string, KeyValuePair<string, string>>(values.Key,new KeyValuePair<string, string>(values.Value.Key.ToString (),values.Value.Value)));
		} else {
			int index = highscoreList.Count;
			foreach (var oldValue in highscoreList) {
				if(int.Parse(oldValue.Value.Key) <= values.Value.Key && !changed){
					index = highscoreList.IndexOf(oldValue);
					changed = true;
				}
			}
			if(changed){
				highscoreList.Insert(index,new KeyValuePair<string, KeyValuePair<string, string>>(values.Key,new KeyValuePair<string, string>(values.Value.Key.ToString (),values.Value.Value)));
			} else{
				highscoreList.Add(new KeyValuePair<string, KeyValuePair<string, string>>(values.Key,new KeyValuePair<string, string>(values.Value.Key.ToString (),values.Value.Value)));
			}
		}
	}

	void saveHighscoreInit(){
		if (playerNameField.text == "") {
			playerName = "Player";
		} else {
			playerName = playerNameField.text;
		}
		if (System.IO.File.Exists (path)) {
			readerOfFile (path);
			writerOfFile ();
		} else {
			writerOfFile();
		}
	}
	
}
