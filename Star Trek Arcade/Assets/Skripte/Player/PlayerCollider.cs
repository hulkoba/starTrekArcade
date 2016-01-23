using UnityEngine;
using System.Collections;

public class PlayerCollider : MonoBehaviour {

	public PlayerHealth playerHealth;
	public AudioClip damageSound;
	AudioSource audioSource;

	// Use this for initialization
	void Awake () {
		playerHealth = transform.parent.GetComponent<PlayerHealth> ();
		audioSource = GetComponent <AudioSource> ();
	}

	void OnTriggerEnter(Collider other){

		if (other.tag == "Bolt") {

			PlayDamageSound();
			playerHealth.ApplyDamage(5);
			//Zerstoere Schuss
			Destroy(other.gameObject);
		}

		if (other.gameObject.name == "Enemy(Clone)") {
			Vector3 helper = gameObject.transform.forward;
			other.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(helper.x*20f,helper.y*20f,helper.z*20f),ForceMode.Impulse);
		}

	}

	private void PlayDamageSound() {
		audioSource.clip = damageSound;
		audioSource.Play();
	}
}
