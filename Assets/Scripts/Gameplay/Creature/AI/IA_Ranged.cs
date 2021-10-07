using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_Ranged : CreatureBehavior
{
    [Header("Attack")]
    public GameObject projectile;
    public float fleeRange = 3;

    protected override void Act()
    {
        if(currentTarget != null)
        {
            if (Vector2.Distance(transform.position, currentTarget.transform.position) < fleeRange)
                GetComponentInParent<CreatureMovement>().MoveTowards(currentTarget.transform.position);
            else
            {
                Attack();
            }
        
        }

    }

    public override void Attack()
    {
        base.Attack();

        GameObject attackEffect = Instantiate(projectile);
        attackEffect.transform.position = transform.position;
        attackEffect.GetComponent<Projectile>().Shoot(transform.position, currentTarget.transform.position);

    }

}