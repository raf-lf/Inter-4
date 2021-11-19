using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wormhole : MonoBehaviour
{
    public Transform destination;
    public List<Faction> factionsAffected = new List<Faction>();
    public GameObject vfxDestination;
    public AudioClip sfxTeleport;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        AreaEffect(collision.gameObject, true);
    }

    public void AreaEffect(GameObject obj, bool on)
    {
        if (obj.GetComponentInChildren<CreatureAtributes>())
        {
            CreatureAtributes creature = obj.GetComponentInChildren<CreatureAtributes>();

            if (factionsAffected.Contains(creature.creatureFaction))
            {
                creature.transform.position = destination.position;
                vfxDestination.transform.position = destination.position;
                vfxDestination.GetComponent<ParticleSystem>().Stop();
                vfxDestination.GetComponent<ParticleSystem>().Play();
                GameManager.scriptAudio.PlaySfx(sfxTeleport, 1, new Vector2(.8f, 1.2f), GameManager.scriptAudio.sfxSource);

            }
        }

    }


}
