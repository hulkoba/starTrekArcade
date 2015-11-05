using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {
	
	public Transform player;
	public float playerDistance;
	public float rotationDamping;
	public float moveSpeed;

	// Use this for initialization
	void Start () {
		Player = GameObject.Find("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		playerDistance = Vector3.Distance(player.position, transform.position);
		
		if(playerDistance < 15f){
			lookAtPlayer();
		}
		if(playerDistance < 12f){
			chase();
		}
	}
	
	void lookAtPlayer(){
		//transform.LookAt(Player);
		Quanternion rotation = Quanternion.LookRotation (player.position - transform.position);
		transform.rotation = Quanternion.Slerp(transform.rotation,rotation,Time.deltaTime * rotationDamping);
	}
	
	void chase(){
		transform.Translate(Vector3.forward = moveSpeed * Time.deltaTime);
	}
}
