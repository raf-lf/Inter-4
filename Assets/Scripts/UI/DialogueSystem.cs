using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public enum expressions { neutral, happy, hype, serious, sad, dismay, angry }

public class DialogueSystem : MonoBehaviour, IPointerClickHandler
{
    public bool dialogueActive;
    public Dialogue currentDialogue;
    public TextMeshProUGUI textBox;
    public RectTransform assitantPoint;
    public Animator animAssistant;
    private int currentStep = 0;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (dialogueActive)
        {
          
            if (currentStep+1 == currentDialogue.lines.Length)
                EndDialogue();
            else
                NextStep();
        }
    }

    private void Awake()
    {
        GameManager.scriptDialogue = this;
    }

    private void Start()
    {
        //Vector3 assistantPosition = Camera.main.ScreenToWorldPoint(assitantPoint.pivot);
        //assistantPosition.z = 0;
        //animAssistant.gameObject.transform.position = assistantPosition;
    }

    public void SetupDialogue(Dialogue dialogue)
    {
        currentDialogue = dialogue;
        StartDialogue();
    }

    private void StartDialogue()
    {
        dialogueActive = true;
        gameObject.GetComponent<Animator>().SetBool("active", true);
        currentStep = 0;
        Step(currentStep);

    }

    public void EndDialogue()
    {
        dialogueActive = false;
        gameObject.GetComponent<Animator>().SetBool("active", false);
        animAssistant.SetTrigger("hide");

    }

    private void Step(int step)
    {
        Debug.Log("Step " + step + " of dialogue " + currentDialogue.name);
        WriteText(currentDialogue.lines[step]);

        animAssistant.SetTrigger(ReturnExpression(currentDialogue.linesExpression[step]));

    }
    private void WriteText(string text)
    {
        textBox.text = text;
    }
    private void NextStep()
    {
        currentStep++;
        Step(currentStep);

    }

    private string ReturnExpression(expressions expression)
    {
        switch (expression)
        {
            default:
            case expressions.neutral:
                return "neutral";
            case expressions.happy:
                return "happy";
            case expressions.hype:
                return "hype";
            case expressions.serious:
                return "serious";
            case expressions.sad:
                return "sad";
            case expressions.dismay:
                return "dismay";
            case expressions.angry:
                return "angry";
        }

    }
}
