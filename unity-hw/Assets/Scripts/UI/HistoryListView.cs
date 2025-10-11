using System.Collections.Generic;
using UnityEngine;

public class HistoryListView : MonoBehaviour
{
    //There could be recycler (List item pool) logic and i roughly know how to do it but i didn't
    //have enough time to make it proper and debug it
    [SerializeField] private HistoryEntryView historyEntryPrefab;
    [SerializeField] private RectTransform contentParent;
    
    private List<HistoryEntryView> historyEntries = new();

    //Using the start function to ensure that singleton is already initialized
    private void Start()
    {
        GameState.Instance.historyEntryAdded.AddListener(AddHistoryEntry);
    }

    private void AddHistoryEntry(RoundResult roundResult)
    {
        var entry =  Instantiate(historyEntryPrefab, contentParent);
        entry.transform.SetAsLastSibling();
        entry.UpdateHistoryEntry(roundResult);
        historyEntries.Add(entry);
    }
    
    //as well there could be the pick deletion of entries by destroying (or disabling and adding it to the pool)
}