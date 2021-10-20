using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_P1_Weapon : SkillBase
{
    public override void Activate()
    {
        base.Activate();
        GameManager.scriptPlayer.GetComponentInChildren<PlayerActions_Player1>().SwitchWeapons();
    }

}
