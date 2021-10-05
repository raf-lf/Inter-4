using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabAreaSlot : MonoBehaviour
{
    public GameObject areaToSpawn = null;

    private void OnEnable()
    {
        PrefabAreaGeneration.PrefabAreasSpawn += SpawnPrefabArea;
    }

    private void OnDisable()
    {
        PrefabAreaGeneration.PrefabAreasSpawn -= SpawnPrefabArea;

    }

    public void SpawnPrefabArea()
    {
        Instantiate(areaToSpawn, transform);
    }
}
