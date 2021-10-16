using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnDeath : MonoBehaviour
{
    public GameObject objectToSpawn;
    private CreatureAtributes creature;

    private void Start()
    {
        creature = GetComponent<CreatureAtributes>();
        creature.OnDeath += Activate;
    }

    public void Activate()
    {
        creature.OnDeath -= Activate;

        GameObject spawnedObject = Instantiate(objectToSpawn, transform);
        spawnedObject.transform.parent = transform.parent;
    }
}
