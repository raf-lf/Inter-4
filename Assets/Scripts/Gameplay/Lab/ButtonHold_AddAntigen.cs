using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHold_AddAntigen : ButtonHold
{
    private Fabricator fabricator;
    public int consumptionPerFrame;

    protected override void Start()
    {
        base.Start();
        fabricator = GetComponentInParent<Fabricator>();
        
    }

    private void Update()
    {
        if (buttonHeld && button.interactable)
        {
            if (fabricator.antigenAdded <= fabricator.currentAntigenTarget)
                fabricator.AddAntigen(consumptionPerFrame);
        }
    }

}
