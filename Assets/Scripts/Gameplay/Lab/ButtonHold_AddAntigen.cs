using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHold_AddAntigen : ButtonHold
{
    private Element_Fabricator fabricator;
    [SerializeField]
    private float consumptionPerFrame;

    public AudioSource sfxLoopSource;

    protected override void Start()
    {
        base.Start();
        fabricator = GetComponentInParent<Element_Fabricator>();
        
    }

    private void Update()
    {
        if (buttonHeld && button.interactable)
        {
            if (!sfxLoopSource.enabled)
            {
                sfxLoopSource.enabled = true;
            }
            sfxLoopSource.pitch = 1 + (fabricator.antigenAdded / fabricator.currentAntigenTarget) * 1;

            consumptionPerFrame = (float)fabricator.currentAntigenTarget / 180;
            //Debug.Log(consumptionPerFrame);

            if (fabricator.antigenAdded < fabricator.currentAntigenTarget)
                fabricator.AddAntigen(consumptionPerFrame);
        }
        else if (sfxLoopSource.enabled)
        {
            sfxLoopSource.enabled = false;
            sfxLoopSource.pitch = 1;
        }
    }

}
