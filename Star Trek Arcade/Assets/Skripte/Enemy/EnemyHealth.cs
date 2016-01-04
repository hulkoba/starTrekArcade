using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

	public GameObject enemyExplosion;
	private float health = 100;

	private GameController gameController;

	public void Start(){
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent <GameController>();
		} else {
			Debug.Log ("Cannot find 'GameController' script");
		}
	}

	//VLLT hier Update aus LifePointController einbauen, damit
	//es beim Multiplayer einfacher ist zu trennen möglicherweise.
	public void ApplyDamage(float damage) {
		health -= damage;
		if(health <= 0) {
			Dying();
		}
	}

	public void Dying() {
		//instantiate an enemyExplosion at the same position as the asteroid
		Instantiate(enemyExplosion, transform.position, transform.rotation);
		gameController.AddScore(10);
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

	public float getCurrentHealth(){
		return health;
	}

}