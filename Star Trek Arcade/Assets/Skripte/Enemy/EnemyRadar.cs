using UnityEngine;
using System.Collections;

public class EnemyRadar : MonoBehaviour {

    public float fieldOfView = 100;
    public bool obstacleDetected = false;
    public bool playerDetected = false;
    public int importancePlayer;
    public int importanceStation;

   // private GameObject player;


	// Use this for initialization
	void Start () {
       // player = GameObject.FindGameObjectWithTag("Player");
        InvokeRepeating("Searching",0.5F,0.5F);
	}

    void Searching()
    {
		Debug.Log("FOUND THE ENEMY!");
        /*Vector3 direction = player.transform.position - transform.position;
        Ray ray = new Ray(transform.position, direction.normalized);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit, 5))
        {
            if(hit.collider.gameObject == player)
            {
                Vector3 dir = hit.transform.position - transform.position;
                float angle = Vector3.Angle(dir, transform.forward);
                if (angle < fieldOfView * 0.5f)

                    //MUSS BEARBEITET WERDEN, WAS GENAU GESUCHT WIRD, DESWEGEN AUCH IMPORTANCEPLAYER-STATION
                    //RANGE ZU PLAYER KUERZER ALS ZU STATION = PLAYER WIRD ANGEGRIFFEN
                    StopSearching();
            }
        }*/
    }

    public void StopSearching()
    {
        playerDetected = true;
        CancelInvoke("Searching");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Astroid"))
        {
            obstacleDetected = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Astroid"))
        {
            obstacleDetected = false;
        }
    }

	// Update is called once per frame
	void Update () {

	}
}
