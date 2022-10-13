using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestGoal
{
    public GoalType goalType;
    public int requiredAmount;
    public int currentAmount;
    public bool isReached;

    public bool IsReached()
    {
        return currentAmount >= requiredAmount;
    }

    public void ItemCollected()
    {
        if (goalType == GoalType.Collect)
        {
            currentAmount++;
        }
    }

    public enum GoalType
    {
        Collect
    }
}
