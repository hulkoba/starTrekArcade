using UnityEngine;
using System.Collections;

public class HealthController : MonoBehaviour {

    public float health = 3;
    private bool isDead = false;

    void ApplyDamage(float damage)
    {
        health -= damage;
        if(health <= 0 && !isDead)
        {
            isDead = true;
            Dying();
        }
        else
        {
            Damaging();
        }
    }

	public virtual void Damaging()
    {

    }

    public virtual void Dying()
    {

    }
}
