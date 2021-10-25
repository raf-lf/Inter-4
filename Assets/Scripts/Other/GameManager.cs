using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager
{
    //Stage
    public static int CurrentPlayerClass;

    //Stage
    public static int currentGameStage = 0;
    public static int dataStored;
    public static int[] dataNeededPerStage = {0,1,2,3,0};

    //Player
    public static List<UpgradeBase> upgradesPurchased = new List<UpgradeBase>();

    //Flags
    public static bool PlayerControl = true;

    public static int[] componentStored = new int[4];

    public static int[] itemConsumable = new int[5];

    //"Singletons"
    public static PlayerAtributes scriptPlayer;
    public static Inventory scriptInventory;
    public static CanvasGameplay scriptCanvas;
    public static CameraControl scriptCamera;
    public static HudGameplay scriptHud;
    public static ArenaManager scriptArena;
    public static LabManager scriptLab;
    public static AudioManager scriptAudio;
    public static PoolManager scriptPool;


}
