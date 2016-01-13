using UnityEngine;
using System.Collections;
using System.Text;
using System.IO;
using System.Collections.Generic;

public class Score : MonoBehaviour {

	private string[] scoreboard;
	List<string> scores = new List<string>();
	public GUISkin menuSkin;   //custom GUIskin reference

	// Use this for initialization
	void Start () {
		Load ("test.txt");
		writeHighscore ("test.txt");
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
						//string[] entries = line.Split(':');
						//if (entries.Length > 0)
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

	private void OnGui(){
		GUI.skin = menuSkin;   //use the custom GUISkin
		GUI.color = Color.blue;
		int position = 1;
		string [] scoreArray = scores.ToArray();
		while (position < scoreArray.Length) {
			GUI.TextField (new Rect(Screen.width/4, 4*Screen.height/8+10, 2*Screen.width/4, Screen.height/8),""+position+". WUHUUUU"+scoreArray[position-1]);
		}
	}

	private void writeHighscore(string fileName){
		string path = Application.dataPath+@"/Skripte/Highscore/scores/"+fileName;
		string[] helper = scores.ToArray ();
		System.IO.File.WriteAllLines (path,helper);
	}
}
