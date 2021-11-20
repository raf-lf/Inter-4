using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

public class HudCreaturePlayer : HudCreature
{
    public GameObject epBar;
    public Image epBarFill;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI epText;

    public override void UpdateValues()
    {
        creature = GetComponentInParent<CreatureAtributes>();
        PlayerAtributes player = creature.GetComponent<PlayerAtributes>();

        hpBarFill.fillAmount = (float)creature.hp / (float)creature.hpMax;
        epBarFill.fillAmount = (float)player.energy / (float)player.energyMax;

        //hpText.text = player.hp.ToString();
        //epText.text = player.energy.ToString();

    }
}
