using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager: MonoBehaviour
{
    public UpgradeBase[] allUpgrades = new UpgradeBase[0];

    private void Start()
    {
        // upgradeList.SetupUpgrades(this);

        GameManager.upgradesPurchased.Clear();

        for (int i = 0; i < allUpgrades.Length; i++)
        {
            if (GameManager.upgradesPurchasedId.Contains(allUpgrades[i].upgradeId))
            {
                allUpgrades[i].ApplyUpgrade(transform);
                GameManager.upgradesPurchased.Add(allUpgrades[i]);
            }

        }
        /*
        foreach (var item in GameManager.upgradesPurchased)
        {
            item.ApplyUpgrade(transform);
        }
        */
    }

}
