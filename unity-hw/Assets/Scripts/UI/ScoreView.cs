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

    private void Start()
    {
        DiceRoller.Instance.onScoreDetermined.AddListener(UpdateScore);
        DiceRoller.Instance.onRoll.AddListener(DisplayWaitingMessage);
    }

    private void UpdateScore(RoundResult roundResult)
    {
        var score = roundResult.Score;
        ScoreString = score.ToString();
        scoreText.text = ScoreString;
    }

    private void DisplayWaitingMessage()
    {
        ScoreString = "???";
        scoreText.text = ScoreString;
    }
}
