using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public string currentQuest;
    
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
        // If the player presses the "Q" key, print the current quest
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Current Quest: " + currentQuest);
        }
    }
}
