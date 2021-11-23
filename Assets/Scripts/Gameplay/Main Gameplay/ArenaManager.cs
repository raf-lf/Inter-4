using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArenaManager : MonoBehaviour
{
    public float timeBeforeReload;

    [Header("Stats")]
    public static int[] consumablesInInventory = new int[5];
    public static int[] componentsInInventory = new int[4];
    public static int antigenCollected;
    public static int antigenCapacity = 250;
    public static bool dataCollected;

    [Header("Counters")]
    public static int expeditionKills;
    public static int expeditionConsumables;
    public static int expeditionComponents;

    [Header("Science Calculations")]
    [SerializeField] private int sciencePerEnemy;
    [SerializeField] private int sciencePerConsumable;
    [SerializeField] private int sciencePerComponent;
    [SerializeField] private float deathPennalty;
    [SerializeField] private float unusedConsumableBonus;
    [SerializeField] private float difficultyBonus;
    [SerializeField] private float extraBonus;

    [Header("Difficulty Boost")]
    [SerializeField] private int timeBoosts;
    [SerializeField] private int extraBoosts;
    [SerializeField] private int maxBoosts;
    [SerializeField] private int[] stageBoosts = new int[5];    
    [SerializeField] private float expeditionTimer;
    [SerializeField] private float gracePeriod;
    [SerializeField] private float maxDifficultyCapTime;
    [SerializeField] private float boostInterval;

    [Header("Boost Effects")]
    [SerializeField] private float boostDamage;
    [SerializeField] private float boostDamageReceived;
    [SerializeField] private float boostSpeed;
    public float currentBoostDamage;
    public float currentBoostDamageReceived;
    public float currentBoostSpeed;

    [Header("Other")]
    public GameObject vfxRecall;

    public delegate void DifficultyBoostDelegate();
    public static event DifficultyBoostDelegate BoostDifficulty;


    private void OnEnable()
        => PlayerAtributes.PlayerDeath += GameOver;
    private void OnDisable()
        => PlayerAtributes.PlayerDeath -= GameOver;
    private void Awake()
        =>GameManager.scriptArena = this;

    private void Start()
    {

        GameManager.SetFrameRate(60);

        StartNewExpedition();

        extraBoosts = stageBoosts[GameManager.currentGameStage];
        timeBoosts = 0;
        expeditionTimer = 0;
    }

    private void FixedUpdate()
    {
        ExpeditionTimer();
 
    }

    public void AntigenChange(int value)
    {
        antigenCollected = Mathf.Clamp(antigenCollected + value, 0, antigenCapacity);
        GameManager.scriptHud.IncreaseFeedbackAntigen();
        GameManager.scriptHud.UpdateHud();
    }

    public void ComponentChange(int id, int value)
    {
        componentsInInventory[id] = Mathf.Clamp(componentsInInventory[id] + value, 0, 100);
        expeditionComponents+=value;
        GameManager.scriptHud.IncreaseFeedbackComponent(id);
        GameManager.scriptHud.UpdateHud();
    }

    public void DataChange(bool yes)
    {
        dataCollected = yes;
        GameManager.scriptHud.IncreaseFeedbackData();
        GameManager.scriptHud.UpdateHud();

    }

    public void ItemChange(int id, int value)
    {
        consumablesInInventory[id] += value;
        expeditionConsumables+= value;
        GameManager.scriptInventory.IncreaseFeedbackItem(id);
        GameManager.scriptInventory.UpdateItems();
    }


    private void StartNewExpedition()
    {
        for (int i = 0; i < componentsInInventory.Length; i++)
        {
            componentsInInventory[i] = 0;
        }
        antigenCollected = 0;
        expeditionKills = 0;
        expeditionComponents = 0;
        expeditionConsumables = 0;
        dataCollected = false;

    }

    public void GameOver()
    {
        EndExpedition(GameManager.scriptPlayer.dead);
    }

    public void EndExpedition(bool playerDied)
    {
        if (playerDied)
            GameManager.scriptAudio.FadeBgm(0, .2f);
        else
        {
            Instantiate(vfxRecall, GameManager.scriptPlayer.transform);

            foreach (var item in GameManager.scriptPlayer.gameObject.GetComponentsInChildren<SpriteRenderer>())
            {
                item.enabled = false;
            }

            GameManager.scriptAudio.FadeBgm(0, .05f);
        }
        GameManager.scriptAudio.FadeSfx(0, .2f);

        GameManager.scriptCamera.EndExpeditionCamera();
        GameManager.PlayerControl = false;

        LabManager.returningFromExpedition = true;
        CalculateScience(playerDied);
        PrepareComponents(playerDied);
        LabManager.expeditionData = dataCollected;

        Invoke(nameof(LoadScene), timeBeforeReload);
    }
    public void LoadScene()
    {
        GameManager.PlayerControl = true;
        SceneManager.LoadScene("lab", LoadSceneMode.Single);
    }

    public void PrepareComponents (bool playerDied)
    {
        for (int i = 0; i < componentsInInventory.Length; i++)
        {
            int finalComponent = componentsInInventory[i];

            /*
            if (playerDied)
                finalComponent /= 2;
            */

            LabManager.expeditionComponents[i] = finalComponent;

        }

        int finalAntigen = antigenCollected;

        /*if (playerDied)
            finalAntigen /= 2;
        */

        LabManager.expeditionAntigen = finalAntigen;

    }

    public void CalculateScience(bool playerDied)
    {
        int scienceEnemies = expeditionKills * sciencePerEnemy;
        ResultScreen.scienceEnemiesDefeated = scienceEnemies;

        int scienceComponents = expeditionComponents * sciencePerComponent;
        ResultScreen.scienceComponentsCollected = scienceComponents;

        int scienceConsumables = expeditionConsumables * sciencePerConsumable;
        ResultScreen.scienceConsumablesCollected = scienceConsumables;

        float scienceBonusUnused = unusedConsumableBonus * ConvertConsumables();
        ResultScreen.bonusUnusedConsumables = scienceBonusUnused;

        float finalScience = scienceEnemies + scienceComponents + scienceConsumables;
        float finalModifier = 1 + scienceBonusUnused + (difficultyBonus * timeBoosts) + extraBonus;

        finalScience *= finalModifier;

        if (playerDied)
        {
            finalScience *= deathPennalty;
            ResultScreen.pennaltyDeath = deathPennalty;
        }
        else
            ResultScreen.pennaltyDeath = 0;


        ResultScreen.scienceFinal = (int)finalScience;

        LabManager.expeditionScience = (int)finalScience;
    }

    public int ConvertConsumables()
    {
        int consumableQty = 0;

        for (int i = 0; i < consumablesInInventory.Length; i++)
        {
            consumableQty += consumablesInInventory[i];
            consumablesInInventory[i] = 0;

        }
        ResultScreen.unusedConsumables = consumableQty;
        return consumableQty;
    }

    private void ExpeditionTimer()
    {
        expeditionTimer += Time.deltaTime;

        if (expeditionTimer > gracePeriod && expeditionTimer <= maxDifficultyCapTime)
        {
            if (expeditionTimer > gracePeriod + boostInterval * timeBoosts && expeditionTimer < gracePeriod + boostInterval * timeBoosts + 1)
            {
                if (timeBoosts < maxBoosts)
                {
                    timeBoosts++;
                    currentBoostDamage = boostDamage * timeBoosts + extraBoosts;
                    currentBoostDamageReceived = boostDamageReceived * timeBoosts + extraBoosts;
                    currentBoostSpeed = boostSpeed * timeBoosts + extraBoosts;
                    BoostDifficulty();
                }
            }
        }
    }
}
