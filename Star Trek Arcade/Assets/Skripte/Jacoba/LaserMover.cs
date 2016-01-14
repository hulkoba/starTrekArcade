using UnityEngine;
using System.Collections;

public class LaserMover : MonoBehaviour {
	public int moveSpeed;

	void Start () {
		GetComponent<Rigidbody>().velocity = transform.forward * moveSpeed;
	}
}
