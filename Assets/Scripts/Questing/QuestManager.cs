using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestManager : MonoBehaviour
{
    public Quest quest;
    public List<Quest> completedQuests = new List<Quest>();

    public GameObject questBox;
    public TextMeshProUGUI questText;
    public Slider questProgress;
    public TextMeshProUGUI timerText;
    
    private GameManager gameManager;
    
    private bool timerIsRunning;
    private float timeRemaining;
    
    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        
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
                StopTimer();
                completedQuests.Add(quest);
                gameManager.GameCompleted();
            }
            questProgress.value = quest.goal.currentAmount;
            questProgress.maxValue = quest.goal.requiredAmount;
        }

        // Check if the timer is running
        if (timerIsRunning)
        {
            // Check if the timer has reached 0
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                quest.isActive = false;
                questText.text = "Quest failed!";
                gameManager.GameOver();
            }
        }
        if (quest.isActive)
        {
            questText.text = quest.description;
        }
    }

    public void StartQuest(Quest quest)
    {
        this.quest = quest;
        quest.isActive = true;
        questBox.SetActive(true);
        questText.text = quest.description;
        StartTimer(70.0f);
    }

    public void StartTimer(float time)
    {
        timeRemaining = time;
        timerIsRunning = true;
    }

    public void StopTimer()
    {
        timerIsRunning = false;
        timerText.text = "Ready for launch!";
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = "Launch in: " + string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
