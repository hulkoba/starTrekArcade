using UnityEngine;
using System.Collections;
using UnityEngine.UI; // for access to Slider

public class PlayerHealth : MonoBehaviour {

	public GameObject playerExplosion;
	public Slider healthUI;
	public Slider shieldUI;
	public Text DockedText;

	public Image damageImage;
	Color flashColour = new Color(1f, 0f, 0f, 0.3f);

	public AudioClip deathSound;
	public AudioClip alertSound;
	AudioSource audioSource;

	int startingShield = 100;
	int startingHealth = 100;
	public int currentHealth = 0;
	public int currentShield = 0;

	float shieldReloadTime = 2.0f;
	float lastDamageTime = 0.0f;

	float shieldReloadWaitingTime = 1f;
	float timeBetweenShieldRecharge = 0.0f;

	PlayerMovement playerMovement; // Reference to the player's movement.
    PlayerShooting playerShooting; // Reference to the PlayerShooting script.

	bool isDead;
	bool damaged;

	GameController gameController;

	void Awake () {
        // Set the initial health of the player.
        currentHealth = startingHealth;
		currentShield = startingShield;

		audioSource = GetComponent <AudioSource> ();
        playerMovement = GetComponent<PlayerMovement> ();
        playerShooting = GetComponent<PlayerShooting> ();
		gameController = GameObject.Find ("GameController").GetComponent<GameController>();

		DockedText.enabled = false;
	}

	void Update () {
		DockedText.text = "Docked";

         if(damaged) {
            // ... set the colour of the damageImage to the flash colour.
            damageImage.color = flashColour;
			lastDamageTime = Time.time;
         } else {
             // ... transition the colour back to clear. flashspeed = 5f
             damageImage.color = Color.Lerp (damageImage.color, Color.clear, 5f * Time.deltaTime);

			if(currentShield <= 100 && Time.time > shieldReloadTime+lastDamageTime){
				if(Time.time > timeBetweenShieldRecharge + shieldReloadWaitingTime && currentShield < 100){
					RechargeShield(1);
					timeBetweenShieldRecharge = Time.time;
				}
			}
         }
         // Reset the damaged flag.
         damaged = false;
    }

	public void Docked() {
		DockedText.enabled = true;

	//	gameController.frozen = true;
		if (currentHealth < 100) {
			playerMovement.enabled = false;
			playerShooting.enabled = false;
			
			playerShooting.torpedoSlider.value = 100;
			while(currentShield < 100){
				RechargeShield(5);
			}
			while(currentHealth < 100){
				RechargeHealth(5);
			}
		}

		if(currentHealth >= 100 && currentShield >= 100) {
			DockedText.enabled = false;
			//playerMovement.enabled = true;
	        //playerShooting.enabled = true;
		}
	}

	public void ApplyDamage(int damage) {
		damaged = true;
		if(currentShield > 0){
			ShieldDamaging(damage);
		} else{
			ShipDamaging(damage);
		}
	}

	void ShieldDamaging(int damage) {
		currentShield -= damage;
		shieldUI.value = currentShield;

		if(currentShield <= 0) {
			PlayAlertSound();

			int damageLeft = currentShield*-1;
			currentShield = 0;
			ShipDamaging(damageLeft);
		}
	}

	void RechargeShield(int num) {
		currentShield += num;
		if(currentShield > 100) {
			currentShield = 100;
		}
		shieldUI.value = currentShield;
	}
	void RechargeHealth(int num) {
		currentHealth += num;
		if(currentHealth > 100) {
			currentHealth = 100;
		}
		healthUI.value = currentHealth;
	}

	void ShipDamaging(int damage) {
		currentHealth -= damage;
		healthUI.value = currentHealth;
		if(currentHealth <= 5 && currentHealth > 0) {
			PlayAlertSound();
		}
		if(currentHealth <= 0 && !isDead) {
			GameOver();
		}
	}

    public void GameOver() {
		isDead = true;
		//PlayDeathSound();

		damageImage.color = flashColour;

		//KAMERA.PARTEN = NULL
		//DANN NACH HINTEN .TransForm UND MAN KÖNNTE DANN EXPLODIEREN
		//DRAN DENKEN, DASS DIE KAMERA DANN GELÖSCHT WERDEN MUSS, WENN NEUES SPIEL GESTARTET WIRD

		//instantiate an playerExplosion at the same position as the ship
		Instantiate(playerExplosion, transform.position, transform.rotation);

		DisableScripts();
		gameController.EndSequence ();
    }

	void DisableScripts () {
		// Turn off the movement and shooting scripts.
        playerMovement.enabled = false;
        playerShooting.enabled = false;
	}

	private void PlayDeathSound() {
		audioSource.clip = deathSound;
		audioSource.Play();
	}

	private void PlayAlertSound() {
		audioSource.clip = alertSound;
		audioSource.Play();
	}
}
