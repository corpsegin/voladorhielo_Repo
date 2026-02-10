using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    private bool didDialogueStart;
    private int lineIndex;
    private float typingTime = 0.05f;

    [Header("text")]
    [SerializeField, TextArea(4, 6)] string[] dialogueLines;
    [SerializeField] GameObject dialoguePanel;
    [SerializeField] TMP_Text dialogueText;

    [Header("Character animation")]
    [SerializeField] Animator characterAnimator;
    [SerializeField] Animator barrierAnimator;
    [SerializeField] DialogueAction[] dialogueAction;
    public static bool interacted = false;

    [Header("activator settings")]
    [SerializeField] GameObject PeceraExplosiva;
    [SerializeField] GameObject PeceraNormal;
    [SerializeField] GameObject willObject;
    [SerializeField] float willDisappearDelay = 1f;
    [SerializeField] GameObject barrier;
    [SerializeField] GameObject Cajon;
    [SerializeField] GameObject CajonCorrecto;
    [SerializeField] Collider2D attack;
    [SerializeField] GameObject barrierCollider;


    private bool actionPlayed;

    [System.Serializable]



    public class DialogueAction
    {
        public int dialogueLine;
        public string animationTrigger;
        [HideInInspector] public bool played;
    }


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

        CheckDialogueAction();

        if (lineIndex >= dialogueLines.Length)
        {
            dialoguePanel.SetActive(false);
            didDialogueStart = false;

            if (CompareTag("Activator"))
            {
                PeceraExplosiva.SetActive(true);
                PeceraNormal.SetActive(false);
                StartCoroutine(ActivatePecera());
            }

            if (CompareTag("Key"))
            {
                Destroy(gameObject);
                gameObject.SetActive(false);
                Cajon.SetActive(false);
                CajonCorrecto.SetActive(true);

            }
            if (CompareTag("Stair"))
            {
                Destroy(gameObject);
            }

            if (CompareTag("Cajon"))
            {
                interacted = true;
            }

            if (CompareTag("Barrier") && interacted == true)
            {
                barrierAnimator.SetTrigger("Open");
                GetComponent<Collider2D>().enabled = false;
                barrierCollider.SetActive(false);
            }


            return;
        }

        StopAllCoroutines();
        StartCoroutine(ShowLine());
    }

    void CheckDialogueAction()
    {
        if (!CompareTag("Will")) return;

        foreach (DialogueAction action in dialogueAction)
        {
            if (lineIndex == action.dialogueLine && !action.played)
            {
                characterAnimator.SetTrigger(action.animationTrigger);
                action.played = true;
            }
        }


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

    IEnumerator ActivatePecera()
    {

        yield return new WaitForSeconds(willDisappearDelay);

        if (willObject != null)
        {
            willObject.SetActive(false);

        }
    }  


    void Update()
    {
        if (interacted == true)
        {
            Debug.Log(Dialogue.interacted);
        }


    }
}
