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
    private Vector3[] crateSpawnPositions = {
        new Vector3(15f, 0.1f, 24.6f),
        new Vector3(32.9f, 0.1f, 33.4f),
        new Vector3(-19.3f, 0.3f, -3f),
        new Vector3(-36f, 0.3f, -36.9f),
        new Vector3(-44.7f, 0f, 27.76f)
    };
    
    void Start()
    {
        questManager = FindObjectOfType<QuestManager>();
        inventory = FindObjectOfType<Inventory>();
    }

    void Update()
    {
        if (questManager.quest.goal.currentAmount == questId)
        {
            questManager.quest.description = "Find the missing gear crate, it was dropped along the dirt road.";
            if (!crateSpawned)
            {
                crateSpawned = true;
                int randomIndex = Random.Range(0, crateSpawnPositions.Length);
                Instantiate(crate, crateSpawnPositions[randomIndex], cratePlaceholder.transform.rotation);
            }
        } else if (questManager.quest.goal.currentAmount == questId + 1 && inventory.HasItem("Gear Crate"))
        {
            questManager.quest.description = "Return the missing gear crate to the launchpad.";
        }

        if (Input.GetKeyDown(KeyCode.E) && isInRange && !questManager.quest.goal.isReached && inventory.HasItem("Gear Crate"))
        {
            instructions.SetActive(false);
            StartCoroutine(PlaceCrate());
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && questManager.quest.goal.currentAmount == questId + 1 && inventory.HasItem("Gear Crate"))
        {
            isInRange = true;
            instructions.SetActive(true);
            instructionsText.text = "Press 'E' to place the gear crate.";
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

    IEnumerator PlaceCrate()
    {
        cratePlaceholder.SetActive(true);
        yield return new WaitForSeconds(1f);
        questManager.quest.goal.ItemCollected();
        inventory.removeItem("Gear Crate");
    }
}
