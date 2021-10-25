using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : DamageCreature
{
    [Header("Projectile")]
    public float speed;
    private Vector2 direction;                                                                                                                                                                              

    public float maxLifetime = 3;
    private float autoDestroyTimer;


    public void Shoot(Vector2 origin, Vector2 target)
    {
        autoDestroyTimer = Time.time + maxLifetime;

        direction = Calculations.GetDirectionToTarget(origin, target);
        transform.rotation = Quaternion.Euler(0,0, Calculations.GetRotationZToTarget(origin, target));

        GetComponent<Rigidbody2D>().velocity = speed * direction;


    }

    protected override void DamageInflicted()
    {
        base.DamageInflicted();
        Death();
    }

    public void Death()
    {
        GetComponentInParent<ObjectPool>().ReturnToPool(gameObject);
    }

    private void Update()
    {
        if (Time.time >= autoDestroyTimer) 
            Death();
    }
}
