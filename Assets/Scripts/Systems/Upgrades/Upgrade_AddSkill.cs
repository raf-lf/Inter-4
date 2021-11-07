using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade Add Skill", menuName = "Data/Upgrades/Add Skill")]

public class Upgrade_AddSkill : UpgradeBase
{
    public GameObject skillObject;
    public override void ApplyUpgrade(Transform transform)
    {
        base.ApplyUpgrade(transform);
        GameManager.scriptSkill.player1Skills.Add(skillObject);

    }
}
