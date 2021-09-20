using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaApplyBuff : MonoBehaviour
{
    public BuffBase buff;
    public bool RemoveOnExit;
    public List<Faction> factionsAffected = new List<Faction>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AreaEffect(collision.gameObject, true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(RemoveOnExit)
            AreaEffect(collision.gameObject, false);

    }

    public void AreaEffect(GameObject obj, bool on)
    {
        if (obj.GetComponentInChildren<CreatureAtributes>())
        {
            CreatureAtributes creature = obj.GetComponentInChildren<CreatureAtributes>();

            if (factionsAffected.Contains(creature.creatureFaction))
            {
                BuffManager scriptBuff = creature.GetComponentInChildren<BuffManager>();

                if (scriptBuff != null)
                {
                    if (on)
                    {
                        scriptBuff.BuffApply(buff);

                    }
                    else
                    {
                        scriptBuff.BuffRemove(buff);

                    }
                }
                else
                    Debug.LogError("Creature has no buff manager!");

            }
        }

    }


}
