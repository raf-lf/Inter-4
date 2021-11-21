using System.Collections.Generic;

[System.Serializable]

public class SaveData
{
    #region Game Manager Data
    //Flags
    public bool endingPlayed;

    //Game Info
    public int currentGameStage;
    public List<UpgradeBase> upgradesPurchased = new List<UpgradeBase>();

    #endregion

    #region Lab Manager Data
    public int vaccineStored;
    public int scienceStored;
    public int antigenStored;
    public int[] componentStored = new int[4];
    public int dataStored;

    public int vaccineInRocket;
    public int[] componentcurrentTarget = new int[4];
    #endregion
}
