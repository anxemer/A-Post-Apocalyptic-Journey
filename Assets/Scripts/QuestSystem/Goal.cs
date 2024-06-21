using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal
{
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
    public int CurrentAmount { get; set; }
    public int RequiredAmount { get; set; }

    public virtual void Init()
    {

    }

    public void Evaluate()
    {
        if (CurrentAmount >= RequiredAmount)
        {
            Complete();
        }
    }

    public void Complete()
    {
        IsCompleted = true;
    }
}
