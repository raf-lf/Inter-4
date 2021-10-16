using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabManager : MonoBehaviour
{
    [Header("Expedition Results")]
    [SerializeField]
    private float startDelay = 1;

    public static int expeditionScience;
    public static int expeditionAntigen;
    public static int[] expeditionComponents = new int[4];
    public static bool expeditionData;

    [Header("Stats")]
    public static int scienceStored;
    public static int antigenStored;
    public static int antigenStoredMax = 1000;
    public static int[] componentStored = new int[4];
    public static int dataStored;

    private void Awake()
        =>GameManager.scriptLab = this;
       

    void Start()
    {
        Invoke(nameof(LabEnter), startDelay);   
    }

    public void LabEnter()
    {
        StoreEverything();
    }

    private void StoreEverything()
    {
        scienceStored += expeditionScience;
        expeditionScience = 0;

        antigenStored = Mathf.Clamp(antigenStored + expeditionAntigen, 0, antigenStoredMax);
        expeditionAntigen = 0;

        for (int i = 0; i < componentStored.Length; i++)
        {
            componentStored[i] += expeditionComponents[i];
            expeditionComponents[i] = 0;

        }

        if (expeditionData)
        {
            dataStored++;
            expeditionData = false;
        }
    }

      
}
