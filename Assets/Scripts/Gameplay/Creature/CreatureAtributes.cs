using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureAtributes : MonoBehaviour
{
    [Header("Combat")]
    public int hp;
    public int hpMax;

    public float damageModifier = 1;
    public float damageReceivedModifier = 1;

    [Header("Movement")]
    public float moveSpeed;
    public float moveSpeedModifier = 1;

    [Header("States")]
    public bool dead;

    public void Damage(int damage)
    {
        float adjustedDamage = damage * damageReceivedModifier;

        HealthChange((int)-adjustedDamage);

        Debug.Log((int)adjustedDamage);
    }

    public void HealthChange(int value)
    {
        hp = Mathf.Clamp(hp + value, 0, hpMax);
        if (hp == 0) Death();
    }

    public void Death()
    {
        dead = true;
        Destroy(gameObject);
    }

}
