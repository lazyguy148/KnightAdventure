using UnityEngine;
using UnityEngine.UI;
public class GameOverUI : MonoBehaviour
{
    public Text finalScoreText;

    void Start()
    {
        finalScoreText.text = "Your Score: " + ScoreManager.instance.GetCurrentScore().ToString();
    }
}
