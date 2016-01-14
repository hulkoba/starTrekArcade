using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {


	public GameObject asteroidExplosion;

	public AudioClip explosionSound;
	AudioSource audioSource;

	void Awake() {
		audioSource = GetComponent<AudioSource>();
	}

	void OnTriggerEnter(Collider other) {
	    // not destroying the Boundary!
		if (other.tag == "Boundary") {
	        return;
	    }

		if (other.tag == "Enemy" || other.tag == "MainCamera") {
			//instantiate an explosion at the same position as the asteroid
			Instantiate(asteroidExplosion, transform.position, transform.rotation);

			PlayExplosionSound();
			Destroy(gameObject);
		}

		if(other.tag == "Bolt") {
			//instantiate an explosion at the same position as the asteroid
			Instantiate(asteroidExplosion, transform.position, transform.rotation);
			PlayExplosionSound();
			Destroy(gameObject);
			Destroy(other.gameObject);
		}
	}

	private void PlayExplosionSound() {
		audioSource.clip = explosionSound;
		audioSource.Play();
	}

}
