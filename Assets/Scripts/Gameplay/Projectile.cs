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

    private void Start()
    {
        autoDestroyTimer = Time.time + maxLifetime;
    }


    public void Shoot(Vector2 origin, Vector2 target)
    {
        direction = Calculations.GetDirectionToTarget(origin, target);
        transform.rotation = Quaternion.Euler(0,0, Calculations.GetRotationZToTarget(origin, target));

        GetComponent<Rigidbody2D>().velocity = speed * direction;


    }

    protected override void DamageInflicted()
    {
        base.DamageInflicted();
        Destruction();
    }

    public void Destruction()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        if (Time.time >= autoDestroyTimer) 
            Destruction();
    }
}
