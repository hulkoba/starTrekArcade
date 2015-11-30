using UnityEngine;
using System.Collections;

public class EnemyShooting : MonoBehaviour 
{
	
	LineRenderer line;
	private float shootingTime = 5f;
	
	// Use this for initialization
	void Start () 
	{
		line = gameObject.GetComponent<LineRenderer> ();
		line.enabled = false;
		StartCoroutine("FireLaser");
	}
	
	// Update is called once per frame
	void Update () {

		//StopCoroutine("FireLaser");
		//StartCoroutine("FireLaser");
	
	}
	
	IEnumerator FireLaser(){
		line.enabled = true;
		
		Ray ray = new Ray(transform.position, transform.forward);
		RaycastHit hit;
			
		line.SetPosition(0,ray.origin);

		if(Physics.Raycast(ray, out hit, 100)){
			line.SetPosition(1,hit.point);
			if(hit.rigidbody)
			{
				Debug.Log("HIT!!!");
				hit.rigidbody.AddForceAtPosition(transform.forward*10,hit.point);
			}
		}
		else
			line.SetPosition(1,ray.GetPoint(100));



		yield return new WaitForSeconds(0.1f);
		line.enabled = false;
		yield return new WaitForSeconds (0.25f);
		StartCoroutine("FireLaser");
	}

}
