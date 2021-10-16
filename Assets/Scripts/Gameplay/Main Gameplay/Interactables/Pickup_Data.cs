using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_Data : PickupBase
{
    public int componentId;
    public int quantity;

    public override void Pickup()
    {
        base.Pickup();
        ArenaManager.dataCollected = true;

        GameManager.scriptHud.UpdateHud();

    }
}
