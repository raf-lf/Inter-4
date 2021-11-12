using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTools: MonoBehaviour
{
    public void PlaySfx(AudioClip clip)
    {
        AudioSource source = null;

        if (GetComponentInChildren<AudioSource>() != null)
            source = GetComponentInChildren<AudioSource>();

        GameManager.scriptAudio.PlaySfx(clip, 1, Vector2.one, source);
    }

    public void DestroySelf(float delay)
    {
        Destroy(gameObject, delay);
    }

}
