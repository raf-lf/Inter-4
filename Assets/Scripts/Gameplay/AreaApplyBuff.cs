using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaApplyBuff : MonoBehaviour
{
    public BuffBase buff;
    public bool RemoveOnExit;
    public List<Faction> factionsAffected = new List<Faction>();
    public int frameApplyIntervals = 15;

    private void OnTriggerStay2D(Collider2D collision)
    {
        //It the remaining value from a division of total frames passed in the game by the interval is zero, tries to apply the effect
        if (Time.frameCount % frameApplyIntervals == 0)
        {
            AreaEffect(collision.gameObject);
        }

    }

    public void AreaEffect(GameObject obj)
    {
        if (obj.GetComponentInChildren<CreatureAtributes>())
        {
            CreatureAtributes creature = obj.GetComponentInChildren<CreatureAtributes>();

            if (factionsAffected.Contains(creature.creatureFaction))
            {
                BuffManager scriptBuff = creature.GetComponentInChildren<BuffManager>();

                if (scriptBuff != null)
                {
                        scriptBuff.BuffApply(buff);
                }
                else
                    Debug.LogError("Creature has no buff manager!");

            }
        }

    }


}
