﻿using UnityEngine;
using System.Collections;

public class EnemyShooting : MonoBehaviour
{
	bool playerInRange;

	private int damage = 5;
	public float laserSpeed;

	LineRenderer line;
	float timer;// Timer for counting up to the next attack.
	private float reloadTime = 1f;

	GameObject player;
	PlayerHealth playerHealth;

	// Use this for initialization
	void Awake () {
		playerInRange = false;
		player = GameObject.FindGameObjectWithTag("Enterprise");
		playerHealth = player.GetComponent<PlayerHealth>();
		Debug.Log("health " + playerHealth);

		Debug.Log("health " + playerHealth.currentHealth);

		line = gameObject.GetComponent<LineRenderer> ();
		line.enabled = false;
	}

	public void Update(){
		// Add the time since Update was last called to the timer.
		timer += Time.deltaTime;

		if(timer >= reloadTime && playerInRange /*&& enemyHealth.currentHealth > 0*/){

			FireLaser ();
		}
		//Debug.Log("playerInRange: " + playerInRange);
		// if (playerInRange == true) {
		// 	StartCoroutine("FireLaser");
		// }
	}

	void OnTriggerEnter (Collider other) {
        // If the entering collider is the player...
        if(other.gameObject == player) {
            playerInRange = true;
        }
    }
	void OnTriggerExit (Collider other){
	// If the exiting collider is the player...
		if(other.gameObject == player) {
			playerInRange = false;
		}
	}

	void FireLaser() {
		// Reset the timer.
        timer = 0f;

		line.enabled = true;
		Ray ray = new Ray(transform.position, transform.forward);
		RaycastHit hit;

		line.SetPosition(0,transform.position);
		//line.SetPosition(0,ray.origin);

		// Set the shootRay so that it starts at the end of the gun and points forward from the barrel.
		ray.origin = transform.position;
		ray.direction = transform.forward;

		if(Physics.Raycast(ray, out hit, 100)){

			// trifft irgendwas
			if(hit.collider.gameObject.name.Equals("FPSController")){

			   	if(playerHealth.currentHealth > 0) {
					playerHealth.ApplyDamage (damage);
				}
			}
			line.SetPosition(1,hit.point);
			//else if(hit.collider.gameObject.tag.Equals("Station")){
				//DAMAGE TO SPACESTATION
			//}
				//hit.rigidbody.AddForceAtPosition(transform.forward*10,hit.point);
		} else {
			line.SetPosition(1,ray.GetPoint(100));
		}

		line.enabled = false;
	}

	// IEnumerator FireLaser(){
	// 	line.enabled = true;
	// 	Ray ray = new Ray(transform.position, transform.forward);
	// 	RaycastHit hit;
	//
	// 	line.SetPosition(0,ray.origin);
	//
	// 	if(Physics.Raycast(ray, out hit, 100)){
	// 		line.SetPosition(1,hit.point);
	// 		// trifft irgendwas
	// 		if(hit.collider.gameObject.name.Equals("FPSController")){
	// 		   	playerHealth.ApplyDamage (damage);
	// 		}
	// 		//else if(hit.collider.gameObject.tag.Equals("Station")){
	// 			//DAMAGE TO SPACESTATION
	// 		//}
	// 			//hit.rigidbody.AddForceAtPosition(transform.forward*10,hit.point);
	// 	} else {
	// 		line.SetPosition(1,ray.GetPoint(100));
	// 	}
	//
	// //	yield return new WaitForSeconds(reloadTime);
	// 	line.enabled = false;
	// 	StartCoroutine("FireLaser");
	// 	yield return new WaitForSeconds (reloadTime);
	// 	//StartCoroutine("FireLaser");
	// }

}
