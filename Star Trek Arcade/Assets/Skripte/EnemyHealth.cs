using UnityEngine;
using System.Collections;

public class EnemyHealth : HealthController {

    private EnemyRadar enemyRadar;
	private GameObject GameController;

	IEnumerator dying(){
		// wait for some time
		//yield return new WaitForSeconds (4f);
		//GameController.GetComponent<CreationScript>().enemyCounter -= 1;
		//Destroy (this.gameObject);
	}

	// Use this for initialization
	void Start () {
		GameController = GameObject.Find("Game_Controller");
		//dying ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
