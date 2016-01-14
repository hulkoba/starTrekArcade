﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI; // for access to Slider

public class PlayerHealth : MonoBehaviour {

	public GameObject playerExplosion;
	public Slider healthUI;
	public Slider shieldUI;

	public Image damageImage;
	Color flashColour = new Color(1f, 0f, 0f, 0.1f);

	public AudioClip deathSound;
	public AudioClip damageSound;
	AudioSource audioSource;

	//PlayerMovement playerMovement; // Reference to the enterprise's movement.
	// PlayerShooting playerShooting;

	int startingShield = 100;
	int startingHealth = 100;
	public int currentHealth = 0;
	int currentShield = 0;

	//PlayerMovement playerMovement;                              // Reference to the player's movement.
    //PlayerShooting playerShooting;                              // Reference to the PlayerShooting script.

	bool isDead;
	bool damaged;

	void Awake () {
        // Set the initial health of the player.
        currentHealth = startingHealth;
		currentShield = startingShield;

		audioSource = GetComponent <AudioSource> ();
        //playerMovement = GetComponent <PlayerMovement> ();
        //playerShooting = GetComponentInChildren <PlayerShooting> ();
    }

	void Update () {
         if(damaged) {
             // ... set the colour of the damageImage to the flash colour.
             damageImage.color = flashColour;
         } else {
             // ... transition the colour back to clear. flashspeed = 5f
             damageImage.color = Color.Lerp (damageImage.color, Color.clear, 5f * Time.deltaTime);
         }
         // Reset the damaged flag.
         damaged = false;
    }


	public void ApplyDamage(int damage) {		
		damaged = true;
		if(currentShield > 0){
			ShieldDamaging(damage);
		} else{
			ShipDamaging(damage);
		}
	}

	public void ShieldDamaging(int damage) {
		currentShield -= damage;
		shieldUI.value = currentShield;

		if(currentShield <= 0) {
			int damageLeft = currentShield*-1;
			currentShield = 0;
			ShipDamaging(damageLeft);
		}
	}

	//SIEHE ENEMYHEALTH FUER IDEEN
	public virtual void RechargeShield(){
		while (currentShield <=100) {
			currentShield += 5;
		}
	}

	public void ShipDamaging(int damage) {

		currentHealth -= damage;
		healthUI.value = currentHealth;

		if(currentHealth <= 0 && !isDead) {
			Dying();
		}
	}

    public void Dying() {
		Debug.Log("DEATH!!!");
		isDead = true;
		PlayDeathSound();

		//KAMERA.PARTEN = NULL
		//DANN NACH HINTEN .TransForm UND MAN KÖNNTE DANN EXPLODIEREN
		//DRAN DENKEN, DASS DIE KAMERA DANN GELÖSCHT WERDEN MUSS, WENN NEUES SPIEL GESTARTET WIRD
		//instantiate an playerExplosion at the same position as the ship
		Instantiate(playerExplosion, transform.position, transform.rotation);
		Destroy(gameObject);
		//Destroy(other.gameObject); --> if we use Bolt for enemies


		// Turn off the movement and shooting scripts.
        //playerMovement.enabled = false;
        //playerShooting.enabled = false;
    }

	private void PlayDeathSound() {
		audioSource.clip = deathSound;
		audioSource.Play();
	}
	private void PlayDamageSound() {
		audioSource.clip = damageSound;
		audioSource.Play();
	}
}
