using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager
{
    //Stage
    public static int currentPlayer;

    //Stage
    public static int currentGameStage;

    //Flags
    public static bool PlayerControl = true;

    //Resources
    public static int science;
    public static int scienceTotal;

    public static int antigen;
    public static int antigenDelievered;

    public static int[] itemConsumable = new int[5];

    //"Singletons"
    public static PlayerAtributes scriptPlayer;
    public static Inventory scriptInventory;
    public static CanvasGameplay scriptCanvas;
    public static CameraControl scriptCamera;
    public static HudMain scriptHud;
}
