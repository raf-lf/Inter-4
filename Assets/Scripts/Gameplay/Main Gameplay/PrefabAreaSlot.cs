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
        GameObject spawnedArea;

        if (areaToSpawn != null) 
            spawnedArea = Instantiate(areaToSpawn, transform);
        else
            spawnedArea = Instantiate(anomaly, transform);

       // RotatePrefab(spawnedArea);
    }

    private void RotatePrefab(GameObject spawnedArea)
    {
        float randomRotation = Random.Range(0f, 360f);
        spawnedArea.transform.rotation = Quaternion.Euler(0, 0, randomRotation);

        MarkForRotationFix[] unrotatableObjects = GetComponentsInChildren<MarkForRotationFix>();

        foreach (MarkForRotationFix uO in unrotatableObjects)
        {
            uO.transform.rotation = Quaternion.Euler(0, 0, -randomRotation);
        }



    }
}
