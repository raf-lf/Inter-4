using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_AutoRevive : BuffBase
{
    public float hpRestoredPercent;
    public GameObject vfxRevival;

    public override void RemoveBuff()
    {
        base.RemoveBuff();
    }

    public void AutoRevive()
    {
        CreatureAtributes atributes = manager.GetComponentInParent<CreatureAtributes>();

        atributes.HealthChange((int)(atributes.hpMax * hpRestoredPercent));

        if(vfxRevival != null)
            Instantiate(vfxRevival, atributes.gameObject.transform);

        RemoveBuff();
    }
}
