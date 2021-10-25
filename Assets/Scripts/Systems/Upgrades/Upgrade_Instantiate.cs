using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade Instantiate Object", menuName = "Data/Upgrades/Instantiate Object")]

public class Upgrade_Instantiate : UpgradeBase
{
    public GameObject objectToInstantiate;
    public override void ApplyUpgrade(Transform transform)
    {
        base.ApplyUpgrade(transform);
        Instantiate(objectToInstantiate, transform);

    }
}
