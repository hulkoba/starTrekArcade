using UnityEngine;
using System.Collections;

public class DestroyByBoundary : MonoBehaviour {
	//destroy the shots as they are leave the boundary
	void OnTriggerExit(Collider other) {
		Destroy(other.gameObject);
	}
}
