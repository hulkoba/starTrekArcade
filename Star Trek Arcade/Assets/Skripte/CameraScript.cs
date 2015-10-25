using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	public float SmoothTime = 1f;

	private Vector3 CameraVelocity;

	private Transform Target = null;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		// only if target exists
		if (Target != null) {

			// rotate towards the given target, damped a little bit
			transform.LookAt(transform.position + Vector3.SmoothDamp(transform.forward, Target.position - transform.position, ref CameraVelocity, SmoothTime));
		}
	}

	// called by the creation script
	public void SetTarget(Transform NewTarget){
		Target = NewTarget;
	}
}
