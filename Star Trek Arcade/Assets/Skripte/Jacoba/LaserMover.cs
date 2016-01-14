using UnityEngine;
using System.Collections;

public class LaserMover : MonoBehaviour {
	int moveSpeed = 20;

	void Start () {
		GetComponent<Rigidbody>().velocity = transform.forward * moveSpeed;
	}
}
