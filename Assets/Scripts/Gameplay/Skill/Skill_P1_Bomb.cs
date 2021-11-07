using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_P1_Bomb : SkillBase
{
    [Header("Bomb")]
    public GameObject effect;

    [Header("Upgrades")]
    public UpgradeBase upgradeSpeedDebuff;
    public UpgradeBase upgradeProtectionArea;
    public GameObject slowEffect;
    public GameObject protectionEffect;
    public float protectionAreaDuration;

    public override void Activate()
    {
        base.Activate();

        //GameManager.scriptPlayer.GetComponent<Animator>().SetTrigger("skill1");
        GameObject spawnedEffect = Instantiate(effect);
        spawnedEffect.transform.position = GameManager.scriptPlayer.transform.position;

        if (GameManager.upgradesPurchased.Contains(upgradeSpeedDebuff))
        {
            GameObject effect2 = Instantiate(slowEffect);
            effect2.transform.position = GameManager.scriptPlayer.transform.position;
        }

        if (GameManager.upgradesPurchased.Contains(upgradeProtectionArea))
        {
            GameObject effect3 = Instantiate(protectionEffect);
            effect3.transform.position = GameManager.scriptPlayer.transform.position;

            ParticleSystem particle = effect3.GetComponentInChildren<ParticleSystem>();
            float time = particle.main.duration;

            StartCoroutine(StopEmission(effect3.GetComponentsInChildren<ParticleSystem>(), protectionAreaDuration));

            Destroy(effect3, protectionAreaDuration + time);
        }

    }

    private IEnumerator StopEmission(ParticleSystem[] particles, float timer)
    {
        yield return new WaitForSeconds(timer);

        foreach (var item in particles)
        {
            var emission = item.emission;
            emission.enabled = false;

        }

    }
}
