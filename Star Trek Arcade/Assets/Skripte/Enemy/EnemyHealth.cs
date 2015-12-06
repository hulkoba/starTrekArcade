using UnityEngine;
using System.Collections;

public class EnemyHealth : HealthController {

	public GameObject explosion;

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
	public override void ApplyDamage(float damage) {
		base.ApplyDamage(damage);
	}

	public virtual IEnumerator ShieldDamaging(float damage)
	{
		shield -= damage;//<---- DA NOCH WAS SUCHEN FUER EIGENTLICHEN SCHADEN

		if(shield < 0){
			float damageLeft = shield*-1;
			shield = 0;
			Damaging(damageLeft);
		}
		//Wait 0.5 Sec.
		//WaitForSeconds(0.5f);//<----? Geht das?
		yield return new WaitForSeconds(0.5f);
		// if (beenShot != true) {
		// 	RechargeShield();
		// }
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
		//beenShot = false;
	}

	public virtual void Dying()
	{
		//instantiate an explosion at the same position as the asteroid
		Instantiate(explosion, transform.position, transform.rotation);
		gameController.AddScore(10);
		//destroy the shot and the asteroid...
		Destroy(gameObject);
	}

	void OnTriggerEnter(Collider other) {
		// not destroying the Boundary!
		if (other.tag == "Boundary") {
			//Dying();
		}
		if (other.tag == "Bolt") {
			Debug.Log ("BOLTHIT!");
		}
	}
}
