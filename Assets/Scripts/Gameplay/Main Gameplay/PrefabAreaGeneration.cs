using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabAreaGeneration : MonoBehaviour
{
    public delegate void SpawnDelegate();
    public static SpawnDelegate PrefabAreasSpawn;

    [Header("Prefab Tables")]
    //How many areas need to be spawned per game stage? What's the chance of remaining empty slots spawning extra prefab areas?
    public int[] minPrefabSmallSpawn = { 0, 5, 4, 3, 3 };
    public int[] minPrefabMediumSpawn = { 0, 1, 2, 3, 3 };
    public int[] minPrefabLargeSpawn = { 0, 0, 1, 2, 3 };
    public float[] extraPrefabSpawnChance = { 0, .1f, .2f, .3f, .5f };
    public GameObject[] slotsContainers = new GameObject[4];


    [Header("Prefab Areas")]
    //The list of all prefab areas
    public List<GameObject> prefabSmall = new List<GameObject>();
    public List<GameObject> prefabMedium = new List<GameObject>();
    public List<GameObject> prefabLarge = new List<GameObject>();
    public List<GameObject> prefabBoss = new List<GameObject>();

    [Header("Placed Prefab Areas")]
    //Areas are picked from this list and are then removed so no repeating areas exist in the same expedition.
    private List<GameObject> placedPrefabSmall = new List<GameObject>();
    private List<GameObject> placedPrefabMedium = new List<GameObject>();
    private List<GameObject> placedPrefabLarge = new List<GameObject>();

    private void Awake()
    {
        
    }
    private void Start()
    {
        placedPrefabSmall = prefabSmall;
        placedPrefabMedium = prefabMedium;
        placedPrefabLarge = prefabLarge;

        for (int i = 0; i < slotsContainers.Length; i++)
        {
            if (i != 0)
                DefineAreaSpawn(i);

        }

    }


    private void DefineAreaSpawn(int slotType)
    {
        PrefabAreaSlot[] slotsFound = slotsContainers[slotType].GetComponentsInChildren<PrefabAreaSlot>();

        switch (slotType)
        {
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;

        }
        for (int i = minPrefabSmallSpawn[GameManager.currentGameStage]; i > 0; i--)
        {
        }

    }

    private void Generation()
    {
        PrefabAreasSpawn();
    }
}
