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
    public Transform buffParent;
    public Dictionary<int, GameObject> buffVfxDictionary = new Dictionary<int, GameObject>();
    //public GameObject[] buffVfx = new GameObject[0];

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

    public void AddBuff(int buffId, GameObject buffVfxObj)
    {
        if (buffVfxDictionary.ContainsKey(buffId))
        {
            //buffVfxDictionary[buffId].SetActive(true);

            ToggleBuffParticles(buffId, true);


        }
        else
        {
            GameObject newBuff = null;
            newBuff = Instantiate(buffVfxObj, buffParent);

            buffVfxDictionary.Add(buffId, newBuff);
        }

    }

    public void RemoveBuff(int buffId)
    {
        if (buffVfxDictionary.ContainsKey(buffId))
        {
            //buffVfxDictionary[buffId].SetActive(false);
            ToggleBuffParticles(buffId, false);
        }

    }

    private void ToggleBuffParticles(int buffId, bool on)
    {
        ParticleSystem[] particles = buffVfxDictionary[buffId].GetComponentsInChildren<ParticleSystem>();

        if (on)
        {
            foreach (ParticleSystem particle in particles)
            {
                var emission = particle.emission;
                emission.enabled = true;

            }

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
