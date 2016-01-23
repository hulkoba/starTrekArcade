
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text;
using System.IO;
using System.Collections.Generic;

public class Score : MonoBehaviour {

	private string[] scoreboard;
	public GUISkin menuSkin;   //custom GUIskin reference
	private GameObject m_TextObject;
	private List<KeyValuePair<string,KeyValuePair<string,string>>> highscoreList = new List<KeyValuePair<string,KeyValuePair<string,string>>> ();

	public float fontSize = 27; //preferred fontsize for this screen size
	public int value = 20;  //factor value for changing fontsize if needed

	public string fileName;
	private string path;

	private bool isNew = true;
	private bool emptyScore = true;

	public Button backToMainmenuButton;

	// Use this for initialization
	void Start () {
		backToMainmenuButton.onClick.AddListener (() => backToMainMenu ());

		fileName = "score.txt";
		path = Application.dataPath+@"/"+fileName;
		fontSize = Mathf.Min(Screen.width, Screen.height) / value;

		if(System.IO.File.Exists(path)){
			readerOfFile (path);
		}
		else{
			emptyScore = true;
		}
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
						KeyValuePair<string,KeyValuePair<string,string>> helper = new KeyValuePair<string,KeyValuePair<string,string>>(stringArray[0],new KeyValuePair<string,string>(stringArray[1],stringArray[2]));
						highscoreList.Add(helper);
						emptyScore = false;
					}
				}
			} while (line != null);
			// Done reading, close the reader and return true to broadcast success    
			theReader.Close ();
		}
	}

	private void OnGUI()
	{
		
		GUI.skin = menuSkin;   //use the custom GUISkin
		menuSkin.button.fontSize = (int)fontSize; //set the fontsize of the button 
		menuSkin.box.fontSize = (int)fontSize; //set the font size of box
		int position = 1;
		GUIStyle highlite = menuSkin.GetStyle ("highlite");
		if (emptyScore) {
			GUI.TextField (new Rect (7*Screen.width / 20, position * 3 * Screen.height / 12 + 155, 2 * Screen.width / 6, Screen.height / 16), "No highscore created. Play more.");
		}
		else{
			foreach(KeyValuePair<string,KeyValuePair<string,string>> entry in highscoreList)
			{
				if(entry.Value.Value == "new"){
					GUI.TextField (new Rect (11*Screen.width / 25, position * Screen.height / 12 + 155, 2 * Screen.width / 6, Screen.height / 16), "" + position + ". " + entry.Key +" : "+entry.Value.Key,highlite);
					isNew = true;
				}
				else{
					GUI.TextField (new Rect (11*Screen.width / 25, position * Screen.height / 12 + 155, 2 * Screen.width / 6, Screen.height / 16), "" + position + ". " + entry.Key +" : "+entry.Value.Key);
				}
				position++;
			}
		}
	}

	private void overwriteNew(){
		for (int i = 0; i < highscoreList.Count; i++) {
			highscoreList[i] = new KeyValuePair<string, KeyValuePair<string, string>>(highscoreList[i].Key,new KeyValuePair<string, string>(highscoreList[i].Value.Key,"null"));
		}
	}

	private void writeHighscore(){

		if (!emptyScore) {
			if (isNew) {
				overwriteNew();
			}
			string[] stringLine = new string[7];
			for (int i = 0; i < highscoreList.Count; i++) {
				stringLine[i] = highscoreList[i].Key+":"+highscoreList[i].Value.Key+":"+highscoreList[i].Value.Value;
			}
			
			System.IO.File.WriteAllLines (path,stringLine);
		}
	}

	private void backToMainMenu(){
		writeHighscore ();
		Application.LoadLevel (0);
	}
}
