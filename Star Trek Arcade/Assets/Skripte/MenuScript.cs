using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuScript : MonoBehaviour {

	public GUISkin menuSkin;   //custom GUIskin reference
	float virtualWidth = 960.0f; //width of the device you're using
	float virtualHeight = 540.0f; //height of the device
	public float fontSize = 27; //preferred fontsize for this screen size
	public int value = 20;  //factor value for changing fontsize if needed
	public Button playButton;
	public Button highscoreButton;
	public Button exitButton;

	// Use this for initialization
	void Start () {
		playButton.onClick.AddListener(() => StartGame());
		highscoreButton.onClick.AddListener (() => ShowHighscore ());
		exitButton.onClick.AddListener (() => ExitGame());
		//check if the size on which game is being played is different
		if (virtualWidth != Screen.width || virtualHeight != Screen.height) {
			//set the new screen sizes if different
			virtualWidth = Screen.width;
			virtualHeight = Screen.height;
			//screen size dependent font size calculation
			fontSize = Mathf.Min(Screen.width, Screen.height) / value;
		}
	}

	private void StartGame(){
		Debug.Log ("Start Game");
		Application.LoadLevel(1); //open the game scene
	}

	private void ShowHighscore(){
		Debug.Log ("ShowHighscore");
		Application.LoadLevel(3); // open the credits scene
	}

	private void ExitGame(){
		Debug.Log ("Exit Game");
		Application.Quit(); // exit the game
	}
}
