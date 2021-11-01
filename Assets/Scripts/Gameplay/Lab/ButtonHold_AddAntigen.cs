using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHold_AddAntigen : ButtonHold
{
    private Element_Fabricator fabricator;
    public int consumptionPerFrame;

    protected override void Start()
    {
        base.Start();
        fabricator = GetComponentInParent<Element_Fabricator>();
        
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
