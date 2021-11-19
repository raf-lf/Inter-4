using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public enum DialogueType { oneShot, comment, cutscene }
public enum expressions { neutral, happy, hype, serious, sad, dismay, angry }

public class DialogueSystem : MonoBehaviour, IPointerClickHandler
{
    private Animator animDialogue;
    public Animator animAssistant;
    public AudioClip[] sfxMessage = new AudioClip[0];

    public bool dialogueActive;
    public Dialogue currentDialogue;
    public TextMeshProUGUI textBox;
    private int currentStep = 0;
    public DialogueType currentDialogueType;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (dialogueActive && currentDialogueType != DialogueType.oneShot)
            StepEnd();
    
    }

    private void Awake()
    {
        GameManager.scriptDialogue = this;
        animDialogue = GetComponent<Animator>();
    }


    public void SetupDialogue(Dialogue dialogue, DialogueType type)
    {
        if (dialogue != currentDialogue)
        {
            currentDialogue = dialogue;
            currentDialogueType = type;
            dialogueActive = true;

            if (type == DialogueType.oneShot || type == DialogueType.comment)
            {
                animDialogue.SetBool("comment", true);
            }

            else if (type == DialogueType.cutscene)
            {
                animDialogue.SetBool("cutscene", true);
            }

            currentStep = 0;
            Step(currentStep);
        }
    }

    public void EndDialogue()
    {
        if (dialogueActive)
        {
            dialogueActive = false;
            animDialogue.SetBool("comment", false);
            animDialogue.SetBool("cutscene", false);

            if(animAssistant != null)
                animAssistant.SetTrigger("hide");

            currentDialogue = null;
        }

    }

    private void Step(int step)
    {
        Debug.Log("Step " + step + " of dialogue " + currentDialogue.name);
        WriteText(currentDialogue.lines[step]);

        if (animAssistant != null)
            animAssistant.SetTrigger(ReturnExpression(currentDialogue.linesExpression[step]));

        int roll = Random.Range(0, sfxMessage.Length);
        GameManager.scriptAudio.PlaySfx(sfxMessage[roll],1,new Vector2(1,1.3f),GameManager.scriptAudio.sfxSource);
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

    public void StepEnd()
    {
        if (currentStep + 1 == currentDialogue.lines.Length)
            EndDialogue();
        else
            NextStep();

    }

    private string ReturnExpression(expressions expression)
    {
        animAssistant.ResetTrigger("hide");
        animAssistant.ResetTrigger("neutral");
        animAssistant.ResetTrigger("happy");
        animAssistant.ResetTrigger("hype");
        animAssistant.ResetTrigger("serious");
        animAssistant.ResetTrigger("sad");
        animAssistant.ResetTrigger("dismay");
        animAssistant.ResetTrigger("angry");

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
