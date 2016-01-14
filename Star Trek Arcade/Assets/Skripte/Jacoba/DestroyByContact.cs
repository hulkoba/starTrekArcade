using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {

	public GameObject playerExplosion;
	public GameObject enemyExplosion;
	public GameObject asteroidExplosion;

	public AudioClip explosionSound;
	AudioSource audioSource;

	PlayerHealth playerHealth;
	EnemyHealth enemyHealth;

	void Awake() {
		audioSource = GetComponent<AudioSource>();

		playerHealth = GetComponent<PlayerHealth> ();
		enemyHealth = GetComponent<EnemyHealth>();
	}

	void DestroyAll (GameObject other) {
		PlayExplosionSound();
		//destroy the shot and the asteroid...
		Destroy(other);
		Destroy(gameObject);
	}

	void OnTriggerEnter(Collider other) {
	    // not destroying the Boundary!
		if (other.tag == "Boundary") {
	            return;
	    }

		if (other.tag == "Asteroid") {
			//instantiate an explosion at the same position as the asteroid
			Instantiate(asteroidExplosion, other.transform.position, other.transform.rotation);

			DestroyAll(other.gameObject);
		}

		if (other.tag == "Enemy") {
			//instantiate an explosion at the same position as the bolt
			Instantiate(asteroidExplosion, other.transform.position, other.transform.rotation);

			Destroy(gameObject);
			if(enemyHealth.currentHealth <= 0 ){
				Instantiate(enemyExplosion, other.transform.position, other.transform.rotation);
				// Increase the score 1 for enemy
				ScoreManager.score += 30;
				DestroyAll (other.gameObject);
			}
		}

		// in FPS Player = mainCamera
		if (other.tag == "mainCamera"){
			// game over by explosion of the enterprise
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            Destroy(gameObject);

			if(playerHealth.currentHealth > 0) {
			 	playerHealth.ApplyDamage (10);
			} else {
				DestroyAll (other.gameObject);
				//gameController.GameOver ();
			}
        }
	}

	private void PlayExplosionSound() {
		audioSource.clip = explosionSound;
		audioSource.Play();
	}

}
