using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance; // Singleton
    private int currentScore;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Giữ lại qua các cảnh (scene)
        }
        else
        {
            Destroy(gameObject); // Tránh trùng lặp
        }
    }

    public void AddScore(int amount)
    {
        currentScore += amount;
        
    }

    public void SubtractScore(int amount)
    {
        currentScore -= amount;
        if (currentScore < 0) currentScore = 0; // Không để điểm âm
        Debug.Log("Điểm hiện tại: " + currentScore);
    }

    public int GetCurrentScore()
    {
        return currentScore;
    }

    public void ResetScore()
    {
        currentScore = 0;
    }
}
