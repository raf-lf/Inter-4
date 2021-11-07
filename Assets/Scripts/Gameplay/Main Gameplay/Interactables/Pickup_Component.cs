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
        GameManager.scriptArena.ComponentChange(componentId, quantity);

    }
}
