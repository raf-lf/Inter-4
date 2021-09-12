using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class Trigger_PointerClick : TriggerBase, IPointerClickHandler
{
    public ParticleSystem particleVfx;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("aaaaaaaaaa");
        TriggerEvent();

    }

    public void Feedback()
    {
        if (particleVfx != null) 
            particleVfx.Play();

    }

    public override void TriggerEvent()
    {
        Debug.Log("Event Triggered through " + this);

        Feedback();

        base.TriggerEvent();
    }


}
