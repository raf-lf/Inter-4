using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Trigger_UiButton : TriggerBase
{
    public override void TriggerEvent()
    {
        Debug.Log("Event Triggered through " + this);
        base.TriggerEvent();
    }

}
