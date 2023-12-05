using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "New Dialog Data", menuName = "Dialog System/Dialog Data")]
public class Dialogue : ScriptableObject
{
    [System.Serializable]
    public struct DialogLine
    {
        public string characterName;
        [TextArea(3, 10)]
        public string dialogText;
    }

    public Dialogue[] dialogue;
    public TextMeshProUGUI textDisplay;
    public float typingSpeed = 0.05f;
}
