using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestController : MonoBehaviour
{
    public GameObject instructions;
    public TextMeshProUGUI instructionsText;

    private bool triggerActive = false;
    private bool conversationActive = false;

    private void Update()
    {
        // Check for input when the trigger is active
        if (triggerActive)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                instructions.SetActive(false);
                conversationActive = true; // Start the conversation when E is pressed
                Debug.Log("Conversation started with " + gameObject.name);
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
                if (!conversationActive)
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
            conversationActive = false;
        }
    }
}
