using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_HandleUi : EventBase
{
    public enum handleMode
    { on, off }


    public GameObject canvasObject;
    public handleMode type;


public override void RunEvent()
    {
        base.RunEvent();
        
        switch (type)
        {
            case handleMode.on:
                canvasObject.SetActive(true);
                break;
            case handleMode.off:
                canvasObject.SetActive(false);
                break;
        }
    }
}
