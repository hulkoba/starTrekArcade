using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour {

	//public Slider torpedoSlider;

	// shots:
	public Transform shot;
	public Transform torpedo;
	public Transform shotSpawn;

	float nextFire = 0.0f;
	float nextTorpedo = 0.0f;

	float fireRate = 0.3f;
	float torpedoRate = 1f;

	[SerializeField] private AudioClip shotSound;
	[SerializeField] private AudioClip torpedoSound;
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
			PlayShotSound(shotSound);
		}
		if(Input.GetButton("Fire2") && Time.time >= nextTorpedo ) {
			nextTorpedo = Time.time + torpedoRate;
			//torpedoSlider.value -= 10;

			shotSpawn.rotation = gameObject.transform.rotation;

			Instantiate(torpedo, shotSpawn.position, shotSpawn.rotation);
			PlayShotSound(torpedoSound);
		}
	}

	// public void shootTorpedo(){
	// 	torpedoSpawn.rotation = GameObject.FindGameObjectWithTag("MainCamera").transform.rotation;
	// 	torpedoSpawn.position = GameObject.FindGameObjectWithTag("MainCamera").transform.forward;
	// 	Instantiate(torpedo, torpedoSpawn.position, torpedoSpawn.rotation);
	// }

	private void PlayShotSound(AudioClip sound) {
		audioSource.clip = sound;
		//audioSource.volume = 0.1f;
		audioSource.Play();
	}
}
