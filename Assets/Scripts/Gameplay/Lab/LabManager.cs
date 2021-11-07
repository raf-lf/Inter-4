using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabManager : MonoBehaviour
{
    [Header("Expedition Results")]
    public static bool returningFromExpedition;

    [SerializeField]
    private float startDelay = 1;

    public static int expeditionScience;
    public static int expeditionAntigen;
    public static int[] expeditionComponents = new int[4];
    public static bool expeditionData;

    [Header("Stats")]
    public static int vaccineStored;
    public static int scienceStored;
    public static int antigenStored;
    public static int antigenStoredMax = 1000;
    public static int[] componentStored = new int[4];
    public static int dataStored;

    [Header("Necessary Stats")]
    public static int[] antigenTarget = { 100, 150, 200, 250, 250 };
    public static int[] componentcurrentTarget = new int[4];
    public static int[] componentTargetBase = { 2, 3, 5, 8, 8 };
    public static int[] componentTargetVariance = { 1, 1, 2, 3, 3 };
    public static int[] componentdillutant = { 10, 15, 20, 25, 25 };
    public static int[] vaccineTarget = { 100, 150, 400, 500, 500 };
    public static int[] dataNeededPerStage = { 0, 1, 2, 3, 0 };


    [Header("Other")]
    public static int vaccineInRocket;
    public GameObject clickBlocker;

    private void Awake()
        =>GameManager.scriptLab = this;
       

    void Start()
    {
        Invoke(nameof(LabEnter), startDelay);   


    }

    public void ToggleClickBlocker(bool on)
    {
        clickBlocker.SetActive(on); 

    }

    public void SetComponentTargets()
    {
        if (componentcurrentTarget[0] == 0)
        {
            for (int i = 0; i < componentcurrentTarget.Length; i++)
            {
                componentcurrentTarget[i] = componentTargetBase[GameManager.currentGameStage] + Random.Range(-componentTargetVariance[GameManager.currentGameStage], componentTargetVariance[GameManager.currentGameStage]);

            }
        }
    }

    public void LabEnter()
    {
        if (returningFromExpedition)
        {
            GetComponentInChildren<ResultScreen>().SetupResults();
            returningFromExpedition = false;
        }
    }

    public void StoreEverything()
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
