using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {
	
	public Transform player;
	public float playerDistance;
	public float rotationDamping;
	public float moveSpeed;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		playerDistance = Vector3.Distance(player.position, transform.position);
		Debug.Log(playerDistance);
		if(playerDistance < 15f){
			lookAtPlayer();
		}
		if(playerDistance < 12f){
			chase();
		}
	}
	
	void lookAtPlayer(){
		//transform.LookAt(Player);
		Quaternion rotation = Quaternion.LookRotation (player.position - transform.position);
		transform.rotation = Quaternion.Slerp(transform.rotation,rotation,Time.deltaTime * rotationDamping);
	}
	
	void chase(){
		lookAtPlayer();
		transform.Translate(transform.forward * moveSpeed * Time.deltaTime);
	}
	
	//Spieler als Sphere oder Rigit Body einbauen, der Enemy hat selbst einen Sphere Collider
	//Sobald der Spieler in den Collider kommt, wird das hier ausgelöst, TriggerEnter
	//WICHTIG: TriggerEnter 
	void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Astroid"))
        {
            obstacleDetected = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Astroid"))
        {
            obstacleDetected = false;
        }
    }
}
