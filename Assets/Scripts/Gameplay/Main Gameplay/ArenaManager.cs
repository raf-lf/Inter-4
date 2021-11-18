using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArenaManager : MonoBehaviour
{
    public float timeBeforeReload;

    [Header("Stats")]
    public static int[] componentsInInventory = new int[4];
    public static int antigenCollected;
    public static int antigenCapacity = 250;
    public static int enemiesDefeated;
    public static int componentsCollected;
    public static int consumablesCollected;
    public static bool dataCollected;

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
    [SerializeField] private int[] stageBoosts = new int[4];    
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
        GameManager.scriptHud.UpdateHud();
    }

    public void ComponentChange(int id, int value)
    {
        componentsInInventory[id] = Mathf.Clamp(componentsInInventory[id] + value, 0, 100);
        ArenaManager.componentsCollected++;
        GameManager.scriptHud.UpdateHud();
    }

    private void StartNewExpedition()
    {
        for (int i = 0; i < componentsInInventory.Length; i++)
        {
            componentsInInventory[i] = 0;
        }
        antigenCollected = 0;
        enemiesDefeated = 0;
        componentsCollected = 0;
        consumablesCollected = 0;
        dataCollected = false;

    }

    public void GameOver()
    {
        EndExpedition(GameManager.scriptPlayer.dead);
    }

    public void EndExpedition(bool playerDied)
    {
        LabManager.returningFromExpedition = true;
        CalculateScience(playerDied);
        PrepareComponents(playerDied);
        LabManager.expeditionData = dataCollected;

        Invoke(nameof(LoadScene), timeBeforeReload);
    }
    public void LoadScene()
    {
        SceneManager.LoadScene("lab", LoadSceneMode.Single);
    }

    public void PrepareComponents (bool playerDied)
    {
        for (int i = 0; i < componentsInInventory.Length; i++)
        {
            int finalComponent = componentsInInventory[i];

            if (playerDied)
                finalComponent /= 2;

            LabManager.expeditionComponents[i] = finalComponent;

        }

        int finalAntigen = antigenCollected;

        if (playerDied)
            finalAntigen /= 2;

        LabManager.expeditionAntigen = finalAntigen;

    }

    public void CalculateScience(bool playerDied)
    {
        int scienceEnemies = enemiesDefeated * sciencePerEnemy;
        ResultScreen.scienceEnemiesDefeated = scienceEnemies;

        int scienceComponents = componentsCollected * sciencePerComponent;
        ResultScreen.scienceComponentsCollected = scienceComponents;

        int scienceConsumables = consumablesCollected * sciencePerConsumable;
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

        for (int i = 0; i < GameManager.itemConsumable.Length; i++)
        {
            consumableQty += GameManager.itemConsumable[i];
            GameManager.itemConsumable[i] = 0;

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
