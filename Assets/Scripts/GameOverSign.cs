using TMPro;
using UnityEngine;

public class GameOverSign : MonoBehaviour
{

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public Animator highScoreAnim;

    public void SetUp(int score, int highScore, bool gotHighScore)
    {
        scoreText.text = "Score: " + score.ToString("0");
        highScoreText.text = PlayerPrefs.GetInt("HighScore").ToString("0");
        if (gotHighScore) highScoreAnim.SetBool("HighScore", true);
    }
}
