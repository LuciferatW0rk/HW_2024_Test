using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.IO;



public class PulpitSpawn : MonoBehaviour
{
    public GameObject spawnItem;  // Reference to the Pulpit prefab
    public TMP_Text timervalue;

    private float spawnInterval;
    private float timeSinceLastSpawn;
    private float timealive;

    private int minDestroyTime;
    private int maxDestroyTime;

    void Start()
    {
        string jsonFilePath = "Assets/json.json"; //path to the json file
        string jsonString = File.ReadAllText(jsonFilePath); // Read the JSON file
        GameData gameData = JsonUtility.FromJson<GameData>(jsonString); //Deserialize the JSON to a C# object

        //access the data
        minDestroyTime = gameData.PulpitData.MinPulpitDestroyTime;
        maxDestroyTime = gameData.PulpitData.MaxPulpitDestroyTime;
        spawnInterval = (float)gameData.PulpitData.PulpitSpawnTime;

        int timealive = UnityEngine.Random.Range(minDestroyTime, maxDestroyTime);

        timeSinceLastSpawn = 0.0f;

        Debug.Log($"Min Pulpit Destroy Time: {minDestroyTime}");
        Debug.Log($"Max Pulpit Destroy Time: {maxDestroyTime}");
        Debug.Log($"Pulpit Spawn Time: {spawnInterval}");
        Debug.Log($"Initial Time Alive: {timealive}");
    }

    void Update()
    {
        timealive -= Time.deltaTime;
        Debug.Log("Time Alive : " + timealive);
        timervalue.text = string.Format("{0:00:00}",timealive);

        if (timealive <= 0)
        {
            Debug.Log("Destroying object after " + timealive + " seconds");
            Destroy(gameObject);
            return;
        }
        timeSinceLastSpawn += Time.deltaTime;
        //Debug.Log("Time Since Last Spawn : " + timeSinceLastSpawn);

        if (timeSinceLastSpawn >= spawnInterval)
        {
            SpawnRandomAdjacentPulpits();
            timeSinceLastSpawn = 0.0f;
        }
    }

    void SpawnRandomAdjacentPulpits()
    {
        if (spawnItem != null)
        {
            // possible spawn directions
            Vector3[] directions = {
                new Vector3(9, 0, 0),  // Right
                new Vector3(-9, 0, 0), // Left
                new Vector3(0, 0, 9),  // Front
                new Vector3(0, 0, -9)  // Back
            };

            // Randomly shuffle the directions array
            for (int i = 0; i < directions.Length; i++)
            {
                Vector3 temp = directions[i];
                int randomIndex = UnityEngine.Random.Range(i, directions.Length);
                directions[i] = directions[randomIndex];
                directions[randomIndex] = temp;
            }

            // Spawn at the first two random directions
            Instantiate(spawnItem, transform.position + directions[0], Quaternion.identity);
        }
        else
        {
            Debug.LogError("Spawn item is not assigned in the inspector.");
        }
    }
}

[Serializable]
public class PulpitData
{
    public int MinPulpitDestroyTime;
    public int MaxPulpitDestroyTime;
    public double PulpitSpawnTime;
}

[Serializable]
public class GameData
{
    public PulpitData PulpitData;
}

