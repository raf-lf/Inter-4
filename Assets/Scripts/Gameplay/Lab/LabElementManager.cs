using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabElementManager : MonoBehaviour
{
    public LabElement[] elements = new LabElement[6];

    public Animator animShortcuts;

    public void OpenElement(LabElement element)
    {

        foreach (var item in elements)
        {
                if(item != element && item.elementActive)
                    item.CloseElement();
        }

        if (element.elementActive)
            element.CloseElement();
        else
            element.OpenElement();


    }
}
