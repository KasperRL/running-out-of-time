using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    // This script is used to store the dialogue for each individual character
    public string name;
    [TextArea(3, 10)]
    public string[] sentences;
}
