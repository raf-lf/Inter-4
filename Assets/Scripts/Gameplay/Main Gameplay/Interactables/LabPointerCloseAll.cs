using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LabPointerCloseAll : MonoBehaviour, IPointerClickHandler
{
    public List<LabPointerClick> labElements = new List<LabPointerClick>();

    public void OnPointerClick(PointerEventData eventData)
    {
        foreach (var item in labElements)
        {
            item.CloseElement();
        }
    }
}
