using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public GameObject dialogueBox;
    public bool dialogueActive = false;
    
    private Queue<string> sentences;
    
    void Awake()
    {
        sentences = new Queue<string>(); // All sentences are stored in a queue to be able to display them one by one
    }

    public void StartDialogue(Dialogue dialogue)
    {
        dialogueActive = true;

        // Activating and initialize all dialogue UI
        dialogueBox.SetActive(true);
        nameText.text = dialogue.name;
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence); // Adding all sentences to the queue
        }

        DisplayNextSentence();
    }

    // This function is called everytime the user presses E
    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue(); // If there are no more sentences, end the dialogue
            return;
        }

        StopAllCoroutines(); // Stop typing last sentence if not finished
        string sentence = sentences.Dequeue();
        StartCoroutine(TypeSentence(sentence));
    }

    // This function is used to type a sentence one letter at a time
    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    // This function is called when there are no more sentences to display
    void EndDialogue()
    {
        // Hide dialogue UI
        dialogueBox.SetActive(false);
        dialogueActive = false;
        
        FindObjectOfType<NPC>().StartQuest(); // Start player quests
    }
}
