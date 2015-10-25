using UnityEngine;
using System.Collections;

public class CreationScript : MonoBehaviour {

	public GameObject Example;
	public float WaitTime = 2f;

	private CameraScript Cam;
	private Transform Group;

	// Use this for initialization
	void Start () {
		Cam = GameObject.Find ("Main Camera").GetComponent<CameraScript>();
		Group = GameObject.Find ("Objects").transform;
		StartCoroutine (CreateGameObject ());
	}

	private IEnumerator CreateGameObject(){

		// wait for some time
		yield return new WaitForSeconds (WaitTime);

		// if originals exists
		if (Example != null) {

			// create new GameObject
			GameObject NewGO = Instantiate (Example);
			// set new random position
			NewGO.transform.position = new Vector3(Random.Range(-10,10),Random.Range(0,3), Random.Range(-2,10));
			NewGO.GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value);
			// name it
			NewGO.name = "Cube_" + Group.childCount;
			// set group as parent
			NewGO.transform.parent = Group;
			// tell the camera the new target
			Cam.SetTarget(NewGO.transform);
		}

		// call this function again
		StartCoroutine (CreateGameObject ());
	}
}
