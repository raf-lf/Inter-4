using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabElement : MonoBehaviour
{
    public bool elementActive;
    public virtual void StartupElement()
    {

    }

    public virtual void OpenElement()
    {
        if (!elementActive)
        {
            GameManager.scriptAudio.PlaySfxSimple(GameManager.scriptLab.sfxTabOpen);
            GetComponent<Animator>().SetBool("active", true);
            StartupElement();
            elementActive = true;
        }

    }

    public virtual void CloseElement()
    {
        if (elementActive)
        {
            GameManager.scriptAudio.PlaySfxSimple(GameManager.scriptLab.sfxTabClose);
            GetComponent<Animator>().SetBool("active", false);
            elementActive = false;
            GameManager.scriptDialogue.EndDialogue();
        }

    }

}
