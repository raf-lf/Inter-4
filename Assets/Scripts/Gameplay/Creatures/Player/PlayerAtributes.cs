using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAtributes : CreatureAtributes
{
    [Header ("Player-Specific")]
    public int energy;
    public int energyMax;

    public delegate void PlayerDeathDelegate();
    public static PlayerDeathDelegate PlayerDeath;

    private void Awake()
    {
        GameManager.scriptPlayer = this;
    }
    protected override void Start()
    {
        base.Start();
        GameManager.PlayerControl = true;

    }
    public bool SpendEnergy(int value)
    {
        if (value <= energy)
        {
            EnergyChange(-value);
            return true;
        }
        else
            return false;

    }

    public void EnergyChange(int value)
    {
        if (value != 0)
        {
            energy = Mathf.Clamp(energy + value, 0, energyMax);

            if (hud != null)
                hud.UpdateValues();

            //Play feedback vfx animation 0 (Damage)
            if (value > 0 && feedbackScript != null)
                feedbackScript.PlayFeedback(2);

            if (hp == 0) Death();
        }
    }

    public override void Death()
    {
        dead = true;
        GameManager.PlayerControl = false;
        anim.Play("death");

        PlayerDeath();
        OnDeath();
    }


}
