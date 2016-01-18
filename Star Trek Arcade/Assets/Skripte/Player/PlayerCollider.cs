using UnityEngine;
using System.Collections;

public class PlayerCollider : MonoBehaviour {

	public PlayerHealth playerHealth;

	// Use this for initialization
	void Start () {
		playerHealth = transform.parent.GetComponent<PlayerHealth> ();
	}

	void OnTriggerEnter(Collider other){

		if(other.tag == "EnterpriseBolt") {
			return;
		}
		if (other.tag == "Bolt") {

			playerHealth.ApplyDamage(5);
			//Zerstoere Schuss
			//Debug.Log ("PLAYERHEALTH:"+other.gameObject.name);
			Destroy(other.gameObject);
		}
		if (other.gameObject.name == "Enemy(Clone)") {
			Vector3 helper = gameObject.transform.forward;
			other.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(helper.x*20f,helper.y*20f,helper.z*20f),ForceMode.Impulse);
		}
		if (other.gameObject.name == "Asteroid(Clone)") {
			Vector3 helper = gameObject.transform.forward;
			other.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(helper.x*10f,helper.y*10f,helper.z*10f),ForceMode.Impulse);

		}
	}
}
