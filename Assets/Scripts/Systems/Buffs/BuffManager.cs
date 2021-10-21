using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BuffManager : MonoBehaviour
{
    [HideInInspector]
    public List<int> ignoredBuffs = new List<int>();

    private CreatureAtributes creature;

    private void OnEnable()
    {
        creature = GetComponentInParent<CreatureAtributes>();
        //Delegate to remove buffs on creature death
        creature.OnDeath += RemoveAllBuffs;
    }

    private void OnDisable()
    {
        creature.OnDeath -= RemoveAllBuffs;

    }

    public void BuffApply(BuffBase buff)
    {
        if (!ignoredBuffs.Contains(buff.buffId))
        {
            if (GetExistingBuff(buff.buffId))
                GetExistingBuff(buff.buffId).ApplyBuff();
            else
                Instantiate(buff, gameObject.transform);
        }

    }
    public void BuffRemove(BuffBase buff)
    {

        if (GetExistingBuff(buff.buffId)) 
            GetExistingBuff(buff.buffId).RemoveBuff();

    }
    public void RemoveAllBuffs()
    {
        BuffBase[] allBuffs = GetComponentsInChildren<BuffBase>();
        foreach (BuffBase buff in allBuffs)
        {
            buff.RemoveBuff();
        }
    }

    private BuffBase GetExistingBuff(int buffId)
    {
        BuffBase foundBuff = null;

        BuffBase[] buffsExisting = GetComponentsInChildren<BuffBase>(true);

        foreach (BuffBase buff in buffsExisting)
        {
            if (buff.buffId == buffId)
            {
                foundBuff = buff;
                break;
            }
        }
        return foundBuff;

    }


}
