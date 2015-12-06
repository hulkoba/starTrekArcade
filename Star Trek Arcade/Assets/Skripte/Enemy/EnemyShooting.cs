using UnityEngine;
using System.Collections;

public class EnemyShooting : MonoBehaviour
{
	public bool startFire;

	public float damage;
	public float laserSpeed;

	LineRenderer line;
	public float reloadTime = 5f;

	// Use this for initialization
	void Start ()
	{
		startFire = false;
		line = gameObject.GetComponent<LineRenderer> ();
		line.enabled = false;
	}

	public void Update(){
		if (startFire) {
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
			// trifft irgendwas
			if(hit.collider.gameObject.name.Equals("FPSController")){
			   	hit.collider.gameObject.GetComponentInChildren<PlayerHealth>().ApplyDamage(damage);
			}
			else if(hit.collider.gameObject.tag.Equals("Station")){
				//DAMAGE TO SPACESTATION
			}
				//hit.rigidbody.AddForceAtPosition(transform.forward*10,hit.point);
		}
		else {
			line.SetPosition(1,ray.GetPoint(100));
		}

		yield return new WaitForSeconds(0.3f);
		line.enabled = false;
		yield return new WaitForSeconds (2.5f);
		StartCoroutine("FireLaser");
	}

}
