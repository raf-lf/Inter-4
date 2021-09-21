using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [Header("Combat Feedback")]
    public ParticleSystem[] feedbackVfx = new ParticleSystem[0];
    public string[] animationName = new string[0];
    private Animator anim;

    [Header("Buff VFX")]
    public GameObject[] buffVfx = new GameObject[0];

    private void Start()
    {
        anim = GetComponentInParent<CreatureAtributes>().animator;
    }
    public void PlayFeedback(int type)
    {
        if (!feedbackVfx[type].gameObject.activeInHierarchy) feedbackVfx[type].gameObject.SetActive(true);
        feedbackVfx[type].Stop();
        feedbackVfx[type].Play();

        if (anim != null && animationName[type] != null) anim.Play(animationName[type]);

    }

    public void VfxBuff(int buffId, bool on)
    {
        if (!buffVfx[buffId].gameObject.activeInHierarchy) buffVfx[buffId].gameObject.SetActive(true);

        ParticleSystem[] particles = buffVfx[buffId].GetComponentsInChildren<ParticleSystem>();
        
        if (on)
        {
            foreach (ParticleSystem particle in particles)
            {
                var emission = particle.emission;
                emission.enabled = true;

            }

            buffVfx[buffId].GetComponent<ParticleSystem>().Play();
        }
        else
        {
            foreach (ParticleSystem particle in particles)
            {
                var emission = particle.emission;
                emission.enabled = false;

            }
        }

    }
}
