using UnityEngine;
using System.Collections;

public class HealthController : MonoBehaviour {

    public float health = 100;
	public float shield = 100;
	public bool beenShot = false;
    protected bool isDead = false;
	
	//VLLT hier Update aus LifePointController einbauen, damit
	//es beim Multiplayer einfacher ist zu trennen möglicherweise.

    void ApplyDamage(float damage)
    {
		beenShot = true;
        //health -= damage;
        if(health <= 0 && shield <= 0 && !isDead)
        {
            isDead = true;
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
		shield -= damage;//<---- DA NOCH WAS SUCHEN FUER EIGENTLICHEN SCHADEN
		beenShot = false;
		if(shield<0){
			float damageLeft = shield*-1;
			shield = 0;
			Damaging(damageLeft);
		}
		//Wait 0.5 Sec.
		//WaitForSeconds(0,5);//<----? Geht das?
		if (beenShot != true) {
			RechargeShield();
		}
	}

	public virtual void RechargeShield(){
		while (beenShot != true) {
			shield += 5;
			//wait 0.5 Sec.
		}
	}

	public virtual void Damaging(float damage)
    {
		health -= damage;
		beenShot = false;
    }

    public virtual void Dying()
    {

    }
}
