using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {


	public GameObject asteroidExplosion;
	Rigidbody body;

	public AudioClip explosionSound;
	AudioSource audioSource;

	void Awake() {
		audioSource = GetComponent<AudioSource>();
		body = gameObject.GetComponent<Rigidbody> ();
	}

	void OnTriggerEnter(Collider other) {
	    // not destroying the Boundary!
		if (other.tag == "Boundary") {
			Debug.Log ("COLLISION DESTROY:"+other.tag);
	        return;
	    }

		if(other.tag == "Asteroid") {
			Debug.Log ("COLLISION DESTROY:"+other.tag);
			body.AddForce((transform.forward *-1) * 5);
		}

		if (other.tag == "Enemy" || other.tag == "MainCamera") {
			//instantiate an explosion at the same position as the asteroid
			Instantiate(asteroidExplosion, transform.position, transform.rotation);
			Debug.Log ("COLLISION DESTROY:"+other.tag);
			PlayExplosionSound();
			Destroy(gameObject);
		}

		if(other.tag == "Bolt" || other.tag == "EnterpriseBolt") {
			//instantiate an explosion at the same position as the asteroid
			Instantiate(asteroidExplosion, transform.position, transform.rotation);
			PlayExplosionSound();
			Destroy(gameObject);
			Destroy(other.gameObject);
		}
	}

	private void OnControllerColliderHit(ControllerColliderHit hit) {
		print("### Collision!  " + hit);
		//Rigidbody body = hit.collider.attachedRigidbody;

		if (body == null || body.isKinematic) {
			return;
		}
		//body.AddForceAtPosition(gameObject.velocity*0.1f, hit.point, ForceMode.Impulse);
	}

	private void PlayExplosionSound() {
		audioSource.clip = explosionSound;
		audioSource.Play();
	}
}
