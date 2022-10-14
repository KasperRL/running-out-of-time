using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RefuelGoal : MonoBehaviour
{
    public GameObject instructions;
    public TextMeshProUGUI instructionsText;
    public GameObject fuelCan;

    private QuestManager questManager;
    private Inventory inventory;
    private bool isInRange;
    
    void Awake()
    {
        questManager = FindObjectOfType<QuestManager>();
        inventory = FindObjectOfType<Inventory>();
        if (questManager != null && questManager.quest.isActive)
        {
            Instantiate(fuelCan, new Vector3(-4f, 0.3f, 69.5f), fuelCan.transform.rotation);
        }
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isInRange && !questManager.quest.goal.isReached && inventory.HasItem("Fuel"))
        {
            questManager.quest.goal.ItemCollected();
            inventory.removeItem("Fuel");
            instructions.SetActive(false);
            questManager.completedQuests.Add(questManager.quest);
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
