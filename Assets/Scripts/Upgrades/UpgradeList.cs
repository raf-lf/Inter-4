using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade List", menuName = "Database")]

public class UpgradeList : ScriptableObject
{
    public List<UpgradeBase> upgradeList = new List<UpgradeBase>();

}
