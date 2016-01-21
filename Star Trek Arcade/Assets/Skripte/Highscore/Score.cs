
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
	private List<KeyValuePair<int,KeyValuePair<string,string>>> highscoreList = new List<KeyValuePair<int,KeyValuePair<string,string>>> ();

	public float fontSize = 27; //preferred fontsize for this screen size
	public int value = 20;  //factor value for changing fontsize if needed

	public string fileName;
	private string path;

	private bool isNew = true;

	public Button backToMainmenuButton;

	// Use this for initialization
	void Start () {
		backToMainmenuButton.onClick.AddListener (() => backToMainMenu ());

		fileName = "score.txt";
		path = Application.dataPath+@"/Skripte/Highscore/scores/"+fileName;
		Load ();
		//writeHighscore ("test.txt");

		fontSize = Mathf.Min(Screen.width, Screen.height) / value;
	}

	private void Load()
	{

		SortedList<int,KeyValuePair<string,string>> helperList = new SortedList<int, KeyValuePair<string,string>> ();

		try
		{
			string line;


			StreamReader theReader = new StreamReader(path, Encoding.Default);
			int position = 1;
			using (theReader)
			{
				do
				{
					line = theReader.ReadLine();
					
					if (line != null)
					{
						string[] stringArray = line.Split(':');
						helperList.Add(int.Parse(stringArray[1]),new KeyValuePair<string, string>(stringArray[0],stringArray[2]));
						position++;
					}
				}
				while (line != null);
				// Done reading, close the reader and return true to broadcast success    
				theReader.Close();

				for(int i = 0; i < 7; i++){
					highscoreList.Add(new KeyValuePair<int,KeyValuePair<string,string>>(helperList.Keys[helperList.Count-1],new KeyValuePair<string,string>(helperList.Values[helperList.Count-1].Key,helperList.Values[helperList.Count-1].Value)));
					helperList.RemoveAt(helperList.Count-1);
				}
			}
		}
		// If anything broke in the try block, we throw an exception with information
		// on what didn't work
		catch 
		{
			Debug.Log("error in fileload in LOADING");
		}

	}

	private void OnGUI()
	{
		
		GUI.skin = menuSkin;   //use the custom GUISkin
		menuSkin.button.fontSize = (int)fontSize; //set the fontsize of the button 
		menuSkin.box.fontSize = (int)fontSize; //set the font size of box
		int position = 1;
		GUIStyle highlite = menuSkin.GetStyle ("highlite");
		foreach(KeyValuePair<int,KeyValuePair<string,string>> entry in highscoreList)
		{
			if(entry.Value.Value == "new"){
				GUI.TextField (new Rect (11*Screen.width / 24, position * Screen.height / 12 + 155, 2 * Screen.width / 12, Screen.height / 16), "" + position + ". " + entry.Value.Key +" : "+entry.Key.ToString(),highlite);
			}
			else{
				GUI.TextField (new Rect (11*Screen.width / 24, position * Screen.height / 12 + 155, 2 * Screen.width / 12, Screen.height / 16), "" + position + ". " + entry.Value.Key +" : "+entry.Key.ToString());
			}
			position++;
		}

		//writeHighscore ();
	}

	private void overwriteNew(){
		for (int i = 0; i < 7; i++) {
			highscoreList[i] = new KeyValuePair<int, KeyValuePair<string, string>>(highscoreList[i].Key,new KeyValuePair<string, string>(highscoreList[i].Value.Key,"null"));
		}
	}

	private void writeHighscore(){

		if (isNew) {
			overwriteNew();
		}
		string[] stringLine = new string[7];
		for (int i = 0; i < 7; i++) {
			stringLine[i] = highscoreList[i].Value.Key+":"+highscoreList[i].Key.ToString()+":"+highscoreList[i].Value.Value;
		}

		System.IO.File.WriteAllLines (path,stringLine);
	}

	private void backToMainMenu(){
		writeHighscore ();
		Application.LoadLevel ("menuScene");
	}
}
//Dictionary<TKey, TValue>() auch zum Sortieren wird passen.
/*Debug.Log (line);
		Debug.Log (line.IndexOf(":"));
		Debug.Log (line.Substring(line.IndexOf (":")+1,line.Length-line.IndexOf(":")-1));
		Debug.Log (int.Parse(line.Substring(line.IndexOf (":")+1,line.Length-line.IndexOf(":")-1)));
		*/