using UnityEngine;
using System.Collections;

public class DamageFeedback : MonoBehaviour {

	public float fadeSpeed = 1.5f;

	private bool fadeStarter = true;

	void Awake(){
		GUI.DrawTexture (0f, 0f, Screen.width, Screen.height);
	}
}
