using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_AutoDestroy : BuffBase
{
    public override void RemoveBuff()
    {
        base.RemoveBuff();
        GetComponentInParent<CreatureAtributes>().Death();
    }

}
