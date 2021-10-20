using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHold : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    protected Button button;

    public bool buttonHeld;

    protected virtual void Start()
    {
        button = GetComponentInChildren<Button>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        buttonHeld = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        buttonHeld = false;
    }
}
