using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TileNameScript : MonoBehaviour
{
    #region Singleton
    private static TileNameScript instance;
    public static TileNameScript Instance
    {
        get
        {
            if (instance == null)
                instance = FindAnyObjectByType(typeof(TileNameScript)) as TileNameScript;
            return instance;
        }
        set
        {
            instance = value;
        }
    }
    #endregion
    public string[] OceanTileNames = new string[0];
    public string[] PirateTileNames = new string[0];
    public string[] RoyalTileNames = new string[0];
    public string[] IslandTileNames = new string[0];
}
