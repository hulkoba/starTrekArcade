using UnityEngine;
using System.Collections;

public class PlayerCollider : MonoBehaviour {

	public PlayerHealth playerHealth;

	// Use this for initialization
	void Start () {
		playerHealth = transform.parent.GetComponent<PlayerHealth> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		if(other.tag == "EnterpriseBolt") {
			return;
		}
		if (other.tag == "Bolt") {
			
			playerHealth.ApplyDamage(5);
			//Zerstoere Schuss
			Debug.Log ("PLAYERHEALTH:"+other.gameObject.name);
			Destroy(other.gameObject);
		}
	}
}
