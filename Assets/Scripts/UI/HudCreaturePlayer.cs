using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class HudCreaturePlayer : HudCreature
{
    public GameObject epBar;
    public Image epBarFill;

    public override void UpdateValues()
    {
        creature = GetComponentInParent<CreatureAtributes>();
        PlayerAtributes player = creature.GetComponent<PlayerAtributes>();

        hpBarFill.fillAmount = (float)creature.hp / (float)creature.hpMax;
        epBarFill.fillAmount = (float)player.energy / (float)player.energyMax;

    }
}
