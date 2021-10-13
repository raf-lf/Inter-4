using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_Protector : CreatureBehavior
{
    [Header("Protect")]
    public float protectRange = 5;

    protected override void Start()
    {
        base.Start();

        //This is here so that the enemy can't protect itself using it's buff

        BuffManager buffManager = gameObject.transform.parent.GetComponentInChildren<BuffManager>();
        BuffBase protectionBuff = transform.parent.GetComponentInChildren<AreaApplyBuff>().buff;

        buffManager.ignoredBuffs.Add(protectionBuff.buffId);
    }

    protected override void Act()
    {
        base.Act();

        if (currentTarget != null)
        {
            if (Vector2.Distance(transform.position, currentTarget.transform.position) > protectRange)
                GetComponentInParent<CreatureMovement>().MoveTowards(currentTarget.transform.position);
            else
                Protect();

        }

    }

    private void Protect()
    {
        busyTime += attackCooldown;
        GetComponentInParent<Animator>().Play("protect");
    }

}
