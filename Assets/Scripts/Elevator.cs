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
        if (inRange)
        {
            if (Input.GetKeyDown(KeyCode.E) && !isUp)
            {
                gameObject.GetComponent<Animator>().Play("Up");
                instructions.SetActive(false);
                isUp = true;
            }
            else if (Input.GetKeyDown(KeyCode.E) && isUp)
            {
                gameObject.GetComponent<Animator>().Play("Down");
                instructions.SetActive(false);
                isUp = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            instructionsText.text = "Press \"E\" to use the elevator";
            instructions.SetActive(true);
            inRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            instructions.SetActive(false);
            inRange = false;
        }
    }
}
