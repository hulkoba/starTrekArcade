using UnityEngine;
using System.Collections;

public class LaserMover : MonoBehaviour {
	public float moveSpeed;

	void Start () {
		Debug.Log ("Forward="+transform.forward);
		GetComponent<Rigidbody>().velocity = transform.forward * moveSpeed;
	}
}
