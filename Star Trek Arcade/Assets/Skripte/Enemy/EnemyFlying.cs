using UnityEngine;
using System.Collections;

public class EnemyFlying : MonoBehaviour {

	//Collider benutzen dafuer
	bool playerInRange;
	public Transform player;
	public float playerDistance;
	
	public float firingRange;

	Rigidbody rb;

	//0.5 usw. sorgt für langsames Drehen!!!
	public float dragTime;
	//Speed sollte sich an dragTime orientieren, der Gegner ist sonst sehr schwerfällig sich zu drehen
	public float speed;

	LineRenderer line;
	float timer;// Timer for counting up to the next attack.
	private float reloadTime = 1f;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player").transform;
		rb = gameObject.GetComponent<Rigidbody> ();
		line = gameObject.GetComponent<LineRenderer> ();
		line.enabled = false;
	}
	
	// Update is called once per frame
	//ANGLE KANN FUER FIELDOFVIEW GENUTZT WERDEN, WENN ETWAS DARIN IST, USW.
	/*
	 * 	float Angle = Vector3.Angle (newEnemyVector, gameObject.transform.forward);

		//Debug.Log ("BASED POSITION" + Angle);

		var relativePoint = transform.InverseTransformPoint(player.position);
		if (relativePoint.x < 0.0){
			//print ("Object is to the left");
			//print ("AllTogether:"+relativePoint);
			//Torque(relativePoint);
		}
		else if (relativePoint.x > 0.0){
			print ("Object is to the right");
			Torque(new Vector3(0,0,0));
			       }
		else
			print ("Object is directly ahead");
	 */
	void Update () {
		playerDistance = Vector3.Distance(player.position, transform.position);
		Vector3 newEnemyVector = player.position-gameObject.transform.position;

		var newRotation = Quaternion.LookRotation(player.position-transform.position, Vector3.up);
		transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * dragTime);
		if (playerDistance >= 15f) {
			Move ();
		}

	   //Wenn 0 dann zielt er genau auf den Spieler;
	   float Angle = Vector3.Angle (newEnemyVector, gameObject.transform.forward);
	   
	   //Shoot
	   // Add the time since Update was last called to the timer.
	   timer += Time.deltaTime;
	   //To show the laser for a couple of seconds and then disable it
	   if (timer >= 0.2f && line.enabled) {
			line.enabled = false;
		}
		if (Angle <= 15f) {
			if(timer >= reloadTime /*&& playerInRange && enemyHealth.currentHealth > 0*/){
				Shoot ();
			}
		}
		
	}

	void Move(){
		rb.AddForce (transform.forward*speed);
	}

	void Shoot(){
		// Reset the timer.
		timer = 0f;
		
		line.enabled = true;
		Ray ray = new Ray(transform.position, transform.forward);
		RaycastHit hit;
		
		//line.SetPosition(0,transform.position);
		line.SetPosition(0,ray.origin);
		
		// Set the shootRay so that it starts at the end of the gun and points forward from the barrel.
		ray.origin = transform.position;
		ray.direction = transform.forward;
		
		if(Physics.Raycast(ray, out hit, 100)){
			
			// trifft irgendwas
			if(hit.collider.gameObject.name.Equals("Player")){
				print ("HIT PLAYER");
				//if(playerHealth.currentHealth > 0) {
				//	playerHealth.ApplyDamage (damage);
				//}
			}
			line.SetPosition(1,hit.point);
			//else if(hit.collider.gameObject.tag.Equals("Station")){
			//DAMAGE TO SPACESTATION
			//}
			//hit.rigidbody.AddForceAtPosition(transform.forward*10,hit.point);
		} else {
			line.SetPosition(1,ray.GetPoint(100));
		}
	}
}
