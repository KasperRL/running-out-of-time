using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestManager : MonoBehaviour
{
    public Quest quest;

    public GameObject questBox;
    public TextMeshProUGUI questText;
    public Slider questProgress;
    
    void Awake()
    {
        // Make sure there is only one QuestManager
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Update()
    {
        // Check if the quest is active
        if (quest.isActive)
        {
            // Check if the quest is completed
            if (quest.goal.IsReached())
            {
                quest.isActive = false;
                questText.text = "Quest completed!";
            }
            questProgress.value = quest.goal.currentAmount;
            questProgress.maxValue = quest.goal.requiredAmount;
        }
    }

    public void StartQuest(Quest quest)
    {
        this.quest = quest;
        quest.isActive = true;
        questBox.SetActive(true);
        questText.text = "Quest: " + quest.description;
    }
}
