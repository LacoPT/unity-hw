using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class DiceRoller : MonoBehaviour
{
    private const float REQUEST_DELAY_TIME = 0.5f;
        
    public static DiceRoller Instance { get; private set; }
    
    public UnityEvent onRoll;
    public UnityEvent<RoundResult> onScoreDetermined;
    
    private int scoreSum;
    private List<Dice> dices = new();
    //caching this so we don't create a new object everytime
    private readonly WaitForSeconds requestDelay = new(REQUEST_DELAY_TIME);
    
    //if you think it's stupid but it works, that means that's not stupid
    //my way of prevention for input event triggering many times
    private bool rolling;

    public void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
        }
    }

    public void Roll()
    {
        if (rolling) return;
        foreach (var dice in dices)
        {
            dice.Roll();
        }
        StartCoroutine(WaitForScore());
        rolling = true;
        onRoll.Invoke();
    }

    //We don't actually create new instances of a list in Dice spawner, but you can never be too sure, so i'll keep this
    public void SetDices(List<Dice> dices)
    {
        this.dices = dices;
    }
    
    private void CalculateScoreSum()
    {
        scoreSum = 0;
        foreach (var dice in dices)
        {
            scoreSum += dice.GetScore();
        }
        var roundResult = GameState.Instance.GetResultFromScore(scoreSum);
        onScoreDetermined.Invoke(roundResult);
        rolling = false;
    }

    private IEnumerator WaitForScore()
    {
        //so calculation don't trigger immediately
        yield return requestDelay;
        while (dices.Any(dice => !dice.IsStill()))
        {
            //Debug.Log("Not all dices stopped yet, waiting...");
            yield return requestDelay;
        }
        //Debug.Log("All dices stopped");
        CalculateScoreSum();
    }
}
