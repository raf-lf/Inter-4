using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade Add Buff", menuName = "Data/Upgrades/Add Buff")]

public class Upgrade_AddBuff : UpgradeBase
{
    public BuffBase buff;
    public override void ApplyUpgrade(Transform transform)
    {
        base.ApplyUpgrade(transform);
        GameManager.scriptPlayer.GetComponentInChildren<BuffManager>().BuffApply(buff);

    }
}
