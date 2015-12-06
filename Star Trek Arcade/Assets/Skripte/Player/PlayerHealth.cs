using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

	public GameObject explosion;
	public float shield;
	public float health;

	public void ApplyDamage(float damage) {
		if(health <= 0 && shield <= 0)
		{
			Dying();
		}
		else
		{
			if(shield > 0){
				ShieldDamaging(damage);
			}
			else{
				Damaging(damage);
			}
		}
	}

	public virtual void ShieldDamaging(float damage)
	{
		shield -= damage;
		if(shield<0) {
			float damageLeft = shield*-1;
			shield = 0;
			Damaging(damageLeft);
		}
	}

	//SIEHE ENEMYHEALTH FUER IDEEN
	public virtual void RechargeShield(){
		while (shield <=100) {
			shield += 5;
		}
	}

	public virtual void Damaging(float damage)
	{
		health -= damage;
	}

    public void Dying()
    {
		//KAMERA.PARTEN = NULL
		//DANN NACH HINTEN .TransForm UND MAN KÖNNTE DANN EXPLODIEREN
		//DRAN DENKEN, DASS DIE KAMERA DANN GELÖSCHT WERDEN MUSS, WENN NEUES SPIEL GESTARTET WIRD
		//instantiate an explosion at the same position as the ship
		Instantiate(explosion, transform.position, transform.rotation);
		Destroy(gameObject);

    }
	
}
