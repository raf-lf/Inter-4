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
        GetComponent<Animator>().SetBool("active", true);
        StartupElement();
        elementActive = true;

    }

    public virtual void CloseElement()
    {
        GetComponent<Animator>().SetBool("active", false);
        elementActive = false;

    }

}
