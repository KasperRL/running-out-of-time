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
    private float crateMaxX = 45f;
    private float crateMaxZ = 25f;
    private float crateMinX = -80f;
    private float crateMinZ = -30f;
    
    void Start()
    {
        questManager = FindObjectOfType<QuestManager>();
        inventory = FindObjectOfType<Inventory>();
    }

    void Update()
    {
        if (questManager.quest.goal.currentAmount == questId)
        {
            questManager.quest.description = "Find the missing gear crate.";
            if (!crateSpawned)
            {
                crateSpawned = true;
                Instantiate(crate, new Vector3(Random.Range(crateMinX, crateMaxX), .2f, Random.Range(crateMinZ, crateMaxZ)), cratePlaceholder.transform.rotation);
            }
        } else if (questManager.quest.goal.currentAmount == questId + 1 && inventory.HasItem("Gear Crate"))
        {
            questManager.quest.description = "Return the missing gear crate to the launch pad.";
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
