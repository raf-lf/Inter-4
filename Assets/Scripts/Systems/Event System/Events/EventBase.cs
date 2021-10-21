using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EventBase : MonoBehaviour
{
    public interface iEvent
    {
        void RunEvent();
    }
    
    public virtual void RunEvent()
    {
        Debug.Log("Event " + this + " played on " + gameObject.name);
    }
}
