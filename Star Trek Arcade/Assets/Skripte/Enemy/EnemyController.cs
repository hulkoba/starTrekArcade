using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public Transform player;
	public float playerDistance = 22;

	LineRenderer line;
	float timer;// Timer for counting up to the next attack.
	private float timeBetweenAttacks = 1f;

	public int damage = 10;
	//Collider benutzen dafuer
	bool playerInRange;

	PlayerHealth playerHealth;

	Rigidbody rb;

	//0.5 usw. sorgt für langsames Drehen!!!
	public float dragTime;
	//Speed sollte sich an dragTime orientieren, der Gegner ist sonst sehr schwerfällig sich zu drehen
	public float speed;



	// Use this for initialization
	void Awake () {
		player = GameObject.Find("Enterprise").transform;
		playerHealth = player.GetComponent<PlayerHealth> ();
		rb = gameObject.GetComponent<Rigidbody> ();
		line = gameObject.GetComponent<LineRenderer> ();
		line.enabled = false;
	}

	// enterprise is in collider range?
	void OnTriggerEnter(Collider other) {
		if(other.tag == "Enterprise") {
			print ("player in range " + other.tag);
			playerInRange = true;
		}
	}

	//enterprise is gone away
	void OnTriggerExit(Collider other) {
		if(other.tag == "Enterprise") {
			playerInRange = false;
		}
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
		if (playerDistance >= 7f) {
			Move ();
		}

	   //Wenn 0 dann zielt er genau auf den Spieler;
	   float Angle = Vector3.Angle (newEnemyVector, gameObject.transform.forward);


	   // Add the time since Update was last called to the timer.
	   timer += Time.deltaTime;
	   //To show the laser for a couple of seconds and then disable it
	   if (timer >= 0.2f && line.enabled) {
			line.enabled = false;
		}

		//print ("player in range " + playerInRange);
		if (Angle <= 15f) {
			// enterprise is close enough AND weapons are ready --> shoot
			if(timer >= timeBetweenAttacks /* && playerInRange  && enemyHealth.currentHealth > 0*/){
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
		//	Debug.Log ("hit collider name" + hit.collider.name);
			// trifft irgendwas
			if(hit.collider.gameObject.name == "Enterprise"){
		//		print ("HIT PLAYER");
				if(playerHealth.currentHealth > 0) {
					playerHealth.ApplyDamage (damage);
				}
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
