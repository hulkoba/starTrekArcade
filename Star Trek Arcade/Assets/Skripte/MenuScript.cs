using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuScript : MonoBehaviour {

	public GUISkin menuSkin;   //custom GUIskin reference
	public string gameLevel; //level to open on clicking Play button
	public string highscorePage;
	float virtualWidth = 960.0f; //width of the device you're using
	float virtualHeight = 540.0f; //height of the device
	public float fontSize = 27; //preferred fontsize for this screen size
	public int value = 20;  //factor value for changing fontsize if needed
	public Button playButton;

	// Use this for initialization
	void Start () {
		//check if the size on which game is being played is different
		if (virtualWidth != Screen.width || virtualHeight != Screen.height) {
			//set the new screen sizes if different
			virtualWidth = Screen.width;
			virtualHeight = Screen.height;
			//screen size dependent font size calculation
			fontSize = Mathf.Min(Screen.width, Screen.height) / value;
		}
	}

	private void OnGUI() {


		playButton.onClick.AddListener(() => StartGame());
		/*GUI.skin = menuSkin;   //use the custom GUISkin
		menuSkin.button.fontSize = (int)fontSize; //set the fontsize of the button
		menuSkin.box.fontSize = (int)fontSize; //set the font size of box

		//create a menu
		GUI.Box(new Rect(Screen.width/8, 10, 3*Screen.width/4, 3*Screen.height/4), "MAIN MENU"); //a box to hold all the buttons

		if (GUI.Button(new Rect(Screen.width/4, Screen.height/8+10, 2*Screen.width/4, Screen.height/8), "PLAY")){
			Application.LoadLevel(gameLevel); //open the game scene
		}

		if (GUI.Button(new Rect(Screen.width/4, 3*Screen.height/8+10, 2*Screen.width/4, Screen.height/8), "HIGHSCORE")){
			Application.LoadLevel(highscorePage); // open the credits scene
		}

		if (GUI.Button(new Rect(Screen.width/4, 4*Screen.height/8+10, 2*Screen.width/4, Screen.height/8), "EXIT")){
			Application.Quit(); // exit the game
		}*/
	}

	private void StartGame(){
		Debug.Log ("Start Game");
		Application.LoadLevel(gameLevel); //open the game scene
	}

	private void ShowHighscore(){
		Debug.Log ("ShowHighscore");
		Application.LoadLevel(highscorePage); // open the credits scene
	}

	private void ExitGame(){
		Debug.Log ("Exit Game");
		Application.Quit(); // exit the game
	}
}
