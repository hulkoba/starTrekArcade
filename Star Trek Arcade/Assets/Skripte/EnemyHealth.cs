using UnityEngine;
using System.Collections;

public class EnemyHealth : HealthController {

    private EnemyRadar enemyRadar;
	private GameObject GameController;

	// Use this for initialization
	void Start () {
		GameController = GameObject.Find("Game_Controller");
		//dying ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
