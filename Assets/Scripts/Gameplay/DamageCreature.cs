using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCreature : MonoBehaviour
{
    public enum DamageTrigger { OnArea, OnImpact}
    public DamageTrigger damageTrigger;

    public int damage;

    public List<Faction> factionsAffected = new List<Faction>();

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (damageTrigger == DamageTrigger.OnArea)
            DamageTriggered(collision.gameObject);

    }

    private void OnCollisionStay2D (Collision2D collision)
    {
        if (damageTrigger == DamageTrigger.OnImpact)
            DamageTriggered(collision.gameObject);

    }

    public void DamageTriggered(GameObject obj)
    {
        if (obj.GetComponentInChildren<CreatureAtributes>())
        {
            CreatureAtributes creature = obj.GetComponentInChildren<CreatureAtributes>();

            if (factionsAffected.Contains(creature.creatureFaction))
                creature.Damage(damage);
        }

    }


}
