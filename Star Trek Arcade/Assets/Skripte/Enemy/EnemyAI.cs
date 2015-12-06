using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {
	
	public Transform player;
	public float playerDistance;

	public Rigidbody rb;

	public float rotationDamping;

	public Transform stations;

	//ENEMY DATEN
	public float moveSpeed;
	public bool kamikaze;
	public float playerPrio;
	public float starPrio;
	public float damage;
	public float expDamage;
	public float laserSpeed;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		player = GameObject.Find("Player").transform;
		stations = GameObject.Find("Stations").transform;
	}
	
	// Update is called once per frame
	void Update () {
		playerDistance = Vector3.Distance(player.position, transform.position);
		var shortestStation = 500f;
		var stationDistanceHelper = 0f;
		for (int i = 0; i < stations.childCount; i++) {
			stationDistanceHelper = Vector3.Distance(stations.GetChild(i).position, transform.position);
			if(stationDistanceHelper < shortestStation){
				shortestStation = stationDistanceHelper;
			}
		}

		//Debug.Log(playerDistance);
		if (playerDistance < 10f) {
			//Debug.Log("SHOOT!");
			attackEnemy();
		}
		if(playerDistance < 15f){
			//Debug.Log ("CHASE!");
			gameObject.GetComponent<EnemyMove>().targetFound = true;
			gameObject.GetComponent<EnemyMove>().agent.enabled = false;
			chase();
		}
		if (playerDistance > 15f && playerDistance < 16f) {
			chase ();
		}
		if (playerDistance > 16f) {
			//Debug.Log ("ESCAPED!");
			gameObject.GetComponent<EnemyMove>().enabled = true;
			gameObject.GetComponent<EnemyMove>().targetFound = false;
			gameObject.GetComponent<EnemyMove>().agent.enabled = true;
		}
	}
	
	void lookAtPlayer(){
		//transform.LookAt(player);
		Quaternion rotation = Quaternion.LookRotation (player.position - transform.position);
		transform.rotation = Quaternion.Slerp(transform.rotation,rotation,Time.deltaTime * rotationDamping);
	}
	
	void chase(){
		lookAtPlayer();
		if (playerDistance > 5f) {
			//transform.Translate (transform.forward * moveSpeed * Time.deltaTime);
			rb.AddForce(transform.forward*moveSpeed);

		} else {
			if(kamikaze == true){
				//transform.Translate (transform.forward * moveSpeed * Time.deltaTime);
				//rb.AddForce(transform.forward*moveSpeed);
				//if HIT on Player ApplyDamage
				//player.gameObject.GetComponent<PlayerHealth>().ApplyDamage(expDamage);
			}
			else{
				if(playerDistance < 4f){
					rb.AddForce(-1*transform.forward*moveSpeed);
				}
				else{
					rb.velocity = Vector3.zero;
					rb.angularVelocity = Vector3.zero;
				}
			}
		}
	}

	void attackEnemy(){
		gameObject.GetComponentInChildren<EnemyShooting>().startFire(damage);	
	}
	
	//Spieler als Sphere oder Rigit Body einbauen, der Enemy hat selbst einen Sphere Collider
	//Sobald der Spieler in den Collider kommt, wird das hier ausgelöst, TriggerEnter
	//WICHTIG: TriggerEnter 
	// void OnTriggerEnter(Collider other)
    // {
        // if (other.gameObject.CompareTag("Astroid"))
        // {
            // obstacleDetected = true;
        // }
    // }

    // void OnTriggerExit(Collider other)
    // {
        // if (other.gameObject.CompareTag("Astroid"))
        // {
            // obstacleDetected = false;
        // }
    // }
}
