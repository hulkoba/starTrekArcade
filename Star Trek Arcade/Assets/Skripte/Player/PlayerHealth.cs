using UnityEngine;
using System.Collections;

public class PlayerHealth : HealthController {

    public GUITexture healthGui;

    private LifePointController lifePointController;
    private float maxHealth;
    private GUIText messageText;
    private Rect guiRect;
    private float guiMaxWidth;

	// Use this for initialization
	void Start () {
        messageText = GameObject.FindGameObjectWithTag("Message").GetComponent<GUIText>();
        lifePointController = GameObject.FindGameObjectWithTag("GameController").GetComponent<LifePointController>();
        guiRect = new Rect(healthGui.pixelInset);
        guiMaxWidth = guiRect.width;
        maxHealth = health;
        Update();
	}

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
		//WaitForSeconds(0.5);//<----? Geht das?
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

    public override void Dying()
    {
		//KAMERA.PARTEN = NULL
		//DANN NACH HINTEN .TransForm UND MAN KÖNNTE DANN EXPLODIEREN 
		//DRAN DENKEN, DASS DIE KAMERA DANN GELÖSCHT WERDEN MUSS, WENN NEUES SPIEL GESTARTET WIRD
        base.Dying();

        lifePointController.LifePoints -= 1;

        if (lifePointController.LifePoints > 0)
            Invoke("Restart", 1);
        else
            messageText.text = "Game Over";
    }

    void Restart()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    // Update is called once per frame
    void Update () {
	    if(healthGui != null)
        {
            guiRect.width = guiMaxWidth * health / maxHealth;
            healthGui.pixelInset = guiRect;
        }
	}
}
