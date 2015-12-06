using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

	public GameObject explosion;
	public float shield;
	public float health;

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

		if(health <= 0 && shield <= 0)
		{
			Dying();
		}
		else
		{
			if(shield > 0){
				ShieldDamaging(damage);
			}
			else{
				Damaging(damage);
			}
		}
	}

	public virtual void ShieldDamaging(float damage)
	{
		shield -= damage;

		if(shield < 0){
			float damageLeft = shield*-1;
			shield = 0;
			Damaging(damageLeft);
		}
	}

	/**
	 * Statt void muss IEnumerator hin, damit yield return new WaitforSeconds() geht.
	 * Dann wartet er mit Yield auf die 0.5 Sekunden und geht dann dort weiter.
	 **/
	public virtual IEnumerator RechargeShield(){
		while (shield <= 100) {
			shield += 5;
			yield return new WaitForSeconds(0.5f);
		}
	}

	public virtual void Damaging(float damage)
	{
		health -= damage;
	}

	public virtual void Dying()
	{
		//instantiate an explosion at the same position as the asteroid
		Instantiate(explosion, transform.position, transform.rotation);
		gameController.AddScore(10);
		//destroy the shot
		Destroy(gameObject);
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "Bolt") {
			ApplyDamage(10);
			//Zerstoere Schuss
			Destroy(other.gameObject);
		}
	}
}
