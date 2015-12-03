using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {

	public float moveSpeed;

	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody>().velocity = Vector3.forward * moveSpeed;
	}
}
