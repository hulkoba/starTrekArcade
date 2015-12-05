using UnityEngine;
using System.Collections;

public class EnemyMove : MonoBehaviour {

	public Transform walkpointArray;
	public NavMeshAgent agent;

	public int walkpointIndex;

	public bool targetFound;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent> ();
		walkpointArray = GameObject.Find ("Walkpoints").transform;
		walkpointIndex = 0;
		targetFound = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (targetFound != true) {
			if (walkpointArray.GetChild (walkpointIndex).position.x != gameObject.transform.position.x) {
				move (walkpointArray.GetChild (walkpointIndex).position);
			} else {
				Debug.Log ("HAT ES ERREICHT DAS ZIEL!");
				if (walkpointIndex < (walkpointArray.childCount - 1)) {
					walkpointIndex += 1;
				} else {
					walkpointIndex = 0;
				}
				move (walkpointArray.GetChild (walkpointIndex).position);
			}
		} else {

		}
	}

	public void move(Vector3 targetPosition){
		agent.SetDestination (targetPosition);
	}

	public void nextIndex(){
		Debug.Log("HAT ES ERREICHT DAS ZIEL!");
		if(walkpointIndex < (walkpointArray.childCount-1)){
			walkpointIndex += 1;
		}
		else{
			walkpointIndex = 0;
		}
	}
}
