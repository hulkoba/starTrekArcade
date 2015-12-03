using UnityEngine;
using System.Collections;

public class colliderScript : MonoBehaviour {



	void OnTriggerEnter(Collider other) {
		//Destroy(other.gameObject);
		if (other.gameObject.name == "Enemy") {
			if(other.GetType () == typeof(SphereCollider)){
				//Debug.Log("Enemy Sphere!");
			}
			else if(other.GetType() == typeof(BoxCollider)){
				//Debug.Log ("Enemy Box!");
			}
		}
		else if(other.gameObject.name == "Astroid"){
			if(other.GetType () == typeof(SphereCollider)){
				//Debug.Log("Astroid Sphere!");
			}
			else if(other.GetType() == typeof(BoxCollider)){
				//Debug.Log ("Astroid Box!");
			}
		}

	}

	void OnTriggerStay(Collider other) {
		//if (other.attachedRigidbody) {
		//	other.attachedRigidbody.AddForce (Vector3.up * 10);
		//}
	}

	void OnTriggerExit(Collider other) {
		// Destroy everything that leaves the trigger
		//Destroy(other.gameObject);

	}
}
