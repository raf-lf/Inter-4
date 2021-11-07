using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickableDialogue : MonoBehaviour, IPointerClickHandler
{

    public Dialogue[] dialogueToPlay = new Dialogue[0];
    public DialogueType type;


    public void OnPointerClick(PointerEventData eventData)
    {
        
        if(GetComponent<Button>())
        {
            if (GetComponent<Button>().interactable)
                PlayDialogue();
        }
        else
            PlayDialogue();
        
    }

    public void PlayDialogue()
    {
        int roll = Random.Range(0, dialogueToPlay.Length -1);

        GameManager.scriptDialogue.SetupDialogue(dialogueToPlay[roll], type);

    }
}
