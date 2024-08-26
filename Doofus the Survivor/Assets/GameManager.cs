using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverScreen;
    public int PlayerScore;
    public Text scoreText;
    
    public void addScore()
    {
        PlayerScore = PlayerScore + 1;
        scoreText.text = PlayerScore.ToString();
    }

    public void restartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void gameOver()
    {
        Time.timeScale = 0;
        gameOverScreen.SetActive(true);
    }
}
