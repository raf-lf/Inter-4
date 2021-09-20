using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAtributes : CreatureAtributes
{
    [Header ("Player-Specific")]
    public int energy;
    public int energyMax;

    private void Awake()
    {
        GameManager.scriptPlayer = this;
    }

    public void EnergyChange(int value)
    {
        if (value != 0)
        {
            energy = Mathf.Clamp(energy + value, 0, energyMax);

            //Play feedback vfx animation 0 (Damage)
            if (value > 0 && feedbackScript != null)
                feedbackScript.PlayFeedback(2);

            if (hp == 0) Death();
        }
    }
}
