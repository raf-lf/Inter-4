using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BuffManager : MonoBehaviour
{
    [HideInInspector]
    public List<int> ignoredBuffs = new List<int>();

    public virtual void BuffApply(BuffBase buff)
    {
        if (!ignoredBuffs.Contains(buff.buffId))
        {
            if (GetExistingBuff(buff.buffId))
                GetExistingBuff(buff.buffId).ApplyBuff();
            else
                Instantiate(buff, gameObject.transform);
        }

    }
    public virtual void BuffRemove(BuffBase buff)
    {

        if (GetExistingBuff(buff.buffId)) 
            GetExistingBuff(buff.buffId).RemoveBuff();

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
