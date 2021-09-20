using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_Powerup : PickupBase
{
    public enum PowerupType { Health, Energy, Damage, Defense, Speed }

    public PowerupType buffType;
    public int ammount;

    public override void Pickup()
    {
        int powerupId = 0;

        switch (buffType)
        {
            case PowerupType.Health:
                powerupId = 0;
                break;
            case PowerupType.Energy:
                powerupId = 1;
                break;
            case PowerupType.Damage:
                powerupId = 2;
                break;
            case PowerupType.Defense:
                powerupId = 3;
                break;
            case PowerupType.Speed:
                powerupId = 4;
                break;

        }

        GameManager.itemConsumable[powerupId] += ammount;
        GameManager.scriptInventory.UpdateItems();

        base.Pickup();
    }
}
