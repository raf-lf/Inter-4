using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Scripting;

public enum Faction
{
    Player, Virus, Neutral, Corpse
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

    public float _moveSpeedModifier = 1;

    public float moveSpeedModifier
    {
        get
        {
            return _moveSpeedModifier;
        }
        set
        {
            _moveSpeedModifier = value;
            GetComponent<CreatureMovement>().UpdateMoveSpeed();
        }
    }


    public float MoveSpeed;

    [Header("States")]
    public bool dead;

    [Header("Factions")]
    public Faction creatureFaction;

    [Header("Components")]
    public Animator animator;
    protected EffectManager feedbackScript;
    public int antigenValue;
    public GameObject player1DeadVirus;


    private void Start()
    {
        feedbackScript = GetComponentInChildren<EffectManager>();

    }

    public void Damage(int damage)
    {
        if (iFramesCurrent <= 0 && !dead)
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
            if (feedbackScript != null)
            {
                if (value < 0)
                    feedbackScript.PlayFeedback(0);
                else if (value > 0)
                    feedbackScript.PlayFeedback(1);
            }

            if (hp == 0) Death();
        }
    }

    public void Death()
    {
        dead = true;
        Invoke(nameof(DisableSelf), 5);
        GetComponent<Animator>().Play("death");

        if (GameManager.currentPlayer == 0)
        {
            if(player1DeadVirus != null)
            {
                GameObject deadVirus = Instantiate(player1DeadVirus,transform.position, Quaternion.identity);
                deadVirus.transform.parent = null;
                deadVirus.GetComponentInChildren<VirusCorpse>().antigen = antigenValue;
            }
        }

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
