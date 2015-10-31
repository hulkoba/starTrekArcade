﻿using UnityEngine;
using System.Collections;

public class CreationScript : MonoBehaviour {

	public GameObject Example;
	public float WaitTime = 2f;

    private int enemyCounter = 0;
    private CameraScript Cam;
	private Transform Group;
    private Transform Player;

	// Use this for initialization
	void Start () {
		Group = GameObject.Find ("Enemies").transform;
        Player = GameObject.Find("Player").transform;
		StartCoroutine (CreateGameObject ());
	}

	private IEnumerator CreateGameObject(){

		// wait for some time
		yield return new WaitForSeconds (WaitTime);

        if (enemyCounter < 10) {
            // if originals exists
            if (Example != null)
            {
                Debug.Log("Spawn Enemy");
                // create new GameObject
                GameObject NewGO = Instantiate(Example);
                // set new random position
                NewGO.transform.position = new Vector3(Random.Range(-10, 10), Random.Range(0, 3), Random.Range(-2, 10));
                NewGO.transform.LookAt(Player);
                // name it
                NewGO.name = "Enemy_" + Group.childCount;
                // set group as parent
                NewGO.transform.parent = Group;
                // tell the camera the new 
                enemyCounter = enemyCounter + 1;
            }
        }

		// call this function again
		StartCoroutine (CreateGameObject ());
	}
}
