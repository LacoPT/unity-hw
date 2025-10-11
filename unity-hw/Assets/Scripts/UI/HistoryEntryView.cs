using System;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;

public class HistoryEntryView : MonoBehaviour
{
    [SerializeField] private Color winColor;
    [SerializeField] private Color loseColor;
    [SerializeField] private Color drawColor;
    [SerializeField] private TMP_Text outcomeText;
    [SerializeField] private TMP_Text scoreText;

    [SerializeField] private LocalizedString historyWinText;
    [SerializeField] private LocalizedString historyLoseText;
    [SerializeField] private LocalizedString historyDrawText;

    private LocalizeStringEvent outcomeTextLocalize;
    
    private void Awake()
    {
        outcomeTextLocalize = outcomeText.GetComponent<LocalizeStringEvent>();
    }

    public void UpdateHistoryEntry(RoundResult result)
    {
        var score = result.Score;
        var outcome = result.Outcome;
        switch (outcome)
        {
            case RoundOutcome.Win:
                outcomeText.color = winColor;
                outcomeTextLocalize.StringReference = historyWinText;
                break;
            case RoundOutcome.Loss:
                outcomeText.color = loseColor;
                outcomeTextLocalize.StringReference = historyLoseText;
                break;
            case RoundOutcome.Draw:
                outcomeText.color = drawColor;
                outcomeTextLocalize.StringReference = historyDrawText;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(outcome));
        }
        scoreText.text = $"{score}";
    }
}