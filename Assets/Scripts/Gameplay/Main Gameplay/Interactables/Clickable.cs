using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clickable : MonoBehaviour
{
    [Header("Base")]
    public bool ignoreProximity;
    [HideInInspector]
    public bool playerInRange;
    [HideInInspector]
    public bool unuseable;
    public GameObject useableFeedback;

    public void OnMouseDown()
    {
        Debug.Log("Click");

        if(ignoreProximity || playerInRange)
        {
            if(!unuseable)
                Click();
        }

    }

    public virtual void Click()
    {
        Debug.Log("Clicked on " + gameObject.name);
    }

    public virtual void PlayerNearby(bool on)
    {
        playerInRange = on;

        if(useableFeedback != null)
        {
            ParticleSystem[] particles = useableFeedback.GetComponentsInChildren<ParticleSystem>();
            if (particles != null)
            {
                foreach (ParticleSystem particle in particles)
                {
                    var emission = particle.emission;
                    emission.enabled = on;
                }
            }
        }

    }

}
