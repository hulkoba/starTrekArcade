using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public Transform player;

	[SerializeField] private AudioClip shotSound;
	private AudioSource audioSource;

	public float playerDistance = 22;
	float timeBetweenAttacks = 2f;
	float nextFire = 0.0f;

	public int damage = 10;

	//Collider benutzen dafuer
	bool playerInRange;

	PlayerHealth playerHealth;
	EnemyHealth enemyHealth;

	public Transform shot;
	public Transform shotSpawn;

	Rigidbody rb;

	//0.5 usw. sorgt für langsames Drehen!!!
	public float dragTime;
	//Speed sollte sich an dragTime orientieren, der Gegner ist sonst sehr schwerfällig sich zu drehen
	public float speed;



	// Use this for initialization
	void Awake () {
		player = GameObject.FindGameObjectWithTag("MainCamera").transform;
		playerHealth = player.GetComponent<PlayerHealth> ();
		enemyHealth = GetComponent<EnemyHealth>();
		rb = gameObject.GetComponent<Rigidbody> ();

		audioSource = GetComponent<AudioSource>();
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

		//print ("player in range " + playerInRange);
		if (Angle <= 15f) {

			if(Time.time >= nextFire /* && playerInRange*/  && enemyHealth.currentHealth > 0){
				nextFire = Time.time + timeBetweenAttacks;
				Shoot ();
			}
		}
	}

	void Move(){
		rb.AddForce (transform.forward*speed);
	}

	void Shoot(){
		shotSpawn.rotation = gameObject.transform.rotation;

		Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
		PlayShotSound();


		// if(playerHealth.currentHealth > 0) {
		// 	playerHealth.ApplyDamage (damage);
		// }
	}

	private void PlayShotSound() {
		audioSource.clip = shotSound;
		audioSource.volume = 0.1f;
		audioSource.Play();
	}
}
