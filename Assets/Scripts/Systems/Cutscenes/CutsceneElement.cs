using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CutsceneAnimations {idle, bob, breathe}

public class CutsceneElement : MonoBehaviour
{
    public CutsceneAnimations animationToPlay;
    public float animationSpeed = 1;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void Animate()
    {
        animator.speed = animationSpeed;
        animator.Play(animationToPlay.ToString());

    }
}
