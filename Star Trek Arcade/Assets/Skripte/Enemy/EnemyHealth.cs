using UnityEngine;
using System.Collections;

public class EnemyHealth : HealthController {
	
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
	
	public virtual IEnumerator ShieldDamaging(float damage)
	{
		shield -= damage;//<---- DA NOCH WAS SUCHEN FUER EIGENTLICHEN SCHADEN
		beenShot = false;
		if(shield<0){
			float damageLeft = shield*-1;
			shield = 0;
			Damaging(damageLeft);
		}
		//Wait 0.5 Sec.
		//WaitForSeconds(0.5f);//<----? Geht das?
		yield return new WaitForSeconds(0.5f);
		if (beenShot != true) {
			RechargeShield();
		}
	}

	/**
	 * Statt void muss IEnumerator hin, damit yield return new WaitforSeconds() geht.
	 * Dann wartet er mit Yield auf die 0.5 Sekunden und geht dann dort weiter.
	 **/
	public virtual IEnumerator RechargeShield(){
		while (beenShot != true) {
			shield += 5;
			yield return new WaitForSeconds(0.5f);
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
