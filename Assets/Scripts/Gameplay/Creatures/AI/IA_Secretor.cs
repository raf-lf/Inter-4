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

    protected override void Start()
    {
        base.Start();

        scriptCreature = GetComponentInParent<CreatureAtributes>();
        scriptMovement = GetComponentInParent<CreatureMovement>();
        secretionVfx = secretionEffect.GetComponentInChildren<ParticleSystem>();

        atributes.OnDeath += SecreteOff;
    }

    protected override void Act()
    {
        base.Act();

        if (currentTarget != null)
        {
            if (Vector2.Distance(transform.position, currentTarget.transform.position) > secretionRange)
            {
                scriptMovement.MoveTowards(currentTarget.transform.position);
                Secrete(false);
            }
            else
                Secrete(true);

        }
        else
            Secrete(false);

    }

    //This needs to be executed in the OnDeath delegate because the check for turning off the effect can't happen when enemy is dying.
    private void SecreteOff()
    {
        Secrete(false);
        atributes.OnDeath -= SecreteOff;

    }

    private void Secrete(bool on)
    {
        if (on && !scriptCreature.dead)
        {
            if (!secreting)
            {
                secretionEffect.GetComponentInChildren<Collider2D>().enabled = true;

                ParticleSystem[] particles = secretionEffect.GetComponentsInChildren<ParticleSystem>();
                foreach(ParticleSystem particle in particles)
                {
                    var emission = particle.emission;
                    emission.enabled = true;
                }
                
            }

            secreting = true;
            secretionEffect.transform.rotation = Quaternion.Lerp(secretionEffect.transform.rotation, Quaternion.Euler(0, 0, Calculations.GetRotationZToTarget(secretionEffect.transform.position, currentTarget.transform.position)), rotationLerp);
        }
        else
        {

            if (secreting)
            {

                secretionEffect.GetComponentInChildren<Collider2D>().enabled = false;

                ParticleSystem[] particles = secretionEffect.GetComponentsInChildren<ParticleSystem>();
                foreach (ParticleSystem particle in particles)
                {
                    var emission = particle.emission;
                    emission.enabled = false;
                }
            }

            secreting = false;
        }
    }

}
