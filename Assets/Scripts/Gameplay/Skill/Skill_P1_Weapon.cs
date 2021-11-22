using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_P1_Weapon : SkillBase
{
    [Header("Switch")]
    public AudioClip sfxSwitchInativator;
    public AudioClip sfxSwitchSweeper;

    public override void Activate()
    {
        base.Activate();

        GameManager.scriptPlayer.GetComponentInChildren<PlayerActions_Player1>().SwitchWeapons();

        AudioClip clip;

        if (GameManager.scriptPlayer.GetComponentInChildren<PlayerActions_Player1>().currentWeapon == 0)
            clip = sfxSwitchInativator;
        else
            clip = sfxSwitchSweeper;

            GameManager.scriptAudio.PlaySfx(clip, 1, new Vector2(.8f, 1.2f), GameManager.scriptAudio.sfxSource);
    }

}
