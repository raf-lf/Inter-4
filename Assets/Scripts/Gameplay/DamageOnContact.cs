using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnContact : MonoBehaviour
{
    public int damage;

    public List<faction> factionsAffected = new List<faction>();

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponentInChildren<CreatureAtributes>())
        {
            CreatureAtributes creature = collision.gameObject.GetComponentInChildren<CreatureAtributes>();
            
            if(factionsAffected.Contains(creature.creatureFaction))
                creature.Damage(damage);
        }

    }


}
