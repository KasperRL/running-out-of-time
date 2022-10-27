using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MissingGearGoal : MonoBehaviour
{
    public GameObject instructions;
    public TextMeshProUGUI instructionsText;
    public GameObject cratePlaceholder;
    public GameObject crate;

    public int questId = 3;
    
    private QuestManager questManager;
    private Inventory inventory;

    private bool isInRange = false;
    private bool crateSpawned = false;
    // All possible spawn locations for the crate
    private Vector3[] crateSpawnPositions = {
        new Vector3(15f, 0.1f, 24.6f),
        new Vector3(32.9f, 0.1f, 33.4f),
        new Vector3(-19.3f, 0.3f, -3f),
        new Vector3(-36f, 0.3f, -36.9f),
        new Vector3(-44.7f, 0f, 27.76f)
    };
    
    void Awake()
    {
        questManager = FindObjectOfType<QuestManager>();
        inventory = FindObjectOfType<Inventory>();
    }

    void Update()
    {
        if (questManager.quest.goal.currentAmount == questId)
        {
            questManager.quest.description = "Find the missing gear crate, it was dropped along the dirt road.";
            if (!crateSpawned) // Only spawn the missing create when this is the current quest and it hasn't been spawned yet
            {                
                // Spawn the crate at a random location from the crateSpawnPositions array
                int randomIndex = Random.Range(0, crateSpawnPositions.Length);
                Instantiate(crate, crateSpawnPositions[randomIndex], cratePlaceholder.transform.rotation);

                crateSpawned = true;
            }
        } else if (questManager.quest.goal.currentAmount == questId + 1 && inventory.HasItem("Gear Crate"))
        {
            questManager.quest.description = "Return the missing gear crate to the launchpad.";
        }

        if (Input.GetKeyDown(KeyCode.E) && isInRange && !questManager.quest.goal.isReached && inventory.HasItem("Gear Crate"))
        {
            instructions.SetActive(false); // Hide instructions UI
            
            cratePlaceholder.SetActive(true); // Show the crate placeholder as if the crate was placed there by the player

            // Increase the quest progress
            inventory.removeItem("Gear Crate");
            questManager.quest.goal.ItemCollected();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && questManager.quest.goal.currentAmount == questId + 1 && inventory.HasItem("Gear Crate")) // Only give the player the ability to place the crate when the player has the crate in their inventory
        {
            isInRange = true;
            
            // Activate instructions UI
            instructions.SetActive(true);
            instructionsText.text = "Press 'E' to place the gear crate.";
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isInRange = false;

            // Deactivate instructions UI
            instructions.SetActive(false);
        }
    }
}
