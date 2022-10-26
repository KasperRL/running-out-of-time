using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RepairGoal : MonoBehaviour
{
    public GameObject instructions;
    public TextMeshProUGUI instructionsText;

    public int questId = 2;
    
    private QuestManager questManager;
    private AudioSource audioSource;

    private bool isInRange = false;
    
    void Start()
    {
        questManager = FindObjectOfType<QuestManager>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (questManager.quest.goal.currentAmount == questId)
        {
            questManager.quest.description = "Repair the rocket.";

            if (Input.GetKeyDown(KeyCode.E) && isInRange && !questManager.quest.goal.isReached)
            {
                instructions.SetActive(false);
                StartCoroutine(Repair());
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && questManager.quest.goal.currentAmount == questId)
        {
            instructions.SetActive(true);
            instructionsText.text = "Press 'E' to repair the rocket";
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

    IEnumerator Repair()
    {
        audioSource.Play();
        yield return new WaitWhile(() => audioSource.isPlaying);
        questManager.quest.goal.ItemCollected();
    }
}
