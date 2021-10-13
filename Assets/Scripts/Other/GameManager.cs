using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager
{
    //Stage
    public static int currentPlayer;

    //Stage
    public static int currentGameStage = 2;
    public static int dataCollected;
    public static int[] dataNeededPerStage = {0,1,2,3,0};

    //Flags
    public static bool PlayerControl = true;

    //Resources
    public static int science;
    public static int scienceStored;

    public static int antigen;
    public static int antigenStored;

    public static int[] component = new int[4];
    public static int[] componentStored = new int[4];

    public static int[] itemConsumable = new int[5];

    //"Singletons"
    public static PlayerAtributes scriptPlayer;
    public static Inventory scriptInventory;
    public static CanvasGameplay scriptCanvas;
    public static CameraControl scriptCamera;
    public static HudMain scriptHud;
    public static ArenaManager scriptArena;
}
