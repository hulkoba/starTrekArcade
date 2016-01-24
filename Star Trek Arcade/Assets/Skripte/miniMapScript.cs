using UnityEngine;
using System.Collections;

public class miniMapScript : MonoBehaviour {

	Vector3 forwardView;

	// Use this for initialization
	void Start () {
		forwardView = gameObject.transform.forward;
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.forward.Set (gameObject.transform.forward.x,forwardView.y,gameObject.transform.forward.z);
	}
}
