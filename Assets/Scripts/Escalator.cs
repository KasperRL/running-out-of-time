using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Escalator : MonoBehaviour
{
    public GameObject instructions;
    public TextMeshProUGUI instructionsText;
    
    private bool triggerActive = false;
    private bool isUp = false;

    void Update()
    {        
        if (triggerActive)
        {
            if (Input.GetKeyDown(KeyCode.E) && !isUp)
            {
                gameObject.GetComponent<Animation>().Play("Up");
                instructions.SetActive(false);
                isUp = true;
            }
            else if (Input.GetKeyDown(KeyCode.E) && isUp)
            {
                gameObject.GetComponent<Animation>().Play("Down");
                instructions.SetActive(false);
                isUp = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            instructionsText.text = "Press \"E\" to use the escelator";
            instructions.SetActive(true);
            triggerActive = true;
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
}
