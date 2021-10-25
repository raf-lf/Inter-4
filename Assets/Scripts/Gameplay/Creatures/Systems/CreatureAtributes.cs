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

    [SerializeField] private float baseDmgMod = 1;
    [SerializeField] private float boostDmgMod;
    public float damageModifier
    {
        get
        {
            return baseDmgMod;
        }
        set
        {
            baseDmgMod = value;
        }
    }

    [SerializeField] private float baseDmgRvdMod = 1;
    [SerializeField] private float boostDmgRvdMod;
    public float damageReceivedModifier
    {
        get
        {
            return baseDmgRvdMod;
        }
        set
        {
            baseDmgRvdMod = value;
        }
    }

    [SerializeField] private float baseSpdMod = 1;
    [SerializeField] private float boostSpdMod;
    public float moveSpeedModifier
    {
        get
        {
            return baseSpdMod;
        }
        set
        {
            baseSpdMod = value;
            GetComponent<CreatureMovement>().UpdateMoveSpeed();
        }
    }

    public float moveSpeed;
    public int iFrames;
    private int iFramesCurrent;

    public bool difficultyBoostable = true;

    [Header("States")]
    public bool dead;

    [Header("Factions")]
    public Faction creatureFaction;

    [Header("Components")]
    [HideInInspector]
    public Animator anim;
    protected HudCreature hud;
    protected EffectManager feedbackScript;
    public int antigenValue;
    public GameObject player1DeadVirus;

    public delegate void DeathDelegate();
    public DeathDelegate OnDeath;

    private void OnDisable()
    {
        if (difficultyBoostable)
        {
            ArenaManager.BoostDifficulty -= UpdateDifficulty;
        }
    }


    private void UpdateDifficulty()
    {
        boostDmgMod = GameManager.scriptArena.currentBoostDamage;
        boostDmgRvdMod = GameManager.scriptArena.currentBoostDamageReceived;
        boostSpdMod = GameManager.scriptArena.currentBoostSpeed;
    }

    protected virtual void Awake()
    {
        hud = GetComponentInChildren<HudCreature>();
        feedbackScript = GetComponentInChildren<EffectManager>();
        anim = GetComponent<Animator>();

    }

    protected virtual void Start()
    {

        if (difficultyBoostable)
        {
            ArenaManager.BoostDifficulty += UpdateDifficulty;
            UpdateDifficulty();
        }

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
            if (value < 0)
            {
                if (hp + value <= 0)
                {
                    Death();
                }
                else
                {
                    if (anim != null)
                        anim.SetTrigger("damage");
                }

                if (feedbackScript != null)
                    feedbackScript.PlayFeedback(0);
            }
            else if (value > 0)
            {
                if (feedbackScript != null)
                    feedbackScript.PlayFeedback(1);
            }


            hp = Mathf.Clamp(hp + value, 0, hpMax);

            if (hud != null)
                hud.UpdateValues();

        }
    }

    public virtual void Death()
    {
        if (anim != null)
            anim.SetTrigger("death");
        dead = true;
        Invoke(nameof(DisableSelf), 5);

        if (creatureFaction == Faction.Virus)
            ArenaManager.enemiesDefeated++;
/*
        if (GetComponentInChildren<SpawnOnDeath>())
            GetComponentInChildren<SpawnOnDeath>().Activate();
*/

        OnDeath();

        SpawnDeadVirus();


    }

    private void SpawnDeadVirus()
    {
        if (GameManager.CurrentPlayerClass == 0)
        {
            if (player1DeadVirus != null)
            {
                GameObject deadVirus = Instantiate(player1DeadVirus, transform.position, Quaternion.identity);
                deadVirus.transform.parent = transform.parent;
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
