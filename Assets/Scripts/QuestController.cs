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

    public void Update()
    {
        if (triggerActive)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                instructions.SetActive(false);
                conversationActive = true;
                Debug.Log("Conversation started with " + gameObject.name);
            }
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (gameObject.tag == "NPC")
            {
                if (!conversationActive)
                {
                    instructionsText.text = "Press \"E\" to talk to " + gameObject.name;
                    instructions.SetActive(true);
                    triggerActive = true;
                }
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            instructions.SetActive(false);
            triggerActive = false;
            conversationActive = false;
        }
    }
}
