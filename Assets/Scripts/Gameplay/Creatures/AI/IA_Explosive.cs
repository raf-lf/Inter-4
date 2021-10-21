using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_Explosive : CreatureBehavior
{
    [Header("Explosion")]
    public float explosionDetectionRange = 2;

    protected override void Act()
    {
        base.Act();

        if (currentTarget != null)
        {
            anim.SetBool("chase", true);

            if (Vector2.Distance(transform.position, currentTarget.transform.position) > explosionDetectionRange)
                GetComponentInParent<CreatureMovement>().MoveTowards(currentTarget.transform.position);
            else
                Explode();

        }
        else
            anim.SetBool("chase", false);

    }

    public void Explode()
    {
        busyTime += 1000;
        anim.SetTrigger("attack");
        GetComponentInParent<CreatureAtributes>().dead = true;
        GetComponentInParent<CreatureAtributes>().OnDeath();
        GetComponentInParent<CreatureAtributes>().Invoke("DisableSelf", 5);
    }

}
