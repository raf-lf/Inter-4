using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAtributes : CreatureAtributes
{
    [Header ("Player-Specific")]
    public int energy;
    public int energyMax;

    public delegate void DeathDelegate();
    public static DeathDelegate PlayerDeath;

    private void Awake()
    {
        GameManager.scriptPlayer = this;
    }
    protected override void Start()
    {
        base.Start();
        GameManager.PlayerControl = true;

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

    public override void Death()
    {
        dead = true;
        GameManager.PlayerControl = false;
        animator.Play("death");

        PlayerDeath();
    }


}
