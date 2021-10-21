using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager: MonoBehaviour
{
    public GameObject[] upgradeObjects = new GameObject[0];

    private void Start()
    {
        SetupUpgrades();
    }

    public void SetupUpgrades()
    {
        foreach (GameObject upgrade in GameManager.purchasedUpgrades)
        {
            Instantiate(upgrade,transform);
        }

    }
}
