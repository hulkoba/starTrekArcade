using UnityEngine;
using System.Collections;

public class LaserMover : MonoBehaviour {

	public float laserSpeed;

	void Start () {
		GetComponent<Rigidbody>().velocity = transform.forward * laserSpeed;
	}

}
