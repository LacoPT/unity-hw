using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

//Caught writing GameManager.cs in 4k, mods, ban this shitcoder
//(jk this is actually event bus + validation)
public class GameState : MonoBehaviour
{ 
    public const int MIN_DICE_COUNT = 1;
    public const int MAX_DICE_COUNT = 10;
    
    //Famous PascalCase naming incident
    [FormerlySerializedAs("LossConditionChanged")] public UnityEvent<int> lossConditionChanged;
    [FormerlySerializedAs("WinConditionChanged")] public UnityEvent<int> winConditionChanged;
    [FormerlySerializedAs("DiceCountChanged")] public UnityEvent<int> diceCountChanged;
    [FormerlySerializedAs("HistoryEntryAdded")] public UnityEvent<RoundResult> historyEntryAdded;
    [FormerlySerializedAs("HistoryEntryRemoved")] public UnityEvent<RoundResult> historyEntryRemoved;
    [FormerlySerializedAs("HistoryCleared")] public UnityEvent historyCleared;

    [FormerlySerializedAs("InitialLossCondition")] [SerializeField] public int initialLossCondition;
    [FormerlySerializedAs("InitialWinCondition")] [SerializeField] public int initialWinCondition;
    [FormerlySerializedAs("InitialDiceCount")] [SerializeField] [Range(MIN_DICE_COUNT, MAX_DICE_COUNT)]
    public int initialDiceCount;
    
    public static GameState Instance { get; private set; }

    private RulesModel Rules = new();
    private GameHistoryModel History = new();

    private void Awake()
    {
        if (Instance is null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    //Using start to ensure that singleton is initialized
    private void Start()
    {
        DiceRoller.Instance.onScoreDetermined.AddListener(AddEntryToHistory);
        ChangeDiceCount(initialDiceCount);
        ChangeWinCondition(initialWinCondition);
        ChangeLossCondition(initialLossCondition);
    }

    public void ChangeLossCondition(int lossCond)
    {
        var newLossCond = Math.Clamp(lossCond, Rules.DiceCount, Rules.WinCond - 1);
        Rules.LossCond = newLossCond;
        lossConditionChanged?.Invoke(newLossCond);
    }

    public void ChangeWinCondition(int winCond)
    {
        var minWinCond = Math.Min(Rules.LossCond + 1, Rules.DiceCount + 1);
        minWinCond = Math.Max(minWinCond, Rules.DiceCount + 1);
        var newWinCond = Math.Clamp(winCond, minWinCond, Rules.DiceCount * 6);
        Rules.WinCond = newWinCond;
        winConditionChanged?.Invoke(newWinCond);
    }

    public void ChangeDiceCount(int diceCount)
    {
        var newDiceCount = Math.Clamp(diceCount, MIN_DICE_COUNT, MAX_DICE_COUNT);
        Rules.DiceCount = newDiceCount;
        diceCountChanged?.Invoke(newDiceCount);
        ChangeWinCondition(Rules.WinCond);
        ChangeLossCondition(Rules.LossCond);
    }

    public void AddEntryToHistory(RoundResult result)
    {
        History.AddEntry(result);
        historyEntryAdded?.Invoke(result);
    }

    public void AddEntryToHistory(int score)
    {
        var result = GetResultFromScore(score);
        AddEntryToHistory(result);
    }


    public void RemoveEntryFromHistory(RoundResult result)
    {
        History.RemoveEntry(result);
        historyEntryRemoved?.Invoke(result);
    }

    public void ClearHistory()
    {
        History.Clear();
        historyCleared?.Invoke();
    }
    
    public RoundResult GetResultFromScore(int score)
    {
        var outcome = Rules.GetOutcome(score);
        return new RoundResult(score, outcome);
    }
}
