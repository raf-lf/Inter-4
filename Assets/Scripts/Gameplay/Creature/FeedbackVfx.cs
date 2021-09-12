using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackVfx : MonoBehaviour
{
    public ParticleSystem[] feedbackVfx = new ParticleSystem[1];
    public string[] animationName = new string[0];
    private Animator anim;

    private void Start()
    {
        anim = GetComponentInParent<CreatureAtributes>().animator;
    }
    public void Play(int type)
    {
        feedbackVfx[type].Stop();
        feedbackVfx[type].Play();

        if (anim != null) anim.Play(animationName[type]);

    }
}
