using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_Toxic : CreatureBehavior
{
    public float fleeRange = 3;

    [Header("Toxic Effect")]
    public GameObject toxinEffect;
    public float toxinInterval;
    private float toxinIntervalTime;
    public ObjectPool pool;

    protected override void Start()
    {
        base.Start();
        pool = GameManager.scriptPool.RequestPool(toxinEffect, 10);
    }

    protected override void Act()
    {
        SpawnToxin();

        if (currentTarget != null)
        {
            anim.SetBool("chase", true);

            if (Vector2.Distance(transform.position, currentTarget.transform.position) < fleeRange)
                GetComponentInParent<CreatureMovement>().MoveAway(currentTarget.transform.position);
        
        }
        else
            anim.SetBool("chase", false);

    }
    public void SpawnToxin()
    {
        if (Time.time > toxinIntervalTime)
        {
            anim.SetTrigger("attack");
            toxinIntervalTime = Time.time + toxinInterval;

            GameObject effect = pool.GetFromPool();
            effect.transform.position = transform.position;
        }

    }

}
