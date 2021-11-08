using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBase : MonoBehaviour
{
    //public List<Faction> pickableFaction = new List<Faction>();
    public GameObject pickupVfx;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<CreatureAtributes>())
        {
            CreatureAtributes pickingCreature = collision.gameObject.GetComponent<CreatureAtributes>();
            //if (pickableFaction.Contains(pickingCreature.creatureFaction))
            if (pickingCreature == GameManager.scriptPlayer)
            {
                Pickup();
            }
        }
        
    }

    public virtual void Pickup()
    {
        if (pickupVfx != null)
        {
            GameObject vfx = Instantiate(pickupVfx);
            vfx.transform.position = transform.position;
        }
        GetComponent<Animator>().SetTrigger("pick");

    }
}
