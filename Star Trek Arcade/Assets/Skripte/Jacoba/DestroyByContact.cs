using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {

	public GameObject asteroidExplosion;
	public GameObject Asteroid;
	Rigidbody body;

//	public AudioClip explosionSound;
//	AudioSource audioSource;

	void Awake() {
//		audioSource = GetComponent<AudioSource>();
		body = gameObject.GetComponent<Rigidbody> ();
	}

	void OnTriggerEnter(Collider other) {
	    // not destroying the Boundary!
		if (other.tag == "Boundary") {
	        return;
	    }

		if(other.tag == "Asteroid" || other.tag == "Enemy") {
			// Asteroiden prallen aneinander ab
			body.AddForce((transform.forward * -1) * 10);
		}

		if (other.tag == "MainCamera" || other.tag == "Starbase") {
			Vector3 helper = other.gameObject.transform.forward;
			body.AddForce(new Vector3(helper.x * 10f,
				helper.y * 10f,
				helper.z * 10f),
				ForceMode.Impulse);

		}

		if(other.tag == "Bolt" || other.tag == "EnterpriseBolt") {
			//PlayExplosionSound();
			Explode(other);

			// if a huge asteroid is shot, it splits into 2
			if(transform.localScale.x >= 4) {
				float scale = transform.localScale.x / 2;
				Vector3 position = new Vector3(transform.position.x + 0.4f, transform.position.y + 0.4f, transform.position.z + 0.4f);
				Vector3 position1 = new Vector3(transform.position.x - 0.4f, transform.position.y - 0.4f, transform.position.z - 0.4f);

				GameObject asteroidChild = Instantiate(Asteroid, position,  Quaternion.identity) as GameObject;
				GameObject asteroidSndChild = Instantiate(Asteroid, position1,  Quaternion.identity) as GameObject;
				asteroidChild.transform.localScale = new Vector3(scale, scale, scale);
				asteroidSndChild.transform.localScale = new Vector3(scale, scale, scale);
			}
		}

		if(other.tag == "Torpedo") {
			Explode(other);
		}
	}

	void Explode(Collider other) {
		//instantiate an explosion at the same position as the asteroid
		Instantiate(asteroidExplosion, transform.position, transform.rotation);
		Destroy(gameObject);
		Destroy(other.gameObject);
	}

	// private void PlayExplosionSound() {
	// 	audioSource.clip = explosionSound;
	// 	audioSource.volume = 0.2f;
	// 	audioSource.Play();
	// }
}
