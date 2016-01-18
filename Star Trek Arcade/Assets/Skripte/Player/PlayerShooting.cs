using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour {

	// shots:
	public Transform shot;
	public Transform shotSpawn;

	float nextFire = 0.0f;
	float fireRate = 0.3f;

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
}
