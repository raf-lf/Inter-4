using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_P1_Bomb : SkillBase
{
    [Header("BPA Bomb")]
    public GameObject effect;

    public override void Activate()
    {
        base.Activate();

        //GameManager.scriptPlayer.GetComponent<Animator>().SetTrigger("skill1");
        GameObject spawnedEffect = Instantiate(effect);
        spawnedEffect.transform.position = GameManager.scriptPlayer.transform.position;
    }
}
