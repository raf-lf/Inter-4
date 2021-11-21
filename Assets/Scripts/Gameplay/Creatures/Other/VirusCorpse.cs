using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusCorpse : MonoBehaviour
{
    public int antigen;
    public ParticleSystem vfxCollect;

    public void Collect()
    {
        GetComponent<Animator>().SetBool("collect", true);
        GetComponent<Rigidbody2D>().isKinematic = true;
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;

        GameManager.scriptArena.AntigenChange(antigen);

        float particleDuration = 0;

        if (vfxCollect != null)
        { 
            vfxCollect.Play();
            particleDuration = vfxCollect.main.duration;
        }

        Destroy(this.gameObject, particleDuration);
    }
}
