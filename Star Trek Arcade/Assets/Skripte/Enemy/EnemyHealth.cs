using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

	public GameObject enemyExplosion;
	int health = 90;
	public int currentHealth = 0;

	int scoreValue = 30;

	public AudioClip enemyDeathSound;
	AudioSource enemyAudio;

	bool isDead;

	void Awake() {
		enemyAudio = GetComponent <AudioSource> ();
		currentHealth = health;
	}

	public void ApplyDamage(int damage) {
		if(isDead) {
			return;
		}

		health -= damage;
		if(currentHealth <= 0) {
			Dying();
		}
	}

	public void Dying() {
		isDead = true;
		PlayExplosionSound();

		//instantiate an enemyExplosion at the same position as the asteroid
		Instantiate(enemyExplosion, transform.position, transform.rotation);

		// Increase the score by the enemy's score value.
		ScoreManager.score += scoreValue;

		//destroy the enemy
		Destroy(gameObject, 1f);
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "Bolt") {
			ApplyDamage(20);
			//Zerstoere Schuss
			Destroy(other.gameObject);
		}
	}

	private void PlayExplosionSound() {
		enemyAudio.clip = enemyDeathSound;
        enemyAudio.Play ();
	}

	// TODO: brauchen wir das?
	public float getCurrentHealth(){
		return health;
	}
}
