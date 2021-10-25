using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnedArea : DamageCreature
{
    [Header("Spawned Area")]
    public float maxLifetime = 3;
    private float autoDestroyTimer;

    private void Start()
    {
    }
    private void OnEnable()
    {
        autoDestroyTimer = Time.time + maxLifetime;

    }

    public void Destruction()
    {
        GetComponentInParent<ObjectPool>().ReturnToPool(gameObject);
    }

    private void Update()
    {
        if (Time.time >= autoDestroyTimer) 
            Destruction();
    }
}
