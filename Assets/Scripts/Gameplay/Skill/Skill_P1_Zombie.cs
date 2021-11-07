using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_P1_Zombie : SkillBase
{
    [Header("Zombie Virus")]
    public GameObject zombiePrefab;
    public float useRange;
    public int zombieNumber;
    public BuffBase timedLifeBuff;
    private List<VirusCorpse> availableCorpses = new List<VirusCorpse>();
    public GameObject vfxSpawn;
    //private ObjectPool zombiePool;

    [Header("Upgrades")]
    public UpgradeBase upgradeMoreZombies;
    public UpgradeBase upgradeZombieDetonation;
    public int extraZombies;
    public GameObject detonationEffect;

    protected override void Start()
    {
        base.Start();
        // zombiePool = GameManager.scriptPool.RequestPool(zombiePrefab, 5);

        if (GameManager.upgradesPurchased.Contains(upgradeMoreZombies))
            zombieNumber += extraZombies;


    }

    public override void SkillUse()
    {
        if (GameManager.scriptPlayer.SpendEnergy(energyCost))
        {
            List<Collider2D> collidersNearby = new List<Collider2D>();
            collidersNearby.AddRange(Physics2D.OverlapCircleAll(GameManager.scriptPlayer.gameObject.transform.position, useRange));

            availableCorpses.Clear();

            foreach (var item in collidersNearby)
            {
                if (item.gameObject.GetComponent<VirusCorpse>())
                {
                    availableCorpses.Add(item.gameObject.GetComponent<VirusCorpse>());
                }

            }

            if(availableCorpses.Count >0)
            {
                Activate();
            }
        }
    }

    public override void Activate()
    {
        base.Activate();

        for (int i = zombieNumber; i > 0; i--)
        {
            if (availableCorpses.Count > 0)
            {
                int roll = Random.Range(0, availableCorpses.Count -1);

                GameObject corpse = availableCorpses[roll].gameObject;

                availableCorpses.Remove(availableCorpses[roll]);

                // GameObject zombie = zombiePool.GetFromPool();

                GameObject zombie = Instantiate(zombiePrefab);

                zombie.transform.position = corpse.transform.position;

                Instantiate(vfxSpawn, zombie.transform);

                BuffManager scriptBuff = zombie.GetComponentInChildren<BuffManager>();
                
                scriptBuff.BuffApply(timedLifeBuff);

                if (GameManager.upgradesPurchased.Contains(upgradeZombieDetonation))
                {
                    SpawnOnDeath deathScript = zombie.AddComponent<SpawnOnDeath>();
                    deathScript.objectToSpawn = detonationEffect;

                }

                Destroy(corpse);

            }
            else
            {
                Debug.Log("No corpses available");
                break;
            }
        }

    }

}
