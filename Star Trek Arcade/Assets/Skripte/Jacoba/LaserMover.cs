using UnityEngine;
using System.Collections;

public class LaserMover : MonoBehaviour {

	void Start () {
		GetComponent<Rigidbody>().velocity = transform.forward * Random.Range(-5, 5);
	}

}
