using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour {

	// shots:
	public Transform shot;
	public Transform shotSpawn;

	float nextFire = 0.0f;
	float fireRate = 0.5f;

	[SerializeField] private AudioClip shotSound;
	private AudioSource audioSource;
	
	// Use this for initialization
	void Awake () {
		audioSource = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	private void Update() {

		//pressed the firebutton AND loaded weapons?
		if(Input.GetButton("Fire1") && Time.time >= nextFire ) {
			nextFire = Time.time + fireRate;

			shotSpawn.rotation = gameObject.transform.rotation;

			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
			PlayShotSound();
		}
	}

	// public void shootTorpedo(){
	// 	torpedoSpawn.rotation = GameObject.FindGameObjectWithTag("MainCamera").transform.rotation;
	// 	torpedoSpawn.position = GameObject.FindGameObjectWithTag("MainCamera").transform.forward;
	// 	Instantiate(torpedo, torpedoSpawn.position, torpedoSpawn.rotation);
	// }

	private void PlayShotSound() {
		audioSource.clip = shotSound;
		audioSource.volume = 0.1f;
		audioSource.Play();
	}

	private void OnControllerColliderHit(ControllerColliderHit hit) {
		print("### Collision!  " + hit);
		Rigidbody body = hit.collider.attachedRigidbody;
		//dont move the rigidbody if the character is on top of it
		// if (m_CollisionFlags == CollisionFlags.Below) {
		// 	return;
		// }

		if (body == null || body.isKinematic) {
			return;
		}
		//body.AddForceAtPosition(m_CharacterController.velocity*0.1f, hit.point, ForceMode.Impulse);
	}

}
