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
            // Check if the game is completed
            if (quest.goal.IsReached())
            {
                quest.isActive = false;
                questText.text = "Quests completed!";

                StopTimer(); // Stop the timer
                StartCoroutine(EndGame()); // End the game and trigger the end scene
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
                questText.text = "Quests failed!";

                gameManager.GameOver(); // Trigger the game over UI
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
        
        // Show quest UI
        questBox.SetActive(true);
        questText.text = quest.description;

        StartTimer(240.0f); // Start the launch countdown
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

    // Display the time in minutes and seconds on quest UI
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = "Launch in: " + string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // End the game and trigger the end scene with a delay
    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(3);
        gameManager.GameCompleted();
    }
}
