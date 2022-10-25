using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RepairGoal : MonoBehaviour
{
    public GameObject instructions;
    public TextMeshProUGUI instructionsText;
    
    private QuestManager questManager;

    private bool isInRange = false;
    
    void Start()
    {
        questManager = FindObjectOfType<QuestManager>();
    }

    void Update()
    {
        if (questManager.quest.goal.currentAmount == 2)
        {
            questManager.quest.description = "Repair the rocket.";

            if (Input.GetKeyDown(KeyCode.E) && isInRange && !questManager.quest.goal.isReached)
            {
                questManager.quest.goal.ItemCollected();
                instructions.SetActive(false);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && questManager.quest.goal.currentAmount == 2)
        {
            instructions.SetActive(true);
            instructionsText.text = "Press E to repair the rocket";
            isInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            instructions.SetActive(false);
            isInRange = false;
        }
    }
}
