using System;
using TMPro;
using UnityEditor.Localization;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;

[RequireComponent(typeof(TextMeshProUGUI))]
[RequireComponent(typeof(LocalizeStringEvent))]
public class StatusTextView : MonoBehaviour
{
    [SerializeField] private LocalizedString drawStatusText;
    [SerializeField] private LocalizedString winStatusText;
    [SerializeField] private LocalizedString lossStatusText;
    [SerializeField] private LocalizedString waitStatusText;
    [SerializeField] private LocalizedString pressStatusText;

    private TextMeshProUGUI statusText;
    private LocalizeStringEvent localizeEvent;

    private void Awake()
    {
        statusText = GetComponent<TextMeshProUGUI>();
        localizeEvent = GetComponent<LocalizeStringEvent>();
    }

    private void Start()
    {
        DiceRoller.Instance.onScoreDetermined.AddListener(SetResultMessage);
        DiceRoller.Instance.onRoll.AddListener(SetWaitingMessage);
    }
    
    private void SetWaitingMessage()
    {
        localizeEvent.StringReference = waitStatusText;
    }

    private void SetPressMessage()
    {
        localizeEvent.StringReference = pressStatusText;
    }

    private void SetResultMessage(RoundResult result)
    {
        localizeEvent.StringReference = result.Outcome switch
        {
            RoundOutcome.Win => winStatusText,
            RoundOutcome.Loss => lossStatusText,
            RoundOutcome.Draw => drawStatusText,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}
