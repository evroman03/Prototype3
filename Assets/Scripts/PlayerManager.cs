using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public GameObject[] TilesSurroundingPlayer;

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
            PlayerShip = Instantiate(PlayerShip, TilePlayerIsOn.transform.GetChild(0));
            TilesSurroundingPlayer = TileManager.Instance.GetSurroundingTiles(TilePlayerIsOn.GetComponent<Tile>());
            RevealFog();
        }
    }

    public void MovePlayerNorth()
    {
        MovePlayer(1);
    }
    public void MovePlayerEast()
    {
        MovePlayer(4);
    }
    public void MovePlayerSouth()
    {
        MovePlayer(6);
    }
    public void MovePlayerWest()
    {
        MovePlayer(3);
    }
    /// <summary>
    /// The eight indexes of TSP are arranged as a Matrix 
    /// [0] [1] [2]
    /// [3] [x] [4]
    /// [5] [6] [7]
    /// We will use a parameter int direction to tell the player which direction to go to
    /// </summary>
    public void MovePlayer(int direction)
    {
        if(TilesSurroundingPlayer[direction].GetComponent<Tile>().type == Tile.TileType.Border)
        {
            UIManager.Instance.Popup.SetActive(true);
            UIManager.Instance.SetUpPopup("Arrrrrgh", "The world's a dangerous place thataways, cap'n", 0);
        }
        else
        {
            TilePlayerIsOn = TilesSurroundingPlayer[direction];
            TilesSurroundingPlayer = TileManager.Instance.GetSurroundingTiles(TilePlayerIsOn.GetComponent<Tile>());
            PlayerShip.transform.position = TilePlayerIsOn.transform.GetChild(0).transform.position;
            RevealFog();
            GameController.Instance.GSM(GameController.GameState.Interacting);
        }
    }
    public void RevealFog()
    {
        foreach (GameObject tile in TilesSurroundingPlayer)
        {
            tile.GetComponent<Tile>().FogState(true);
        }
        TilePlayerIsOn.GetComponent<Tile>().FogState(true);    
    }

    void Update()
    {
        
    }
}
