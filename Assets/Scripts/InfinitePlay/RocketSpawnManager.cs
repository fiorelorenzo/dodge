using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RocketSpawnManager : MonoBehaviour
{

    public float spawnRate;
    public List<Transform> rocketSpawners;
    public List<GameObject> rocketSpawnWarnings;
    public GameObject simpleRocketPrefab;
    public TextMeshProUGUI warningText;
    public float showWarningTime;

    private float timeToSpawn;
    private int index = -1;
    private Transform spawner;
    private GameObject warning;
    //private int difficultyLevel = 0;

    // Use this for initialization
    void Start()
    {
        warningText.gameObject.SetActive(false);
        timeToSpawn = spawnRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.IsGamePaused() && GameManager.Instance.IsPlayerAlive())
        {

            SetIndexes();

            if (timeToSpawn <= showWarningTime)
            {
                warningText.SetText(timeToSpawn.ToString("0"));
                warningText.gameObject.SetActive(true);
                warning.SetActive(true);
            }

            if (timeToSpawn <= 0f)
            {
                //var spawner = rocketSpawners[Random.Range(0, rocketSpawners.Count)];
                warningText.gameObject.SetActive(false);
                warning.SetActive(false);
                var rocket = Instantiate(simpleRocketPrefab, spawner.transform.position, Quaternion.identity);
                rocket.SetActive(true);
                Debug.Log("Spawned rocket at " + spawner.name);
                timeToSpawn = spawnRate;
                index = -1;
            }
            else
            {
                timeToSpawn -= Time.deltaTime;
            }
        }
    }

    public void DisableAll()
    {
        warningText.gameObject.SetActive(false);
        rocketSpawnWarnings.ForEach(a => a.SetActive(false));
    }

    private void SetIndexes()
    {
        if (index == -1)
        {
            index = Random.Range(0, rocketSpawners.Count);
            spawner = rocketSpawners[index];
            warning = rocketSpawnWarnings[index];
        }
    }

    private int GetSpawnerIndex()
    {
        if (index == -1)
            return Random.Range(0, rocketSpawners.Count);
        else
            return index;
    }
}
