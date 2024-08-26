using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverScreen;
    public GameObject Music;
    public GameObject FallSound;
    public int PlayerScore;
    public Text scoreText;
    
    public void addScore()
    {
        PlayerScore = PlayerScore + 1;
        scoreText.text = PlayerScore.ToString();
    }

    public void restartGame()
    {
        Music.SetActive(true);
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Music.SetActive(true);
    }
    public void gameOver()
    {
        Time.timeScale = 0;
        Music.SetActive(false);
        FallSound.SetActive(true);
        gameOverScreen.SetActive(true);
    }
}
