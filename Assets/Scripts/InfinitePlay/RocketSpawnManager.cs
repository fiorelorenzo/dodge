using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketSpawnManager : MonoBehaviour {

    public float spawnRate;
    public List<Transform> rocketSpawners;
    public GameObject simpleRocketPrefab;

    private float timeToSpawn = 0f;
    //private int difficultyLevel = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!GameManager.Instance.IsGamePaused() && GameManager.Instance.IsPlayerAlive())
        {
            if (timeToSpawn >= spawnRate)
            {
                var spawner = rocketSpawners[Random.Range(0, rocketSpawners.Count)];
                var rocket = Instantiate(simpleRocketPrefab, spawner.transform.position, Quaternion.identity);
                rocket.SetActive(true);
                Debug.Log("Spawned rocket at " + spawner.name);
                timeToSpawn = 0f;
            }
            else
            {
                timeToSpawn += Time.deltaTime;
            }
        }
	}
}
