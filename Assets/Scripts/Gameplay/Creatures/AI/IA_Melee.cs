using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_Melee : CreatureBehavior
{
    [Header("Attack")]
    public float attackRange = 2;

    protected override void Act()
    {
        base.Act();

        if (currentTarget != null)
        {
            anim.SetBool("chase", false);

            if (Vector2.Distance(transform.position, currentTarget.transform.position) > attackRange)
                GetComponentInParent<CreatureMovement>().MoveTowards(currentTarget.transform.position);
            else
                Attack();
        }
        else
            anim.SetBool("chase", false);
    }

}
