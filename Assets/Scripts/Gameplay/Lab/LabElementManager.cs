using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabElementManager : MonoBehaviour
{
    public LabElement[] elements = new LabElement[6];

    public Animator animShortcuts;

    public void OpenElement(LabElement element)
    {
        if(element.elementActive)
            element.CloseElement();
        else
            element.OpenElement();

        for (int i = 0; i < elements.Length; i++)
        {
            if (elements[i] != element)
                elements[i].CloseElement();

        }

    }
}
