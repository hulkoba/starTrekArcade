using UnityEngine;
using System.Collections;

public class flyingMovement : MonoBehaviour {

	public float speed;
	public float laserDamage;

	Rigidbody rb;

	GameObject enemy;

	LineRenderer line;
	float timer;// Timer for counting up to the next attack.
	private float reloadTime = 1f;


	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody> ();

		line = gameObject.GetComponent<LineRenderer> ();
		line.enabled = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		timer += Time.deltaTime;
		if (timer >= 0.2f && line.enabled) {
			line.enabled = false;
		}

		if (Input.GetKey (KeyCode.W)) {
			move (speed);
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
		if (Input.GetKey (KeyCode.Space)&&timer >= reloadTime) {
			shoot();
		}

		//Senkrecht zueinander ist 0, Parallel 1, genau hinter dir -1,
		//Es wird ein Kegel aufgezogen damit, mit Vector3.dot wird das Kreuzprodukt von deiner Position - Gegner Position gezogen,
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

	void shoot(){
		// Reset the timer.
		timer = 0f;
		
		line.enabled = true;
		Ray ray = new Ray(transform.position, transform.forward);
		RaycastHit hit;
		
		//line.SetPosition(0,transform.position);
		line.SetPosition(0,ray.origin);
		
		// Set the shootRay so that it starts at the end of the gun and points forward from the barrel.
		ray.origin = transform.position;
		ray.direction = transform.forward;
		
		if(Physics.Raycast(ray, out hit, 100)){
			
			// trifft irgendwas
			//if(hit.collider.gameObject.tag.Equals("Enemy")){
			//	hit.collider.gameObject.GetComponents("EnemyHealth").ApplyDamage(laserDamage);
			//}
			//line.SetPosition(1,hit.point);
			//else if(hit.collider.gameObject.tag.Equals("Station")){
			//DAMAGE TO SPACESTATION
			//}
			//hit.rigidbody.AddForceAtPosition(transform.forward*10,hit.point);
		} else {
			line.SetPosition(1,ray.GetPoint(100));
		}
	}

	public void setSpeed(float newSpeed){
		speed = newSpeed;
	}

	public void setLaserDamage(float newLaserDamage){
		laserDamage = newLaserDamage;
	}

	public void setReloadTime(float newReloadTime){
		reloadTime = newReloadTime;
	}

}
