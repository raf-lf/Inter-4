using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Trigger_EnterAreaObjectTag : TriggerBase
{
    public bool oneShot;
    public string enteringObjectTag = "Player";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag(enteringObjectTag))
        {
            TriggerEvent();
        }

        
    }

    public override void TriggerEvent()
    {
        if (oneShot)
            gameObject.SetActive(false);

        Debug.Log("Event Triggered through " + this);
        base.TriggerEvent();
    }

}
