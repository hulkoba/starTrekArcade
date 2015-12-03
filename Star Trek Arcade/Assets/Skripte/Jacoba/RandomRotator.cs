using UnityEngine;
using System.Collections;

public class RandomRotator : MonoBehaviour {
	public float tumble;

	void Start () {
		// Random.insideUnitSphere = x || y || z
		GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * tumble;
	}

}
