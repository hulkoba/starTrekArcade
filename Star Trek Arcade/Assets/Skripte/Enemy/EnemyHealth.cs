using UnityEngine;
using System.Collections;

public class EnemyHealth : HealthController {
	
	public int life = 3;

	// Use this for initialization
	void Start () {
		
	}
	
	public override void Damaging()
	{
		health -= 1;
		if(health <= 0){
			Dying();
		}
	}
	
	public override void Dying()
	{
		Destroy(gameObject);
		//Punkte vergeben
	}
}
