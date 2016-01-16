
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text;
using System.IO;
using System.Collections.Generic;

public class Score : MonoBehaviour {

	private string[] scoreboard;
	List<string> scores = new List<string>();
	public GUISkin menuSkin;   //custom GUIskin reference
	private GameObject m_TextObject;

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

			using (theReader)
			{
				do
				{
					line = theReader.ReadLine();
					
					if (line != null)
					{
						scores.Add(line);
					}
				}
				while (line != null);
				// Done reading, close the reader and return true to broadcast success    
				theReader.Close();
			}
		}
		// If anything broke in the try block, we throw an exception with information
		// on what didn't work
		catch 
		{
			Debug.Log("error in fileload");
		}

	}

	private void OnGUI()
	{
		
		GUI.skin = menuSkin;   //use the custom GUISkin
		menuSkin.button.fontSize = (int)fontSize; //set the fontsize of the button 
		menuSkin.box.fontSize = (int)fontSize; //set the font size of box
		string [] scoreArray = scores.ToArray();
		if (scoreArray.Length == 0) {
			Debug.Log ("HIGHSCORE LEER");
		} else {
			for (int position = 0; position < scoreArray.Length; position++) {
				GUI.TextField (new Rect (Screen.width / 3, position * Screen.height / 12 + 155, 2 * Screen.width / 16, Screen.height / 16), "" + (position + 1) + ". " + scoreArray [position]);
			}
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
						scores.Add(line);
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

		string[] helper = scores.ToArray ();
		System.IO.File.WriteAllLines (path,helper);
	}
}
//Dictionary<TKey, TValue>() auch zum Sortieren wird passen.
/*Debug.Log (line);
		Debug.Log (line.IndexOf(":"));
		Debug.Log (line.Substring(line.IndexOf (":")+1,line.Length-line.IndexOf(":")-1));
		Debug.Log (int.Parse(line.Substring(line.IndexOf (":")+1,line.Length-line.IndexOf(":")-1)));
		*/