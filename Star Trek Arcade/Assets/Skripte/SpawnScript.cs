using UnityEngine;
using System.Collections;

public class SpawnScript : MonoBehaviour {

	public GameObject Example;
	public float WaitTime = 2f;

    public int enemyCounter = 0;

	private Transform Group;
    private Transform Player;

	// Use this for initialization
	void Start () {
		Group = GameObject.Find ("Enemies").transform;
        Player = GameObject.Find("Player").transform;
		StartCoroutine (CreateGameObject ());
	}

	private IEnumerator CreateGameObject(){

		// wait for some time
		yield return new WaitForSeconds (WaitTime);

        if (enemyCounter < 3) {
            // if originals exists
            if (Example != null)
            {

                // create new GameObject
                GameObject NewGO = Instantiate(Example);
                // set new random position
                NewGO.transform.position = new Vector3(Random.Range(-10, 10), Random.Range(0, 3), Random.Range(-2, 10));
                NewGO.transform.LookAt(Player);
                // name it
                NewGO.name = "Enemy_" + Group.childCount;
                // set group as parent
                NewGO.transform.parent = Group;
                // tell the camera the new
                enemyCounter = enemyCounter + 1;
				Debug.Log(enemyCounter);
            }
        }

		// call this function again
		StartCoroutine (CreateGameObject ());
	}
}
