using UnityEngine;
using System.Collections;

public class HealthController : MonoBehaviour {

    public float health = 3;
	public float shield = 100;
	public bool beenShot = false;
    private bool isDead = false;

    void ApplyDamage(float damage)
    {
        health -= damage;
        if(health <= 0 && shield <= 0 && !isDead)
        {
            isDead = true;
            Dying();
        }
        else
        {
			if(shield > 0){
				ShieldDamaging();
			}
			else{
				Damaging();
			}
        }
    }

	public virtual void ShieldDamaging()
	{
		shield -= 10;//<---- DA NOCH WAS SUCHEN FUER EIGENTLICHEN SCHADEN
		beenShot = false;
		//Wait 0.5 Sec.
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

	public virtual void Damaging()
    {

    }

    public virtual void Dying()
    {

    }
}
