using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Singleton
    private static PlayerManager instance;
    public static PlayerManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindAnyObjectByType(typeof(PlayerManager)) as PlayerManager;
            return instance;
        }
        set
        {
            instance = value;
        }
    }
    #endregion

    public GameObject TilePlayerIsOn;
    public GameObject PlayerShip;

    void Start()
    {
        TileManager.Instance.MapInitialized += InitializePlayer;  
    }
    public void InitializePlayer(bool mapInitialized)
    {
        if(mapInitialized)
        {
            if (TilePlayerIsOn == null)
            {
                TilePlayerIsOn = TileManager.Instance.GetTileAtCoordinates(1, 1);
            }
            print("HERE");
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
