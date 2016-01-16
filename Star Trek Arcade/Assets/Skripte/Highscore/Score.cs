
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
	private SortedDictionary<string,KeyValuePair<string,int>> highscoreDict = new SortedDictionary<string,KeyValuePair<string,int>> ();

	public float fontSize = 27; //preferred fontsize for this screen size
	public int value = 20;  //factor value for changing fontsize if needed

	// Use this for initialization
	void Start () {
		Load ("test.txt");
		//writeHighscore ("test.txt");

		fontSize = Mathf.Min(Screen.width, Screen.height) / value;
	}

	private void Load(string fileName)
	{

		try
		{
			string line;

			string path = Application.dataPath+@"/Skripte/Highscore/scores/"+fileName;
			StreamReader theReader = new StreamReader(path, Encoding.Default);
			int position = 2;
			using (theReader)
			{
				do
				{
					line = theReader.ReadLine();
					
					if (line != null)
					{
						string[] stringArray = line.Split(':');
						highscoreDict.Add(position.ToString(),new KeyValuePair<string, int>(stringArray[0],int.Parse(stringArray[1])));
						position++;
					}
				}
				while (line != null);
				// Done reading, close the reader and return true to broadcast success    
				theReader.Close();
				highscoreDict.Add("1",new KeyValuePair<string, int>("SortiertVonSelbst!",13));
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
		foreach(KeyValuePair<string,KeyValuePair<string,int>> entry in highscoreDict)
		{
			GUI.TextField (new Rect (Screen.width / 3, position * Screen.height / 12 + 115, 2 * Screen.width / 12, Screen.height / 16), "" + position + ". " + entry.Value.Key +" : "+entry.Value.Value.ToString());
			position++;
		}


	}

	private void writeHighscore(string fileName){

		string path = Application.dataPath+@"/Skripte/Highscore/scores/"+fileName;

		try
		{
			string line;
			StreamReader theReader = new StreamReader(path, Encoding.Default);
			
			using (theReader)
			{
				do
				{
					line = theReader.ReadLine();
					
					if (line != null)
					{

					}
					else {
						Debug.Log ("Noch kein Highscore erstellt");
					}
				}
				while (line != null);
				// Done reading, close the reader and return true to broadcast success    
				theReader.Close();
			}
		}
				catch 
		{
			Debug.Log("error in fileload");
		}

		//System.IO.File.WriteAllLines (path,stringLine);
	}
}
//Dictionary<TKey, TValue>() auch zum Sortieren wird passen.
/*Debug.Log (line);
		Debug.Log (line.IndexOf(":"));
		Debug.Log (line.Substring(line.IndexOf (":")+1,line.Length-line.IndexOf(":")-1));
		Debug.Log (int.Parse(line.Substring(line.IndexOf (":")+1,line.Length-line.IndexOf(":")-1)));
		*/