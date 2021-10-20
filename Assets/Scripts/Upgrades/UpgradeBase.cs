using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeBase : MonoBehaviour
{
    public int upgradeId;
    public int scienceCost;

    private void Start()
    {
        ApplyUpgrade();
    }

    public virtual void ApplyUpgrade()
    {

    }
}
