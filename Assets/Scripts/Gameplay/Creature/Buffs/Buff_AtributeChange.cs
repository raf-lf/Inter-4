using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_AtributeChange : BuffBase
{
    [Header("Buff Effect - Atribute Change")]
    public float damage;
    public float damageReceived;
    public float moveSpeed;

    public override void ApplyBuff()
    {
        if (!active) ModifyAtributes(true);
        base.ApplyBuff();
    }

    public override void RemoveBuff()
    {
        if (active) ModifyAtributes(false);
        base.RemoveBuff();
    }

    private void ModifyAtributes(bool on)
    {
            CreatureAtributes creature = GetComponentInParent<CreatureAtributes>();
            float modifier;

            if (on) modifier = 1;
            else modifier = -1;

            if (damage != 0)
                creature.damageModifier += (damage * modifier);
            if (damageReceived != 0)
                creature.damageReceivedModifier += (damageReceived * modifier);
            if (moveSpeed != 0)
            {
                creature.moveSpeedModifier += (moveSpeed * modifier);
             //   creature.GetComponent<CreatureMovement>().UpdateMoveSpeed();
            }

    }

}
