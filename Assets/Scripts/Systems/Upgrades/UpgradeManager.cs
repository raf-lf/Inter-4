using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager: MonoBehaviour
{
    public UpgradeList upgradeList;

    private void Start()
    {
        //        upgradeList.SetupUpgrades(this);

        foreach (var item in GameManager.upgradesPurchased)
        {
            item.ApplyUpgrade(transform);
        }
    }

}
