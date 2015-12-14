using UnityEngine;
using System.Collections;

public class flyingMovement : MonoBehaviour {

	private float speed;

	Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody> ();
		speed = 55f;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Input.GetKey (KeyCode.W)) {
			Debug.Log("BEFORE:"+rb.transform.position);
			Debug.Log("W PRESSED");
			move (speed);
			Debug.Log("AFTER:"+rb.transform.position);
		} else if (Input.GetKey (KeyCode.LeftArrow)) {
			Debug.Log("LEFT PRESSED");
			rotate(0f,-1f);
		}
		else if (Input.GetKey (KeyCode.RightArrow)) {
			Debug.Log("RIGHT PRESSED");
			rotate(0f,1f);
		}
	 	else if (Input.GetKey (KeyCode.UpArrow)) {
			Debug.Log("BEFORE:"+rb.transform.position);
			Debug.Log("UP PRESSED");
			rotate(55f,0f);
			Debug.Log("NOW:"+rb.transform.position);
		}
		else if (Input.GetKey (KeyCode.DownArrow)) {
			Debug.Log("DOWN PRESSED");
			rotate(-1f,0f);
		}

	}

	void move(float forwardSpeed){
		rb.AddForce (GameObject.FindGameObjectWithTag("MainCamera").transform.forward*speed);
	}

	void rotate(float upDown, float leftRight){
		rb.AddTorque (new Vector3(upDown,leftRight,0f));
	}

	void torque(float leftRight){
		rb.AddTorque (new Vector3(0f,0f,1f)*leftRight);
	}

	public void setSpeed(float newSpeed){
		speed = newSpeed;
	}
}
