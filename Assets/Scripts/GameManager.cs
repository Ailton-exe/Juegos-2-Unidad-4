using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public int bestScore;

    public int currentScore;

    public int currentLevel = 0;

    public static GameManager singleton;

    void Awake()
    {
        if (singleton == null)
        {
            singleton = this;

        }
        else if (singleton != this)
        {
            Destroy(gameObject);
        }

        bestScore = PlayerPrefs.GetInt("HighScore");
    }

    public void NextLevel()
    {
        Debug.Log("Pasamos el nivel");
    }

    public void RestartLevel()
    {
        Debug.Log("Reiniciamos el nivel");
        singleton.currentScore = 0;
        FindFirstObjectByType<BallController>().ResetBall();
    }
    
    public void AddScore(int scoreToAdd)
    {
        currentScore += scoreToAdd; 

        if (currentScore > bestScore)
        {
            bestScore = currentScore;
            PlayerPrefs.SetInt("HighScore", currentScore);
        }
    }

    
}
