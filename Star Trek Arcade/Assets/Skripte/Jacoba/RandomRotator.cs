using UnityEngine;
using System.Collections;

public class RandomRotator : MonoBehaviour {

	void Start () {
		// rotate: Random.insideUnitSphere = x || y || z
		GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * Random.Range(-1, 1);

		//move
		GetComponent<Rigidbody>().velocity = Vector3.forward * Random.Range(-2, 2);
	}

}
