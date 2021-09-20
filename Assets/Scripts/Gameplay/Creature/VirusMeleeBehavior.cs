using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusMeleeBehavior : CreatureBehavior
{
    private CreatureAtributes creature;

    private void Start()
    {
        creature = GetComponentInParent<CreatureAtributes>();
    }

    private void ApproachPlayer()
    {
        if(currentTarget != null)
        {
            if (Vector2.Distance(transform.position, currentTarget.transform.position) > 1)
                GetComponentInParent<CreatureMovement>().MoveTowards(currentTarget.transform.position);
            else
                GetComponentInParent<Animator>().Play("attack");
        }

    }

    protected override void Update()
    {
        base.Update();

        if (!creature.dead) ApproachPlayer();
    }
}
