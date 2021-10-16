using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_Component : PickupBase
{
    public int componentId;
    public int quantity;

    public override void Pickup()
    {
        base.Pickup();
        ArenaManager.componentsInInventory[componentId] += quantity;
        ArenaManager.componentsCollected++;
        GameManager.scriptHud.UpdateHud();

    }
}
