using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LabPointerClick : MonoBehaviour, IPointerClickHandler
{
    public GameObject elementInterface;

    public void OnPointerClick(PointerEventData eventData)
    {
        OpenElement();
    }

    public void OpenElement()
    {
        elementInterface.SetActive(true);
        transform.parent.GetComponentInChildren<LabPointerCloseAll>().labElements.Add(this);
        transform.parent.GetComponent<Animator>().SetBool("open", false);
    }

    public void CloseElement()
    {
        elementInterface.SetActive(false);
        transform.parent.GetComponentInChildren<LabPointerCloseAll>().labElements.Remove(this);
        transform.parent.GetComponent<Animator>().SetBool("open", true);

    }

}
