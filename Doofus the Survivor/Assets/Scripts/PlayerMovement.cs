using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class PlayerMovement : MonoBehaviour
{
    private float playerSpeed ;
    public float horizontalInput;
    public float verticalInput;
    private PlayerData playerData;

    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        string jsonFilePath = Path.Combine(Application.streamingAssetsPath, "json.json"); // Adjust path if necessary
        string jsonString = File.ReadAllText(jsonFilePath); // Read the JSON file
        Debug.Log("JSON String: " + jsonString);

        GameData gameData = JsonUtility.FromJson<GameData>(jsonString); // Deserialize the JSON to a C# object

        playerSpeed = gameData.player_data.speed;

        playerSpeed *= playerSpeed;
        Debug.Log($"player speed : {playerSpeed}");
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * Time.deltaTime * playerSpeed * verticalInput);
        transform.Translate(Vector3.right * Time.deltaTime * playerSpeed * horizontalInput);

        if(transform.position.y < -2)
        {
            HandlePlayerDeath();
        }
    }

    void HandlePlayerDeath()
    {
        gameManager.gameOver();
        Destroy(gameObject);
    }
}




