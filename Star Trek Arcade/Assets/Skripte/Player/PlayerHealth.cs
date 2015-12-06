using UnityEngine;
using System.Collections;
using UnityEngine.UI; // for access to Slider

public class PlayerHealth : MonoBehaviour {

	public GameObject playerExplosion;
	public Slider healthUI;
	public Slider shieldUI;
	// TODO: transparent damageimage
	//public Image damageImage
	//public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

	//EnterpriseController playerMovement; // Reference to the player's movement.

	public int startingShield = 100;
	public int startingHealth = 100;
	public int currentHealth;
	public int currentShield;

	void Awake () {
        // Set the initial health of the player.
        currentHealth = startingHealth;
		currentShield = startingShield;
    }

	// void Update () {
    //     // If the player has just been damaged...
    //     if(damaged) {
    //         // ... set the colour of the damageImage to the flash colour.
    //         damageImage.color = flashColour;
    //     } else {
    //         // ... transition the colour back to clear.
    //         damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
    //     }
    //     // Reset the damaged flag.
    //     damaged = false;
    // }


	public void ApplyDamage(int damage) {
		Debug.Log("Apply damage" + damage);
		if(currentShield > 0){
			ShieldDamaging(damage);
		} else{
			Damaging(damage);
		}
	}

	public void ShieldDamaging(int damage) {
		currentShield -= damage;
		shieldUI.value = currentShield;
		if(currentShield <= 0) {
			int damageLeft = currentShield*-1;
			currentShield = 0;
			Damaging(damageLeft);
		}
	}

	//SIEHE ENEMYHEALTH FUER IDEEN
	public virtual void RechargeShield(){
		while (currentShield <=100) {
			currentShield += 5;
		}
	}

	public void Damaging(int damage) {
		currentHealth -= damage;
		healthUI.value = currentHealth;

		if(currentHealth <= 0) {
			Dying();
		}
	}

    public void Dying() {
		Debug.Log("DEATH!!!");

		//KAMERA.PARTEN = NULL
		//DANN NACH HINTEN .TransForm UND MAN KÖNNTE DANN EXPLODIEREN
		//DRAN DENKEN, DASS DIE KAMERA DANN GELÖSCHT WERDEN MUSS, WENN NEUES SPIEL GESTARTET WIRD
		//instantiate an playerExplosion at the same position as the ship
		Instantiate(playerExplosion, transform.position, transform.rotation);
		Destroy(gameObject);
		//Destroy(other.gameObject); --> if we use Bolt for enemies
    }
}
