using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade List", menuName = "Data/Upgrade List")]

public class UpgradeList : ScriptableObject
{

    //THIS IS NOT USED ATM
    public UpgradeBase[] upgrade = new UpgradeBase[0];
    public bool[] upgradePurchased = new bool[0];

    public void SetupUpgrades(UpgradeManager updateParent)
    {

    }


}
