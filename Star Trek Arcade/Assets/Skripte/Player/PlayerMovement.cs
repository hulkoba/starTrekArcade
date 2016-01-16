﻿using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	[SerializeField] private AudioClip warpSound;
	private AudioSource audioSource;

	private float flySpeed = 10;   // left shift for flying
	private float warpSpeed = 40;  // w for warping

	Rigidbody rb;

	// Use this for initialization
	void Awake () {
		audioSource = GetComponent<AudioSource>();
		rb = gameObject.GetComponent<Rigidbody> ();
	}

	void FixedUpdate () {
		float speed;
		GetInput(out speed);

		//Senkrecht zueinander ist 0, Parallel 1, genau hinter dir -1,
		//Es wird ein Kegel aufgezogen damit, mit Vector3.dot wird das Skalarprodukt von deiner Position - Gegner Position gezogen,
		//dann wird mit transform.forward das Kreuzprodukt gebildet und dann kann man halt gucken, je nachdem, wie  man zum Gegner steht ergibt das Kreuzprodukt bestimmte Werte, wie oben genannt
		//siehe Internet: Vector3.dot das Beispiel erläutert es gut
		//Damit kann man neben dem Schießen von Zielsuchenden Raketten bestimmt auch den Gegner fliegen lassen, also wenn der Spieler aus diesem
		//Bereich raus ist, dann richte dich etwas aus und fliege vorwärts oder so, so ist ein wenig nicht genaues Fliegen drin
		//Oder fürs schießen ein Random von ein paar Metern, dass man mal vorbei schießt, oder so
		//if (Vector3.Dot (transform.forward, (transform.position - enemy.transform.position).normalized) > 0.9) {
		//}
	}

	private void GetInput(out float speed) {
		speed = 0f;

		// keep track of whether or not the enterprise is moving'
		//isMoving = !Input.GetKey(KeyCode.LeftShift) || !Input.GetKey(KeyCode.W);

		// set the desired speed to be flying or 'warping'
		if(Input.GetKeyDown(KeyCode.W)) {
			PlayWarpSound();
		}
		if(Input.GetKey(KeyCode.W)) {
			//speed = warpSpeed;
			move(warpSpeed);
		}

		if(Input.GetKey(KeyCode.LeftShift)) {
			//speed = flySpeed;
			move(flySpeed);
		}

		//rotate
		if (Input.GetKey (KeyCode.LeftArrow)) {
			rotateView(0f,-15f);
		}
		if (Input.GetKey (KeyCode.RightArrow)) {
			rotateView(0f,15f);
		}
		if (Input.GetKey (KeyCode.UpArrow)) {
			rotateView(-15f,0f);
		}
		if (Input.GetKey (KeyCode.DownArrow)) {
			rotateView(15f,0f);
		}
	}

	void move(float speed){
		rb.AddForce (GameObject.FindGameObjectWithTag("MainCamera").transform.forward * speed);
	}

	void rotateView(float upDown, float leftRight){
		rb.AddTorque (transform.right * upDown + transform.up * leftRight);
	}

	private void PlayWarpSound() {
		audioSource.clip = warpSound;
		audioSource.volume = 0.1f;
		audioSource.Play();
	}
}
