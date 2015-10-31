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

    public override void Damaging()
    {
        base.Damaging();
    }

    public override void Dying()
    {
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
