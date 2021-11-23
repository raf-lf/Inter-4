using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeBase : ScriptableObject
{
    public int upgradeId;

    public virtual void ApplyUpgrade(Transform transform)
    {

    }
}
