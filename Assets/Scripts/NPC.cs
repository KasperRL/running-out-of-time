using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPC : MonoBehaviour
{
    public GameObject instructions;
    public TextMeshProUGUI instructionsText;

    public Dialogue dialogue;
    public Quest quest;

    private DialogueManager dialogueManager;
    private QuestManager questManager;

    private bool inRange = false;

    void Awake()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        questManager = FindObjectOfType<QuestManager>();
    }

    private void Update()
    {
        // Check for input when in range
        if (inRange)
        {
            if (Input.GetKeyDown(KeyCode.E) && !dialogueManager.dialogueActive && !questManager.quest.isActive)
            {
                instructions.SetActive(false);

                dialogueManager.StartDialogue(dialogue);
            }
            else if (Input.GetKeyDown(KeyCode.E) && dialogueManager.dialogueActive && !questManager.quest.isActive)
            {
                dialogueManager.DisplayNextSentence();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (gameObject.tag == "NPC" && !questManager.quest.isActive)
            {
                // Only show the instructions if the player is close enough to the NPC and a conversation is not already active
                if (!dialogueManager.dialogueActive)
                {
                    inRange = true;
                    
                    instructionsText.text = "Press \"E\" to talk to " + gameObject.name;
                    instructions.SetActive(true);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inRange = false;
            dialogueManager.dialogueActive = false; // Reset dialogue state when player leaves the NPC's range
            
            instructions.SetActive(false);
            dialogueManager.dialogueBox.SetActive(false);
        }
    }

    public void StartDialogue()
    {
        dialogueManager.StartDialogue(dialogue);
        dialogueManager.dialogueActive = true;
    }

    public void StartQuest()
    {
        questManager.StartQuest(quest);
    }
}
