using UnityEngine;
using System.Collections;

public class LaserMover : MonoBehaviour {
	public float moveSpeed;
	
	void Start () {
		GetComponent<Rigidbody>().velocity = transform.forward * moveSpeed;
	}
}
