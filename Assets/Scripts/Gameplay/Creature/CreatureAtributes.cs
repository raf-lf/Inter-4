using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum faction
{
    Player, Virus, Neutral
}

public class CreatureAtributes : MonoBehaviour
{
    [Header("Combat")]
    public int hp;
    public int hpMax;

    public float damageModifier = 1;
    public float damageReceivedModifier = 1;

    public int iFrames;
    private int iFramesCurrent;

    [Header("Movement")]
    public float moveSpeed;
    public float moveSpeedModifier = 1;

    [Header("States")]
    public bool dead;

    [Header("Factions")]
    public faction creatureFaction;

    [Header("Components")]
    public Animator animator;
    private FeedbackVfx feedbackScript;

    private void Start()
    {
        feedbackScript = GetComponentInChildren<FeedbackVfx>();

    }

    public void Damage(int damage)
    {
        if (iFramesCurrent <= 0)
        {
            iFramesCurrent = iFrames;

            float adjustedDamage = damage * damageReceivedModifier;

            HealthChange((int)-adjustedDamage);

            //Debug.Log((int)adjustedDamage);
        }
    }

    public void HealthChange(int value)
    {
        if (value != 0)
        {
            hp = Mathf.Clamp(hp + value, 0, hpMax);

            if (GetComponentInChildren<HudCreature>()) GetComponentInChildren<HudCreature>().UpdateValues();

            //Play feedback vfx animation 0 (Damage)
            if (value < 0 && feedbackScript != null)
                feedbackScript.Play(0);

            if (hp == 0) Death();
        }
    }

    public void Death()
    {
        dead = true;
        Invoke(nameof(DisableSelf), 5);
        GetComponent<Animator>().Play("death");
    }

    private void DisableSelf()
    {
        gameObject.SetActive(false);

    }

    private void FixedUpdate()
    {
        if (iFramesCurrent > 0) iFramesCurrent--;
    }
}
