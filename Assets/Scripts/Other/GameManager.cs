using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager
{
    //Flags
    public static bool endingPlayed;
    public static bool briefingPlayed;
    public static bool PlayerControl = true;

    //Game Info
    public static int currentGameStage = 0;
    public static List<int> upgradesPurchasedId = new List<int>();
    public static List<UpgradeBase> upgradesPurchased = new List<UpgradeBase>();


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
    public static DialogueSystem scriptDialogue;
    public static SkillManager scriptSkill;

    //"Preferences"
    public static bool soundsOn = true;
    public static bool musicOn = true;

    public static void SetFrameRate(int fps)
    {
        Application.targetFrameRate = fps;

    }
}
