using UnityEngine;
using System.Collections;

public class StationHealth : MonoBehaviour {

	public int stationHealt = 100;
	private CapsuleCollider capCollider;

	// Use this for initialization
	void Start () {
		capCollider = gameObject.GetComponent<CapsuleCollider> ();
	}
	
	// Update is called once per frame
	void Update () {
	
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
		if (stationHealt >= 0) {
			stationHealt = stationHealt - damage;
		} else {
			dying();
		}
	}

	void dying(){
		Debug.Log ("Station is dying");
		Destroy (gameObject);
	}

	void changeScore(int scoreChange){
		ScoreManager.score = ScoreManager.score + scoreChange;
	}
}
