using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulpitarea : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject scoreAquired;

    // Start is called before the first frame update
    void Start()
    {
        scoreAquired.SetActive(false);
        gameManager = GameObject.FindGameObjectWithTag("Logic").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        scoreAquired.SetActive(true);
        gameManager.addScore();
    }
}
