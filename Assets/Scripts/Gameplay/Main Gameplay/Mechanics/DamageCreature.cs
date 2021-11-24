using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCreature : MonoBehaviour
{
    //Determinges what trigger type is used for effect; Trigger or Collision.
    public enum DamageTrigger { OnArea, OnImpact}
    public DamageTrigger damageTrigger;

    public int damage;
    private int modifiedDamage;

    public List<Faction> factionsAffected = new List<Faction>();

    public PlaySfx sfxDamage;

    protected virtual void OnTriggerStay2D(Collider2D collision)
    {
        if (damageTrigger == DamageTrigger.OnArea)
            DamageTriggered(collision.gameObject);

    }

    protected virtual void OnCollisionStay2D (Collision2D collision)
    {
        if (damageTrigger == DamageTrigger.OnImpact)
            DamageTriggered(collision.gameObject);

    }

    public void ApplyDamageModifiers()
    {
        modifiedDamage = damage;

        if (GetComponentInParent<CreatureAtributes>())
        {
            modifiedDamage = (int)(modifiedDamage * GetComponentInParent<CreatureAtributes>().damageModifier);
        }
    }

    public void DamageTriggered(GameObject obj)
    {
        //Is colliding object a creature?
        if (obj.GetComponentInChildren<CreatureAtributes>())
        {
            CreatureAtributes creature = obj.GetComponentInChildren<CreatureAtributes>();

            //Is colliding creature's faction affectable?
            if (factionsAffected.Contains(creature.creatureFaction))
            {
                //First, modify damage based on attacker's current damage modifier
                ApplyDamageModifiers();

                if (sfxDamage != null && creature.iFramesCurrent == 0)
                    sfxDamage.PlayInspectorSfx();

                creature.Damage(modifiedDamage);
                DamageInflicted();

            }
        }

    }

    //This is here for projectiles!
    protected virtual void DamageInflicted()
    {

    }

}
