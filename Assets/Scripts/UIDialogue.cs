using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIDialogue : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI dialogueText;
    Customer customer;

    void Start()
    {
        dialogueText.text = "";
    }

    public void UpdateDialogue(string text)
    {
        dialogueText.text = text;
    }
}
