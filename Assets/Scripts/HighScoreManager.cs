using TMPro;
using UnityEngine;

public class HighScoreManager : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI bestTimeText;

    private int highScore;
    private float bestTime;

    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", -1); 
        bestTime = PlayerPrefs.GetFloat("BestTime", -1); 

        if (highScore == -1)
        {
            highScoreText.text = "HighScore: None";
        }
        else
        {
            highScoreText.text = "HighScore: " + highScore.ToString();
        }

        if (bestTime == -1)
        {
            bestTimeText.text = "BestTime: None";
        }
        else
        {
            int minutes = Mathf.FloorToInt(bestTime / 60F);
            int seconds = Mathf.FloorToInt(bestTime % 60F);
            bestTimeText.text = "BestTime: " + string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    public void SaveHighScore(int newScore, float newTime)
    {
        PlayerPrefs.SetInt("HighScore", newScore);
        PlayerPrefs.SetFloat("BestTime", newTime);
        PlayerPrefs.Save();
    }
}
