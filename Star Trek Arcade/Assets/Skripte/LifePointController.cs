using UnityEngine;
using System.Collections;

public class LifePointController : MonoBehaviour {

    public GUIText lpText;
    private int lifePoints = 0;

    public int LifePoints
    {
        get
        {
            return lifePoints;
        }
        set
        {
            lifePoints = value;
            if(lifePoints < 0)
            {
                lifePoints = 0;
            }
            Update();
        }
    }

	// Use this for initialization
	void Start () {
	
	}

    void Awake()
    {
        LifePoints = PlayerPrefs.GetInt("LPs");
    }
	
	// Update is called once per frame
	void Update () {
        lpText.text = lifePoints.ToString();
	}

    void OnDestroy()
    {
        PlayerPrefs.SetInt("LPs", lifePoints);
    }
}
