using UnityEngine;
using System.Collections;

public class EnemyMove : MonoBehaviour {
	
	public NavMeshAgent agent;
	Transform target;

	public Transform stations;

	public float playerPrio;
	public float stationPrio;

	public Transform player;
	public float playerDistance;
	
	public float firingRange;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("Enterprise").transform;
		stations = GameObject.Find("Stations").transform;
		agent = GetComponent<NavMeshAgent> ();
		target = GameObject.Find("Enterprise").transform;
	}
	
	// Update is called once per frame
	void Update () {

		playerDistance = Vector3.Distance(player.position, transform.position);

		var shortestStation = Mathf.Infinity;
		var stationDistanceHelper = 0f;
		var targetHelper = "Station1";

		for (int i = 0; i < stations.childCount; i++) {
			stationDistanceHelper = Vector3.Distance(stations.GetChild(i).position, transform.position);
			if(stationDistanceHelper < shortestStation){
				shortestStation = stationDistanceHelper;
				targetHelper = stations.GetChild(i).name;
			}
		}

		if (playerDistance * playerPrio <= shortestStation * stationPrio) {
			changeTarget ("Enterprise");
		} else {
			//changeTarget (targetHelper);
		}

		agent.SetDestination(target.position);

		var targetDistance = Vector3.Distance(target.position,transform.position);
		if (targetDistance <= firingRange) {
			gameObject.GetComponent<EnemyShooting> ().startFire = true;
		} else {
			gameObject.GetComponent<EnemyShooting> ().startFire = false;
		}
	}

	public void changeTarget(string targetName){
		target = GameObject.Find (targetName).transform;
	}

}
