using UnityEngine;
using System.Collections;

public class StationHealth : MonoBehaviour {

	public int stationHealth = 100;
	private CapsuleCollider capCollider;

	public GameObject stationExplosion;

	// Use this for initialization
	void Start () {
		capCollider = gameObject.GetComponent<CapsuleCollider> ();
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "Bolt") {
			applyDamage(5);
			Destroy(other.gameObject);
			changeScore(-5);
		} else if (other.tag == "EnterpriseBolt") {
			applyDamage(5);
			Destroy(other.gameObject);
			changeScore(-10);
		} else if (other.tag == "Torpedo") {
			applyDamage(15);
			Destroy(other.gameObject);
			changeScore(-10);
		} else if (other.tag == "Enemy(Clone)") {
			applyDamage(15);
			Destroy(other.gameObject);
			changeScore(-10);
		}
	}

	void applyDamage(int damage){
		if (stationHealth >= 0) {
			stationHealth -= damage;
		} else {
			dying();
		}
	}

	void dying(){
		Instantiate(stationExplosion, transform.position, transform.rotation);
		Destroy (gameObject);
	}

	void changeScore(int score){
		ScoreManager.score += score;
	}
}
