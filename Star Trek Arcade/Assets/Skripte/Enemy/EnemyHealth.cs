using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

	public GameObject enemyExplosion;
	int health = 90;
	public int currentHealth = 0;

	int scoreValue = 30;

	public AudioClip enemyDeathSound;
	AudioSource audioSource;

	bool isDead;

	void Awake() {
		audioSource = GetComponent <AudioSource> ();
		currentHealth = health;
	}

	public void ApplyDamage(int damage) {
		if(isDead) {
			return;
		}
		currentHealth -= damage;
		if(currentHealth <= 0 && !isDead) {
			Dying();
		}
	}

	public void Dying() {
		isDead = true;
		PlayExplosionSound();

		//instantiate an enemyExplosion at the same position as the asteroid
		Instantiate(enemyExplosion, transform.position, transform.rotation);
		//destroy the enemy
		Destroy(gameObject);

		// Increase the score by the enemy's score value.
		ScoreManager.score += scoreValue;
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "EnterpriseBolt") {
			//Debug.Log ("ENEMYHEALTH:"+other.gameObject.name);
			ApplyDamage(20);
			//Zerstoere Schuss
			Destroy(other.gameObject);
		}
		if(other.tag == "Bolt") {
			Debug.Log("hit enemy ");
		//	Debug.Log ("ENEMYHEALTH:"+other.gameObject.name);
			//ApplyDamage(2);
			//Destroy(other.gameObject);
		}
	}

	private void PlayExplosionSound() {
		audioSource.clip = enemyDeathSound;
        audioSource.Play ();
	}

	// TODO: brauchen wir das?
	public float getCurrentHealth(){
		return currentHealth;
	}
}
