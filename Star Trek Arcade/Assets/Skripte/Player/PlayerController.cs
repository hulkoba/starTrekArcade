using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	//flyingSpeed
	private float speed;

//	private float laserDamage;

//	private float torpedoDamage;
//	public Transform torpedo;
//	public Transform torpedoSpawn;

	Rigidbody rb;

	GameObject enemy;


	float timer;// Timer for counting up to the next attack.
	private float reloadLaserTime = 1f;
//	private float reloadTorpedoTime = 5f;

	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody> ();

		speed = 55f;
	//	laserDamage = 10f;
	}

	// Update is called once per frame
	void FixedUpdate () {
		timer += Time.deltaTime;
		if (timer >= 0.2f) {

		}

		if (Input.GetKey (KeyCode.W)) {
			move (speed * 3);
		}
		if (Input.GetKey (KeyCode.LeftArrow)) {
			rotate(0f,-15f);
		}
		if (Input.GetKey (KeyCode.RightArrow)) {
			rotate(0f,15f);
		}
		if (Input.GetKey (KeyCode.UpArrow)) {
			rotate(-15f,0f);
		}
		if (Input.GetKey (KeyCode.DownArrow)) {
			rotate(15f,0f);
		}
		if (Input.GetKey (KeyCode.Space) && timer >= reloadLaserTime) {
			shootLaser();
		}
		// if(Input.GetKey(KeyCode.X) && timer >= reloadTorpedoTime){
		// 	Debug.Log("TORPEDO");
		// 	shootTorpedo();
		// }

		//Senkrecht zueinander ist 0, Parallel 1, genau hinter dir -1,
		//Es wird ein Kegel aufgezogen damit, mit Vector3.dot wird das Skalarprodukt von deiner Position - Gegner Position gezogen,
		//dann wird mit transform.forward das Kreuzprodukt gebildet und dann kann man halt gucken, je nachdem, wie  man zum Gegner steht ergibt das Kreuzprodukt bestimmte Werte, wie oben genannt
		//siehe Internet: Vector3.dot das Beispiel erläutert es gut
		//Damit kann man neben dem Schießen von Zielsuchenden Raketten bestimmt auch den Gegner fliegen lassen, also wenn der Spieler aus diesem
		//Bereich raus ist, dann richte dich etwas aus und fliege vorwärts oder so, so ist ein wenig nicht genaues Fliegen drin
		//Oder fürs schießen ein Random von ein paar Metern, dass man mal vorbei schießt, oder so
		//if (Vector3.Dot (transform.forward, (transform.position - enemy.transform.position).normalized) > 0.9) {
		//
		//}

	}

	void move(float forwardSpeed){
		rb.AddForce (GameObject.FindGameObjectWithTag("MainCamera").transform.forward*speed);
	}

	void rotate(float upDown, float leftRight){
		rb.AddTorque (transform.right*upDown+transform.up*leftRight);
	}

	void torque(float leftRight){
		rb.AddTorque (new Vector3(0f,0f,1f)*leftRight);
	}

	void shootLaser(){
		// Reset the timer.
		timer = 0f;

	}

	// public void shootTorpedo(){
	// 	torpedoSpawn.rotation = GameObject.FindGameObjectWithTag("MainCamera").transform.rotation;
	// 	torpedoSpawn.position = GameObject.FindGameObjectWithTag("MainCamera").transform.forward;
	// 	Instantiate(torpedo, torpedoSpawn.position, torpedoSpawn.rotation);
	// }

	public void setSpeed(float newSpeed){
		speed = newSpeed;
	}

	// public void setLaserDamage(float newLaserDamage){
	// 	laserDamage = newLaserDamage;
	// }

	public void setReloadTime(float newReloadTime){
		reloadLaserTime = newReloadTime;
	}

	// public void setTorpedoDamage(float newTorpedoDamage){
	// 	torpedoDamage = newTorpedoDamage;
	// }

}
