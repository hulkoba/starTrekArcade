using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public Transform player;
	Rigidbody rb;

	GameController gameController;
	EnemyHealth enemyHealth;

	[SerializeField] private AudioClip shotSound;
	private AudioSource audioSource;

	float playerDistance;
	float timeBetweenAttacks = 2f;
	float nextFire = 0.0f;

	public Transform shot;
	public Transform shotSpawn;

	public Transform spaceStation;

	Rigidbody rb;

	//0.5 usw. sorgt für langsames Drehen!!!
	public float dragTime;
	//Speed sollte sich an dragTime orientieren, der Gegner ist sonst sehr schwerfällig sich zu drehen
	float speed;

	// Use this for initialization
	void Awake () {

		gameController = GameObject.Find ("GameController").GetComponent<GameController>();
		player = GameObject.FindGameObjectWithTag("MainCamera").transform;
		enemyHealth = GetComponent<EnemyHealth>();
		rb = gameObject.GetComponent<Rigidbody> ();
		audioSource = GetComponent<AudioSource>();

		speed = dragTime * 10;
	}

	// Update is called once per frame
	//ANGLE KANN FUER FIELDOFVIEW GENUTZT WERDEN, WENN ETWAS DARIN IST, USW.
	/*
	 * 	float Angle = Vector3.Angle (newEnterpriseVector, gameObject.transform.forward);
		var relativePoint = transform.InverseTransformPoint(player.position);
		if (relativePoint.x < 0.0){
			//Torque(relativePoint);
		}
		else if (relativePoint.x > 0.0){
			Torque(new Vector3(0,0,0));  }
		else
			print ("Object is directly ahead"); */
	void FixedUpdate () {

		playerDistance = Vector3.Distance(player.position, transform.position);
		Vector3 newEnterpriseVector = player.position - gameObject.transform.position;



		//Wenn 0 dann zielt er genau auf den Spieler;
		float Angle = Vector3.Angle (newEnterpriseVector, gameObject.transform.forward);

		if (Angle <= 15f && playerDistance <= 20) {
			if(Time.time >= nextFire && gameController.frozen == false && enemyHealth.currentHealth > 0){
				nextFire = Time.time + timeBetweenAttacks;
				Shoot ();
			}
		}
	}

	void targetLook(Transform target){
		var newRotation = Quaternion.LookRotation(target.position - transform.position, Vector3.up);
		transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * dragTime);
		if (playerDistance >= 10  && gameController.frozen == false) {			
			Move ();
		}
	}

	void Move(){
		rb.AddForce (transform.forward*speed);
	}

	void Shoot(){
		shotSpawn.rotation = gameObject.transform.rotation;
		Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
		PlayShotSound();
	}

	private void PlayShotSound() {
		audioSource.clip = shotSound;
		audioSource.volume = 0.1f;
		audioSource.Play();
	}
}
