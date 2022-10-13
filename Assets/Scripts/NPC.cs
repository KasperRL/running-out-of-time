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

    private bool triggerActive = false;

    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        questManager = FindObjectOfType<QuestManager>();
    }

    private void Update()
    {
        // Check for input when the trigger is active
        if (triggerActive)
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
                    instructionsText.text = "Press \"E\" to talk to " + gameObject.name;
                    instructions.SetActive(true);
                    triggerActive = true;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            instructions.SetActive(false);
            dialogueManager.dialogueBox.SetActive(false);
            triggerActive = false;
            dialogueManager.dialogueActive = false;
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
