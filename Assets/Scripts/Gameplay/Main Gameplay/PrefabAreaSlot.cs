using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabAreaSlot : MonoBehaviour
{
    public GameObject areaToSpawn;
    public GameObject anomaly;

    private void OnEnable()
    {
        PrefabAreaGeneration.PrefabGenerate += SpawnPrefabArea;
    }

    private void OnDisable()
    {
        PrefabAreaGeneration.PrefabGenerate -= SpawnPrefabArea;

    }

    public void SpawnPrefabArea()
    {
        if (areaToSpawn != null) 
            Instantiate(areaToSpawn, transform);
        else
            Instantiate(anomaly, transform);
    }
}
