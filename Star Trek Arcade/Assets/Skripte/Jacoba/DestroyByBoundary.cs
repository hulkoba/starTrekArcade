using UnityEngine;
using System.Collections;

public class DestroyByBoundary : MonoBehaviour {
	//destroy the shots as they are leave the boundary
	void OnTriggerExit(Collider other) {
		Debug.Log (other.gameObject.name + "Destroy Bolt? BYBOUNDARY");
		Destroy(other.gameObject);
	}
}
