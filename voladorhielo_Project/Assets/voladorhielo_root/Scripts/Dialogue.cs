using System.Collections;
using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    private bool didDialogueStart;
    private int lineIndex;
    private float typingTime = 0.05f;

    [SerializeField, TextArea(4, 6)] string[] dialogueLines;
    [SerializeField] GameObject dialoguePanel;
    [SerializeField] TMP_Text dialogueText;
    
    
    public void Interact()
    {
        if (!didDialogueStart)
        {
            StartDialogue();
        }
        else
        {
            NextLine();
        }
    }

    void StartDialogue()
    {
        didDialogueStart = true;
        dialoguePanel.SetActive(true);
        lineIndex = 0;
        StartCoroutine(ShowLine());
    }

    void NextLine()
    {
        lineIndex++;

        if (lineIndex >= dialogueLines.Length)
        {
            dialoguePanel.SetActive(false);
            didDialogueStart = false;
            
            if (CompareTag("Key") || CompareTag("Stair"))
            {
                Destroy(gameObject);
           
            }
            return;
        }

        StopAllCoroutines();
        StartCoroutine(ShowLine());
    }

    IEnumerator ShowLine()
    {
        dialogueText.text = "";

        foreach (char ch in dialogueLines[lineIndex])
        {
            dialogueText.text += ch;
            yield return new WaitForSeconds(typingTime);
        }
    }
}
