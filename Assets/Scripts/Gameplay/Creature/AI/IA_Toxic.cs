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

    protected override void Act()
    {
        SpawnToxin();

        if (currentTarget != null)
        {
            if (Vector2.Distance(transform.position, currentTarget.transform.position) < fleeRange)
                GetComponentInParent<CreatureMovement>().MoveTowards(currentTarget.transform.position);
        
        }

    }

    public void SpawnToxin()
    {
        if (Time.time > toxinIntervalTime)
        {
            toxinIntervalTime = Time.time + toxinInterval;

            GameObject effect = Instantiate(toxinEffect);
            effect.transform.position = transform.position;
        }

    }

}
