using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabAreaGeneration : MonoBehaviour
{
    //Delegate and observer. All prefab area slots are subscribed to this.
    public delegate void SpawnDelegate();
    public static SpawnDelegate PrefabGenerate;

    //If this is on, areas will not be removed from the pool when they are defined, allowing possible duplicated areas.
    public bool allowDuplicateAreas;

    [Header("Prefab Tables")]
    //How many areas need to be spawned per game stage?
    public int[] minPrefabSmallSpawn = { 0, 5, 4, 3, 3 };
    public int[] minPrefabMediumSpawn = { 0, 1, 2, 3, 3 };
    public int[] minPrefabLargeSpawn = { 0, 0, 1, 2, 3 };
    //What's the chance of remaining empty slots spawning extra prefab areas past the minimum areas?
    public float[] extraPrefabSpawnChance = { 0, .1f, .2f, .3f, .5f };
    //0 - Fixed prefab slots. Use small areas
    //1 - Small prefab slots. Use small areas
    //2 - Medium prefab slots. Use medium areas and boss areas
    //3 - Large prefab slots. Use large areas
    public GameObject[] slotsContainers = new GameObject[4];


    [Header("Prefab Areas")]
    //The list of all prefab areas
    public List<GameObject> prefabSmall = new List<GameObject>();
    public List<GameObject> prefabMedium = new List<GameObject>();
    public List<GameObject> prefabLarge = new List<GameObject>();
    public List<GameObject> prefabBoss = new List<GameObject>();

    [Header("Placed Prefab Areas")]
    //Copies of prefab areas. Areas are picked from these lists so that they can be removed from the pool to avoid duplicate areas, if needed.
    [SerializeField]
    private List<GameObject> placeablePrefabSmall = new List<GameObject>();
    [SerializeField]
    private List<GameObject> placeablePrefabMedium = new List<GameObject>();
    [SerializeField]
    private List<GameObject> placeablePrefabLarge = new List<GameObject>();
    
    private void Start()
    {
        //A new list needs to be defined, because prefabs will be removed from the pool if duplicates are not to be allowed
        placeablePrefabSmall = prefabSmall;
        placeablePrefabMedium = prefabMedium;
        placeablePrefabLarge = prefabLarge;

        //In end-game, boss areas are not determined by the need of collecting data. Instead, they're added to the pool of medium prefab areas.
        //At this stage, multiple boss areas can be generated at the same time.
        if (GameManager.currentGameStage >= 4)
            placeablePrefabMedium.AddRange(prefabBoss);

        //Defines minimum areas for each slot type. Number of minimum areas depend on current stage.
        for (int i = 0; i < slotsContainers.Length; i++)
                SetupSlots(i);

        //Attempts to generate extra prefab areas in slots that are empty after standart generation. Chances depend on current stage. This is repeated for each slot type.
        for (int slotType = 0; slotType < slotsContainers.Length; slotType++)
        {
            //Locates all slots of the same size
            PrefabAreaSlot[] prefabAreas = slotsContainers[slotType].GetComponentsInChildren<PrefabAreaSlot>();

            //Defines a list that will contain all empty slots
            List<PrefabAreaSlot> emptyPrefabAreas = new List<PrefabAreaSlot>();

            //Clears list, if it was already used (not sure if this is needed)
            emptyPrefabAreas.Clear();

            //Adds all slots that are empty to the list
            foreach (PrefabAreaSlot area in prefabAreas)
            {
                if (area.areaToSpawn == null)
                {
                    emptyPrefabAreas.Add(area);
                }
            }

            //Repeats for every empty slot in the list
            for (int areaCount = emptyPrefabAreas.Count; areaCount > 0 ; areaCount--)
            {
                //Each empty slot has a chance to spawn in an extra area. Chance is defined by game stage.
                float roll = Random.Range(0f, 1f);
                if (roll <= extraPrefabSpawnChance[GameManager.currentGameStage])
                {
                    int generatedSlotType = 0;

                    switch (slotType)
                    {
                        //Small areas for fixed slots
                        case 0:
                            generatedSlotType = 0;
                            break;
                        //Small areas for small slots
                        case 1:
                            generatedSlotType = 0;
                            break;
                        //Medium areas for medium slots
                        case 2:
                            generatedSlotType = 1;
                            break;
                        //Large areas for large slots
                        case 3:
                            generatedSlotType = 2;
                            break;

                    }

                    //Selects an area for each empty slot
                    emptyPrefabAreas[areaCount - 1].areaToSpawn = SelectAreaPrefab(generatedSlotType);

                    //Debug.Log("Rolled " + roll + " / " + extraPrefabSpawnChance[GameManager.currentGameStage] + " for area " + emptyPrefabAreas[areaCount-1].name);
                }

            }
            
        }

        //If there's remaining data to be collected, spawn a single boss area in a random slot of medium size. This can override a slot that already has a defined area!
        if (GameManager.dataStored < GameManager.dataNeededPerStage[GameManager.currentGameStage])
        {

            List<PrefabAreaSlot> slotsFound = new List<PrefabAreaSlot>();
            slotsFound.AddRange(slotsContainers[2].GetComponentsInChildren<PrefabAreaSlot>());

            Debug.Log("Placing boss area.");
            slotsFound[Random.Range(0, slotsFound.Count)].areaToSpawn = SelectAreaPrefab(3);


        }

        //At last, triggers event to generate areas
        Generation();

    }

    //Sets up all slots of a specific type, defined by the parameter
    private void SetupSlots(int slotType)
    {

        //Gets and lists all slots of that type
        List<PrefabAreaSlot> slotsFound = new List<PrefabAreaSlot>();
        slotsFound.AddRange(slotsContainers[slotType].GetComponentsInChildren<PrefabAreaSlot>());

        //This is a mess. But basically, we define what slots are avaiable to be defined and for each slot type, repear a number of times equal to the minimum area number of that size.
        switch (slotType)
        {
            //Fixed Slots
            case 0:
                Debug.Log("Placing " + slotsFound.Count + " fixed areas.");
                for (int i = slotsFound.Count; i > 0; i--)
                {
                    //Picks a random slot between the ones that were found
                    int randomSlot = (int)Random.Range(0, slotsFound.Count);

                    //Defines slot's area from the slot table
                    slotsFound[randomSlot].areaToSpawn = SelectAreaPrefab(0);

                    //Removes slot from the pool of slots before repeating the process
                    slotsFound.Remove(slotsFound[randomSlot]);
                }

                break;

            //Small Slots
            case 1:
                Debug.Log("Placing " + minPrefabSmallSpawn[GameManager.currentGameStage] +"/"+ slotsFound.Count + " small areas.");
                for (int i = minPrefabSmallSpawn[GameManager.currentGameStage]; i > 0; i--)
                {
                    int randomSlot = (int)Random.Range(0, slotsFound.Count);

                    slotsFound[randomSlot].areaToSpawn = SelectAreaPrefab(0);

                    slotsFound.Remove(slotsFound[randomSlot]);
                }

                break;

            //Medium Slots
            case 2:
                Debug.Log("Placing " + minPrefabMediumSpawn[GameManager.currentGameStage] + "/" + slotsFound.Count + " medium areas.");
                for (int i = minPrefabMediumSpawn[GameManager.currentGameStage]; i > 0; i--)
                {

                    int randomSlot = (int)Random.Range(0, slotsFound.Count);

                    slotsFound[randomSlot].areaToSpawn = SelectAreaPrefab(1);

                    slotsFound.Remove(slotsFound[randomSlot]);
                }

                break;

            //Large Slots
            case 3:
                Debug.Log("Placing " + minPrefabLargeSpawn[GameManager.currentGameStage] + "/" + slotsFound.Count + " large areas.");
                for (int i = minPrefabLargeSpawn[GameManager.currentGameStage]; i > 0; i--)
                {

                    int randomSlot = (int)Random.Range(0, slotsFound.Count);

                    slotsFound[randomSlot].areaToSpawn = SelectAreaPrefab(2);

                    slotsFound.Remove(slotsFound[randomSlot]);
                }

                break;
        }


    }


    //This method rturns an area prefab from the area tables based on slot type
    private GameObject SelectAreaPrefab(int slotType)
    {
        int roll = 0;
        GameObject prefabToPlace = null;

        switch (slotType)
        {
            //Small areas
            case 0:
                //Selects random area from area table
                roll = (int)Random.Range(0, placeablePrefabSmall.Count);
                prefabToPlace = placeablePrefabSmall[roll];
                Debug.Log("Prefab area " + placeablePrefabSmall[roll].name + " assigned.");

                //If allow duplicates is disabled. Remove selected area from table. WARNING if there are no more prefabs in the table, nothing will spawn in the area!
                if (!allowDuplicateAreas)
                    placeablePrefabSmall.Remove(placeablePrefabSmall[roll]);

                break;
            //Medium areas
            case 1:
                roll = (int)Random.Range(0, placeablePrefabMedium.Count);
                prefabToPlace = placeablePrefabMedium[roll];
                Debug.Log("Prefab area " + placeablePrefabMedium[roll].name + " assigned.");

                if (!allowDuplicateAreas)
                    placeablePrefabMedium.Remove(placeablePrefabMedium[roll]);

                break;
            //Medium areas
            case 2:
                roll = (int)Random.Range(0, placeablePrefabLarge.Count);
                prefabToPlace = placeablePrefabLarge[roll];
                Debug.Log("Prefab area " + placeablePrefabLarge[roll].name + " assigned.");

                if (!allowDuplicateAreas)
                    placeablePrefabLarge.Remove(placeablePrefabLarge[roll]);

                break;
            //Boss areas
            case 3:
                roll = (int)Random.Range(0, prefabBoss.Count);
                prefabToPlace = prefabBoss[roll];
                Debug.Log("Prefab area " + prefabBoss[roll].name + " assigned.");

                break;

        }

        return prefabToPlace;
    }

    //This is the event that will trigger the generation of all defined prefabs! This is a delegate observer and all prefab slots are subscribed to it.
    private void Generation()
    {
        PrefabGenerate();
    }
}
