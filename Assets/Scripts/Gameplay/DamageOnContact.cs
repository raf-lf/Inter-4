using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnContact : MonoBehaviour
{
    public int damage;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponentInChildren<CreatureAtributes>())
            collision.gameObject.GetComponentInChildren<CreatureAtributes>().Damage(damage);

    }


}
