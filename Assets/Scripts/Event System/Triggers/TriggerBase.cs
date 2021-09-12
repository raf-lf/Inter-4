using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class TriggerBase : MonoBehaviour
{
    public EventQueue eventQueue;

    public interface iTrigger
    {
        void TriggerEvent();
    }

    public virtual void TriggerEvent()
    {
        eventQueue.RunQueue();

    }
}
