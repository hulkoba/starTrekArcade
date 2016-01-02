using UnityEngine;
using System.Collections;

public class LaserScript : MonoBehaviour {

	LineRenderer line;

	// Use this for initialization
	void Start ()
	{
		line = gameObject.GetComponent<LineRenderer> ();
		line.enabled = false;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
			StopCoroutine("FireLaser");
			StartCoroutine("FireLaser");
		}
	}

	IEnumerator FireLaser(){
		line.enabled = true;

		Ray ray = new Ray(transform.position, transform.forward);
		RaycastHit hit;

		line.SetPosition(0,ray.origin);

		if(Physics.Raycast(ray, out hit, 100)){
			line.SetPosition(1,hit.point);
			if(hit.rigidbody) {
					hit.rigidbody.AddForceAtPosition(transform.forward*10,hit.point);
				}
			} else {
				line.SetPosition(1,ray.GetPoint(100));
			}

			yield return null;

		line.enabled = false;
	}
}
