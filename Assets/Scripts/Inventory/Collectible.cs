using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    private Inventory inventory;
    private QuestManager questManager;

    public AudioClip pickupSound;

    void Awake()
    {
        inventory = FindObjectOfType<Inventory>();
        questManager = FindObjectOfType<QuestManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            // Increase the quest progress
            if (questManager != null && questManager.quest.isActive)
            {
                questManager.quest.goal.ItemCollected();
            }

            inventory.AddItem(gameObject.tag);

            AudioSource.PlayClipAtPoint(pickupSound, transform.position);
            
            Destroy(gameObject);
        }
    }
}
