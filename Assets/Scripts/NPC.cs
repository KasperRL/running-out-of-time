using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPC : MonoBehaviour
{
    public GameObject instructions;
    public TextMeshProUGUI instructionsText;
    public Dialogue dialogue;

    private bool triggerActive = false;
    private bool dialogueStarted = false;

    private void Update()
    {
        // Check for input when the trigger is active
        if (triggerActive)
        {
            if (Input.GetKeyDown(KeyCode.E) && !dialogueStarted)
            {
                // Start the dialogue
                instructions.SetActive(false);
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
                dialogueStarted = true;
            }
            else if (Input.GetKeyDown(KeyCode.E) && dialogueStarted)
            {
                // Display the next sentence
                FindObjectOfType<DialogueManager>().DisplayNextSentence();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (gameObject.tag == "NPC")
            {
                // Only show the instructions if the player is close enough to the NPC and a conversation is not already active
                if (!dialogueStarted)
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
            triggerActive = false;
        }
    }

    public void StartDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        dialogueStarted = true;
    }
}
