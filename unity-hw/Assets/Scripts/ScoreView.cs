using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    TextMeshProUGUI scoreText;
    private string ScoreString = "";

    private void Awake()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateScore(int score)
    {
        ScoreString = $"Score: {score}";
        scoreText.text = ScoreString;
    }

    public void DisplayWaitingMessage()
    {
        ScoreString = "Waiting for score...";
        scoreText.text = ScoreString;
    }
}
