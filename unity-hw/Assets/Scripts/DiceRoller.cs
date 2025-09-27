using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class DiceRoller : MonoBehaviour
{
    [SerializeField] private ScoreView scoreView;
    
    public UnityEvent onRoll;
    public UnityEvent<int> onScoreDetermined;
    
    private int ScoreSum = 0;
    private List<Dice> dices = new();
    private WaitForSeconds requestDelay = new WaitForSeconds(0.5f);
    private bool rolling = false;

    public void Awake()
    {
        onRoll.AddListener(scoreView.DisplayWaitingMessage);
        onScoreDetermined.AddListener(scoreView.UpdateScore);
    }

    public void Roll()
    {
        if (rolling) return;
        onRoll.Invoke();
        foreach (var dice in dices)
        {
            dice.Roll();
        }
        StartCoroutine(WaitForScore());
        rolling = true;
    }

    public void CalculateScoreSum()
    {
        ScoreSum = 0;
        foreach (var dice in dices)
        {
            ScoreSum += dice.GetScore();
        }
        onScoreDetermined.Invoke(ScoreSum);
        rolling = false;
    }

    public void SetDices(List<Dice> dices)
    {
        this.dices = dices;
    }

    private IEnumerator WaitForScore()
    {
        yield return requestDelay;
        while (!AllStopped())
        {
            Debug.Log("Not all dices stopped yet, waiting...");
            yield return requestDelay;
        }
        Debug.Log("All dices stopped");
        CalculateScoreSum();
    }

    private bool AllStopped()
    {
        foreach (var dice in dices)
        {
            if (!dice.IsStill())
                return false;
        }

        return true;
    }
}
