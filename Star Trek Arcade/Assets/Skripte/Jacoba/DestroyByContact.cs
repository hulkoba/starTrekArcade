using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {

	public GameObject explosion;

	public AudioClip explosionSound;
	AudioSource audioSource;

	void Awake() {
		audioSource = GetComponent<AudioSource>();
	}


	void OnTriggerEnter(Collider other) {
	    // not destroying the Boundary or an enemy!
		if (other.tag == "Boundary" || other.tag == "Enemy") {
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

		// Increase the score 1 for asteroid
		ScoreManager.score += 1;

		PlayExplosionSound();
		//destroy the shot and the asteroid...
	    Destroy(other.gameObject);
	    Destroy(gameObject);
	}

	// void OnTriggerStay(Collider other) {
	// 	if (other.attachedRigidbody) {
	// 		other.attachedRigidbody.AddForce (Vector3.up * 10);
	// 	}
	// }

	private void PlayExplosionSound() {
		audioSource.clip = explosionSound;
		audioSource.Play();
	}

}
