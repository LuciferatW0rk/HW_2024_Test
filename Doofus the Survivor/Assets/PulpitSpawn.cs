using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.IO;

public class PulpitSpawn : MonoBehaviour
{
    public GameObject spawnItem;  // Reference to the Pulpit prefab
    public TMP_Text timervalue;

    private float spawnInterval;
    private float timeSinceLastSpawn;
    private float timealive; // Ensure this is a class-level variable

    private int minDestroyTime;
    private int maxDestroyTime;

    void Start()
    {
        string jsonFilePath = Path.Combine(Application.dataPath, "json.json"); // Adjust path if necessary
        string jsonString = File.ReadAllText(jsonFilePath); // Read the JSON file
        Debug.Log("JSON String: " + jsonString);

        GameData gameData = JsonUtility.FromJson<GameData>(jsonString); // Deserialize the JSON to a C# object

        if (gameData == null || gameData.pulpit_data == null)
        {
            Debug.LogError("Deserialization failed or data is null.");
            return; // Early exit if deserialization fails
        }

        // Access the data
        minDestroyTime = gameData.pulpit_data.min_pulpit_destroy_time;
        maxDestroyTime = gameData.pulpit_data.max_pulpit_destroy_time;
        spawnInterval = (float)gameData.pulpit_data.pulpit_spawn_time;

        // Ensure timealive is assigned properly
        timealive = UnityEngine.Random.Range(minDestroyTime, maxDestroyTime);

        timeSinceLastSpawn = 0.0f;

        //Debug.Log($"Min Pulpit Destroy Time: {minDestroyTime}");
        //Debug.Log($"Max Pulpit Destroy Time: {maxDestroyTime}");
        //Debug.Log($"Pulpit Spawn Time: {spawnInterval}");
        //Debug.Log($"Initial Time Alive: {timealive}");
    }

    void Update()
    {
        //Debug.Log("Time.deltaTime: " + Time.deltaTime);

        // Update timealive
        timealive -= Time.deltaTime;
        //Debug.Log("Time Alive : " + timealive);

        // Calculate seconds and milliseconds
        float seconds = Mathf.Floor(timealive);
        float milliseconds = (timealive - seconds) * 1000; // Convert fractional seconds to milliseconds

        // Format timealive to show seconds and milliseconds
        timervalue.text = string.Format("{0:0}.{1:00}", seconds, Mathf.FloorToInt(milliseconds/10));

        // Destroy object when time is up
        if (timealive <= 0)
        {
            //Debug.Log("Destroying object after " + timealive + " seconds");
            Destroy(gameObject);
            return;
        }

        // Handle spawning logic
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn >= spawnInterval)
        {
            SpawnRandomAdjacentPulpits();
            timeSinceLastSpawn = 0.0f;
        }
    }

    void SpawnRandomAdjacentPulpits()
    {
        if (spawnItem == null)
        {
            //Debug.LogError("Spawn item is not assigned in the inspector.");
            return;
        }

        // Possible spawn directions
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

        // Spawn at the first direction
        Instantiate(spawnItem, transform.position + directions[0], Quaternion.identity);
    }
}