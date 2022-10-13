using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RefuelGoal : MonoBehaviour
{
    public GameObject instructions;
    public TextMeshProUGUI instructionsText;

    private QuestManager questManager;
    private Inventory inventory;
    private bool isInRange;
    
    void Awake()
    {
        questManager = FindObjectOfType<QuestManager>();
        inventory = FindObjectOfType<Inventory>();
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isInRange && !questManager.quest.goal.isReached && inventory.HasItem("Fuel"))
        {
            questManager.quest.goal.ItemCollected();
            inventory.removeItem("Fuel");
            instructions.SetActive(false);
        }
    }
    
    void OnTriggerEnter()
    {
        if (inventory.HasItem("Fuel"))
        {
            instructions.SetActive(true);
            instructionsText.text = "Press E to refuel the ship";
            isInRange = true;
        }
    }

    void OnTriggerExit()
    {
        instructions.SetActive(false);
        isInRange = false;
    }
}
