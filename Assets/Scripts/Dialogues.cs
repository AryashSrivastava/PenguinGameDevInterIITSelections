using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogues : MonoBehaviour
{
    public GameObject dialogueBox; // The UI panel containing the dialogue box
    public TextMeshProUGUI textComponent; // Text component inside the dialogue box
    public string[] lines; // Array of dialogue lines
    public float textSpeed; // Speed of typing
    private int index; // Tracks the current dialogue line

    void Start()
    {
        dialogueBox.SetActive(false); // Ensure dialogue box is initially inactive
    }

    void Update()
    {
        // Continue or skip dialogue when left mouse button is pressed
        if (dialogueBox.activeSelf && Input.GetMouseButtonDown(0))
        {
            if (textComponent.text == lines[index])
            {
                NextLine(); // Go to the next line if the current one is finished
            }
            else
            {
                StopAllCoroutines(); // Skip typing
                textComponent.text = lines[index];
            }
        }
    }

    public void TriggerDialogue()
    {
        dialogueBox.SetActive(true); // Activate the dialogue box
        Time.timeScale = 0; // Pause the game
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        textComponent.text = string.Empty; // Clear current text
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c; // Type out each character
            yield return new WaitForSecondsRealtime(textSpeed); // Wait in real-time (not affected by Time.timeScale)
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            StartCoroutine(TypeLine());
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        dialogueBox.SetActive(false); // Deactivate the dialogue box
        Time.timeScale = 1; // Resume the game
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            TriggerDialogue(); // Start dialogue when the player enters the trigger
        }
    }
}
