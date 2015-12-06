using UnityEngine;
using System.Collections;

public class LaserMover : MonoBehaviour {
	private float moveSpeed = 20f;

	void Start () {
		GetComponent<Rigidbody>().velocity = transform.forward * moveSpeed;
	}
}
