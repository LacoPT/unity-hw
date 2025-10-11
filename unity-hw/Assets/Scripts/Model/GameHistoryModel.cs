using System;
using System.Collections.Generic;
using UnityEngine;

//There is no actual save / load logit Y E T , but let's pretend that this class exists for serialization purposes
//TODO: Rethink if i should use that class in the first place
public class GameHistoryModel
{
    private List<RoundResult> history = new();

    public void AddEntry(RoundResult entry)
    {
        history.Add(entry);
    }
    
    public void RemoveEntry(RoundResult entry)
    {
        history.Remove(entry);
    }

    public void Clear()
    {
        history.Clear();
    }
}
