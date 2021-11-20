using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_Data : PickupBase
{
    public GameObject alternateSpawn;

    private void Start()
    {
        if (GameManager.currentGameStage >= 5)
        {
            GameObject otherItem = Instantiate(alternateSpawn);
            otherItem.transform.position = transform.position;

            Destroy(gameObject);
        }
    }

    public override void Pickup()
    {
        base.Pickup();
        ArenaManager.dataCollected = true;

        GameManager.scriptHud.UpdateHud();

    }
}
