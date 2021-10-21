using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventQueue : MonoBehaviour
{
    [SerializeField]
    private EventBase[] eventSequence = new EventBase[0];
    public int currentIndex = 0;

    private void Start()
    {
        eventSequence = GetComponentsInChildren<EventBase>();
    }

    public void RunQueue()
    {
        eventSequence[currentIndex].RunEvent();
    }
}
