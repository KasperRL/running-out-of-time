using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<string> items = new List<string>();

    void Awake()
    {
        // Make sure there is only one Inventory
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void AddItem(string item)
    {
        items.Add(item);
    }

    public void removeItem(string item)
    {
        items.Remove(item);
    }

    public bool HasItem(string item)
    {
        return items.Contains(item);
    }
}
