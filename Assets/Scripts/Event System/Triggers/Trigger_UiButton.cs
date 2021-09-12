using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Trigger_UiButton : TriggerBase
{
    public GameObject uiToClose;

    public override void TriggerEvent()
    {
        base.TriggerEvent();

        if(uiToClose != null)
        {
            Debug.LogError("UiToClose not setup!");

        }
    }


}
