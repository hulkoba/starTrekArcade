﻿using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {

	public GameObject explosion;
	public GameObject playerExplosion;

	//script
	private GameController gameController;

	void Start () {
        GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
        if (gameControllerObject != null) {
            gameController = gameControllerObject.GetComponent <GameController>();
        } else {
			Debug.Log ("Cannot find 'GameController' script");
		}
    }

	void OnTriggerEnter(Collider other) {
	    // not destroying the Boundary!
		if (other.tag == "Boundary") {
	            return;
	    }

		//instantiate an explosion at the same position as the asteroid
		Instantiate(explosion, transform.position, transform.rotation);

		// in FPS Player = mainCamera
		//if (other.tag == "mainCamera"){
			// game over by explosion of the enterprise
            //Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            //gameController.GameOver ();
        //}


		gameController.AddScore(1);
		//destroy the shot and the asteroid...
	    Destroy(other.gameObject);
	    Destroy(gameObject);

		// 	if (other.tag == "Enemy") {
		//
		// 	}
		// 	else if(other.tag == "Astroid"){
		//
		// 	}
	}


	// void OnTriggerStay(Collider other) {
	// 	//if (other.attachedRigidbody) {
	// 	//	other.attachedRigidbody.AddForce (Vector3.up * 10);
	// 	//}
	// }
	//
	// void OnTriggerExit(Collider other) {
	// 	// Destroy everything that leaves the trigger
	// 	//Destroy(other.gameObject);
	// }
}
