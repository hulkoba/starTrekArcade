using UnityEngine;
using System.Collections;

public class lookAtYou : MonoBehaviour {

    public GameObject Enemy;
    public float WaitTime = 2f;

    private Transform Group;
    private int EnemyCounter;

    // Use this for initialization
    void Start() {
        EnemyCounter = 0;
        Group = GameObject.Find("Enemies").transform;
    }

    // Update is called once per frame
    void Update() {
        transform.rotation = Camera.main.transform.rotation;
        if (EnemyCounter < 10)
        {
            StartCoroutine(CreateEnemy());
        }
    }

    private IEnumerator CreateEnemy()
    {

        // wait for some time
        yield return new WaitForSeconds(WaitTime);

        // if originals exists
        if (Enemy != null)
        {

            // create new GameObject
            GameObject NewEnemy = Instantiate(Enemy);
            // set new random position
            NewEnemy.transform.position = new Vector3(Random.Range(-10, 10), Random.Range(0, 3), Random.Range(-2, 10));
            NewEnemy.GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value);
            // name it
            NewEnemy.name = "Enemy_" + Group.childCount;
            // set group as parent
            NewEnemy.transform.parent = Group;
            // tell the camera the new target
        }
    }
} 