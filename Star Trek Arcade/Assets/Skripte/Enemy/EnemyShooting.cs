using UnityEngine;
using System.Collections;

public class EnemyShooting : MonoBehaviour
{

	LineRenderer line;
	public float reloadTime = 5f;

	// Use this for initialization
	void Start ()
	{
		line = gameObject.GetComponent<LineRenderer> ();
		line.enabled = false;
		StartCoroutine("FireLaser");
	}

	IEnumerator FireLaser(){
		line.enabled = true;

		Ray ray = new Ray(transform.position, transform.forward);
		RaycastHit hit;

		line.SetPosition(0,ray.origin);

		if(Physics.Raycast(ray, out hit, 5)){
			line.SetPosition(1,hit.point);
			// trifft irgendwas
			if(hit.rigidbody) {
				Debug.Log("HIT!!!");
				if(hit.rigidbody.gameObject.name.Equals("Player")){
			     	hit.rigidbody.gameObject.GetComponent<PlayerHealth>().ApplyDamage(10f);
			    }
				//hit.rigidbody.AddForceAtPosition(transform.forward*10,hit.point);
			}
		}
		else {
			line.SetPosition(1,ray.GetPoint(5));
		}

		yield return new WaitForSeconds(0.1f);
		line.enabled = false;
		yield return new WaitForSeconds (0.3f);
		StartCoroutine("FireLaser");
	}

}
