using UnityEngine;
using System.Collections;
using UnityEngine.UI; // for access to Slider

public class PlayerShooting : MonoBehaviour {

	public Slider torpedoSlider;

	// shots:
	public Transform shot;
	public Transform torpedo;
	public Transform shotSpawn;

	float nextFire = 0.0f;
	float nextTorpedo = 0.0f;

	float fireRate = 0.3f;
	float torpedoRate = 1f;
	bool torpedoFired = false;

	[SerializeField] private AudioClip shotSound;
	[SerializeField] private AudioClip torpedoSound;
	[SerializeField] private AudioClip readyToFireSound;
	private AudioSource audioSource;

	// Use this for initialization
	void Awake () {
		audioSource = GetComponent<AudioSource>();

		// recharge torpedo slider each second
		InvokeRepeating("RechargeTorpedos", 0, 1.0f);
	}

	// Update is called once per frame
	private void Update() {

		//pressed the firebutton AND loaded weapons?
		if((Input.GetButton("Fire1")||Input.GetKey(KeyCode.Space)) && Time.time >= nextFire ) {
			nextFire = Time.time + fireRate;

			shotSpawn.rotation = gameObject.transform.rotation;

			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
			PlayShotSound(shotSound, 0.1f);
		}

		if((Input.GetButton("Fire2")||Input.GetKey(KeyCode.LeftAlt)) && Time.time >= nextTorpedo && torpedoSlider.value >= 10) {
			torpedoFired = true;

			nextTorpedo = Time.time + torpedoRate;
			torpedoSlider.value -= 10;

			shotSpawn.rotation = gameObject.transform.rotation;

			Instantiate(torpedo, shotSpawn.position, shotSpawn.rotation);
			PlayShotSound(torpedoSound, 1f);
		}
	}

	public void RechargeTorpedos() {
		torpedoSlider.value += 1;
		if(torpedoSlider.value == 100 && torpedoFired) {
			PlayReadySound();
			torpedoFired = false;
		}
	}

	private void PlayReadySound() {
		audioSource.clip = readyToFireSound;
		audioSource.Play();
	}

	private void PlayShotSound(AudioClip sound, float volume) {
		audioSource.clip = sound;
		audioSource.volume = volume;
		audioSource.Play();
	}
}
