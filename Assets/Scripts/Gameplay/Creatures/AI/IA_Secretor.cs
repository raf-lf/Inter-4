using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_Secretor : CreatureBehavior
{
    private CreatureMovement scriptMovement;
    private CreatureAtributes scriptCreature;

    [Header("Secretion")]
    public float secretionRange = 3;
    public float rotationLerp = .3f;
    public GameObject secretionEffect;
    private ParticleSystem secretionVfx;
    private bool secreting;

    protected override void Update()
    {
        base.Update();

        if (scriptCreature.dead) 
            Secrete(false);
    }

    protected override void Start()
    {
        base.Start();

        scriptCreature = GetComponentInParent<CreatureAtributes>();
        scriptMovement = GetComponentInParent<CreatureMovement>();
        secretionVfx = secretionEffect.GetComponentInChildren<ParticleSystem>();

        Secrete(false);

    }

    protected override void Act()
    {
        base.Act();

        if (currentTarget != null)
        {
            anim.SetBool("chase", true);

            if (Vector2.Distance(transform.position, currentTarget.transform.position) > secretionRange)
            {
                scriptMovement.MoveTowards(currentTarget.transform.position);
                Secrete(false);
            }
            else
                Secrete(true);

        }
        else
        {
            anim.SetBool("chase", false);
            Secrete(false);
        }

    }

    private void Secrete(bool on)
    {
        if (on)
        {
            secretionEffect.transform.rotation = Quaternion.Lerp(secretionEffect.transform.rotation, Quaternion.Euler(0, 0, Calculations.GetRotationZToTarget(secretionEffect.transform.position, currentTarget.transform.position)), rotationLerp);
            SecreteEffect(true);
        }
        else
        {
            SecreteEffect(false);
        }
    }

    private void SecreteEffect(bool on)
    {
        if (on)
        {
            if (!secreting)
            {
                secreting = true;
                anim.SetBool("attacking", true);

                secretionEffect.GetComponentInChildren<Collider2D>().enabled = true;

                ParticleSystem[] particles = secretionEffect.GetComponentsInChildren<ParticleSystem>();
                foreach (ParticleSystem particle in particles)
                {
                    var emission = particle.emission;
                    emission.enabled = true;
                }

            }
        }
        else
        {
            if (secreting)
            {
                secreting = false;
                anim.SetBool("attacking", false);

                secretionEffect.GetComponentInChildren<Collider2D>().enabled = false;

                ParticleSystem[] particles = secretionEffect.GetComponentsInChildren<ParticleSystem>();
                foreach (ParticleSystem particle in particles)
                {
                    var emission = particle.emission;
                    emission.enabled = false;
                }
            }
        }

    }

}
