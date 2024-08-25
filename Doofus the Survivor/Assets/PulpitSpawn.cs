using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PulpitSpawn : MonoBehaviour
{
    public GameObject spawnItem;  // Reference to the Pulpit prefab
    float spawnInterval = 2.5f;  // Time interval between spawns
    float lifetime = 5.0f; // Time till the pulpit despawns
    private float timeSinceLastSpawn;
    private float timealive;

    void Start()
    {
        timeSinceLastSpawn = 0.0f;
        timealive = 0.0f;
    }

    void Update()
    {
        timealive += Time.deltaTime;
        Debug.Log("Time Alive : " + timealive);
        if (timealive > lifetime)
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
                int randomIndex = Random.Range(i, directions.Length);
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
