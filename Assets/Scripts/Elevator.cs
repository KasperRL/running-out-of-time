using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Elevator : MonoBehaviour
{
    public GameObject instructions;
    public TextMeshProUGUI instructionsText;
    
    private bool inRange = false;
    private bool isUp = false;

    void Update()
    {        
        if (inRange && Input.GetKeyDown(KeyCode.E))
        {
            if (isUp)
            {
                MoveDown();
            } else
            {
                MoveUp();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inRange = true;
            
            instructionsText.text = "Press \"E\" to use the elevator";
            instructions.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inRange = false;
            
            instructions.SetActive(false);
        }
    }

    void MoveDown()
    {
        gameObject.GetComponent<Animator>().Play("Down"); // Play the elevator animation to go down
        
        instructions.SetActive(false); // Hide instructions UI

        isUp = false;
        inRange = false;
    }

    void MoveUp()
    {
        gameObject.GetComponent<Animator>().Play("Up"); // Play the elevator animation to go up

        instructions.SetActive(false); // Hide instructions UI
        
        isUp = true;
        inRange = false;
    }
}
