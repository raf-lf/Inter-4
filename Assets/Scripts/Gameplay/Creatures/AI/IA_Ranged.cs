using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_Ranged : CreatureBehavior
{
    [Header("Attack")]
    public GameObject projectile;
    private ObjectPool attackPool;
    public float fleeRange = 3;

    protected override void Start()
    {
        base.Start();
        attackPool = GameManager.scriptPool.RequestPool(projectile, 30);
    }

    protected override void Act()
    {
        if(currentTarget != null)
        {
            anim.SetBool("chase", true);

            if (Vector2.Distance(transform.position, currentTarget.transform.position) < fleeRange)
                GetComponentInParent<CreatureMovement>().MoveAway(currentTarget.transform.position);
            else
            {
                Attack();
            }
        
        }
        else
            anim.SetBool("chase", false);

    }

    public override void Attack()
    {
        base.Attack();

        GameObject attackEffect = attackPool.GetFromPool();
        attackEffect.transform.position = transform.position;
        attackEffect.GetComponent<Projectile>().Shoot(transform.position, currentTarget.transform.position);

    }

}
