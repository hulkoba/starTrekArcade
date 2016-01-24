using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour {

    public static int score;
    Text text;

    void Awake () {
        text = GetComponent <Text> ();
        score = 0;
    }

    void Update () {
        // Set the displayed text to be the word "Score" followed by the score value.
		if (text.tag == "Score") {
			text.text = "Score: " + score;
		}
    }
}
