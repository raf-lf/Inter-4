using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectedCell : RandomObjectSpawner
{
    private bool triggered;
    public List<Faction> triggeringFactions = new List<Faction>();

    protected override void Start()
    {
        //This needs to be here so that the spawner isn't triggered!
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!triggered && collision.gameObject.GetComponent<CreatureAtributes>())
        {
            if(triggeringFactions.Contains(collision.gameObject.GetComponent<CreatureAtributes>().creatureFaction))
            {
                TriggerBurst(); 
            }

        }
    }
    public void TriggerBurst()
    {
        GetComponentInChildren<Animator>().SetTrigger("activate");
        triggered = true;

    }

    public override void Activate()
    {
        base.Activate();
    }
}
