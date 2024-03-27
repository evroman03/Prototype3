using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameController : MonoBehaviour
{
    #region Singleton
    private static GameController instance;
    public static GameController Instance
    {
        get
        {
            if (instance == null)
                instance = FindAnyObjectByType(typeof(GameController)) as GameController;
            return instance;
        }
        set
        {
            instance = value;
        }
    }
    #endregion
    private Coroutine currentState = null;
    public int buttonChoice=2;
    [HideInInspector] public bool startedGame;
    public enum GameState
    {
        None, BeginTurn, Sailing, Interacting, Fighting, Raiding, Resting, Event,
    }
    public GameState state;
    
    void Start()
    {
        startedGame = false;
        currentState = StartCoroutine(DummyCoroutine());
        UIManager.Instance.ToggleCompassButtons(false);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.C) && !startedGame)
        {
            InitializeGame();
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
        }
    }
    public void InitializeGame()
    {
        startedGame=true;
        TileManager.Instance.InitializeMap();
        ResourceManager.Instance.InitializeResources();
        GSM(GameState.BeginTurn);
    }
    public void GSM(GameState gamestate)
    {
        switch(gamestate)
        {
            case GameState.BeginTurn:
                StopCoroutine(currentState);
                state = GameState.BeginTurn;
                currentState = StartCoroutine(BeginTurn());
                break;
            case GameState.Sailing:
                StopCoroutine(currentState);
                state = GameState.Sailing;
                currentState = StartCoroutine(Sailing());
                break;
            case GameState.Interacting:
                StopCoroutine(currentState);
                state=GameState.Interacting;
                currentState = StartCoroutine(Interacting());
                break;
            case GameState.Fighting:
                StopCoroutine(currentState);
                state=GameState.Fighting;
                currentState = StartCoroutine(Fighting());
                break;
            case GameState.Raiding:
                StopCoroutine(currentState);
                state=GameState.Raiding;
                currentState = StartCoroutine(Raiding());
                break;
            case GameState.Resting:
                StopCoroutine(currentState);
                state=GameState.Resting;
                currentState = StartCoroutine(Resting());
                break;
            case GameState.Event:
                StopCoroutine(currentState);
                state=GameState.Event;
                currentState = StartCoroutine(Event());
                break;
        }
    }
    public bool Chance100(int chance)
    {
        int temp = UnityEngine.Random.Range(1, 101);
        if(temp <= chance)
        {
            return true;
        }
        return false;
    }
    IEnumerator DummyCoroutine()
    {
        // Dummy coroutine to prevent currentState from being null
        yield return null;
    }
    IEnumerator BeginTurn()
    {
        buttonChoice = 2;
        print("BEGINSTATE");
        while (state == GameState.BeginTurn)
        {
            switch (buttonChoice)
            {
                case 0:
                    break;
                case 1:
                    break;
                default:
                    GSM(GameState.Sailing);
                    break;
            }
            yield return null;
        }
    }
    IEnumerator Sailing()
    {
        buttonChoice = 2;
        print("SAILING STATE");
        UIManager.Instance.ToggleCompassButtons(true);
        while (state == GameState.Sailing)
        {
            switch (buttonChoice)
            {
                case 0:
                    break;
                case 1:
                    GSM(GameState.Interacting);
                    break;
                default:
                    break;
            }
            yield return null;
        }
    }
    IEnumerator Interacting()
    {
        buttonChoice = 2;
        print("INTERACTINGSTATE");
        UIManager.Instance.ToggleCompassButtons(false);
        var tile = PlayerManager.Instance.TilePlayerIsOn.GetComponent<Tile>();
        if (tile.HasInteractable)
        {
            var popup = UIManager.Instance.Popup;
            var temp = tile.Interactable.GetComponent<Interactable>();
            popup.SetActive(true);
            UIManager.Instance.SetUpPopup(temp.ToSeparatedString(temp.type)+" Sighted", temp.description, 1);
        }
        else if (!tile.HasInteractable && (tile.type == Tile.TileType.RoyalPort || tile.type == Tile.TileType.Island))
        {
            var popup = UIManager.Instance.Popup;
            popup.SetActive(true);
            UIManager.Instance.SetUpPopup(tile.ToSeparatedString(tile.type) + ", ahead!", tile.description, 2);
        }
        else
        {
            var popup = UIManager.Instance.Popup;
            popup.SetActive(true);
            UIManager.Instance.SetUpPopup("Ahoy, " + tile.ToSeparatedString(tile.type) + " here.", tile.description, 0);
        }
        while(state == GameState.Interacting)
        {
            switch (buttonChoice)
            {
                case 0:

                    if(tile.Interactable != null)
                    {
                        //If we catch the player, go to fighting
                        if (Chance100(tile.Interactable.GetComponent<Interactable>().CatchPlayerChance))
                        {
                            GSM(GameState.Fighting);
                        }
                        //Otherwise the player escapes, enable sailing and a popup.
                        else
                        {
                            var popup = UIManager.Instance.Popup;
                            popup.SetActive(true);
                            UIManager.Instance.SetUpPopup("Escaped from the " + tile.Interactable.name, "Choose a direction to escape to, cap'n.", 0);
                            UIManager.Instance.ToggleCompassButtons(true);
                            GSM(GameState.Resting);
                        }
                    }
                    GSM(GameState.Resting);
                    break;
                case 1:
                    //If the player chooses to fight
                    if(tile.HasInteractable)
                    {
                        GSM(GameState.Fighting);
                    }
                    else if(tile.type == Tile.TileType.Island || tile.type == Tile.TileType.RoyalPort)
                    {
                        GSM(GameState.Raiding);
                    }
                    else
                    {
                        GSM(GameState.Resting);
                    }
                    break;
                case 2:
                    //If no option clicked (player is deciding)
                    break;
            }
            yield return null;
        }
    }
    IEnumerator Fighting()
    {
        buttonChoice = 2;
        print("FIGHTINGSTATE");
        var tile = PlayerManager.Instance.TilePlayerIsOn.GetComponent<Tile>();
        var popup = UIManager.Instance.Popup;
        var enemy = PlayerManager.Instance.TilePlayerIsOn.GetComponent<Tile>().Interactable.GetComponent<Interactable>();
        var rm = ResourceManager.Instance;

        int enemyRand = UnityEngine.Random.Range(3, 5);  //More punishing (their dmg divided by a larger number, their losses guaranteed to be bigger)
        int playerRand = UnityEngine.Random.Range(1, 3); //Possibility to be less punishing

        int dmgToPlayer = (enemy.ShipDamage * (enemy.ShipManpower / enemyRand)) - ((rm.healthAmount / enemy.ShipDamage) * (enemy.ShipManpower / enemyRand));
        int dmgToEnemy = (rm.cannonCount * (rm.crewAmount/playerRand)) - (enemy.ShipHealth / rm.cannonCount) * (rm.crewAmount / playerRand)     * (int)(rm.reputationAmount*0.1f);
        
        int enemyLosses = (rm.crewAmount * enemyRand * (int)(rm.reputationAmount * 0.1f));
        int playerLosses = (enemy.ShipManpower * (int)(playerRand*0.5f));

        while (state == GameState.Fighting)
        {
            switch (buttonChoice)
            {
                case 0:
                    break;
                case 1:
                    GSM(GameState.Resting);
                    break;
                default:
                    break;
            }
            yield return null;
        }
    }
    IEnumerator Raiding()
    {
        buttonChoice = 2;
        print("RAIDINGSTATE");
        var inputNum = UIManager.Instance.InputFieldNum;
        var tile = PlayerManager.Instance.TilePlayerIsOn.GetComponent<Tile>();
        var popup = UIManager.Instance.Popup;
        int crewLostToFort = 0;
        int crewLostToIsland = 0;
        int goldGained = 0;

        print(inputNum);
        if(inputNum > 0) //if you actually sent crew to the island
        {
            if (tile.type == Tile.TileType.RoyalPort) //if raiding a fort
            {
                crewLostToFort = Mathf.Clamp((int)(tile.Hostiles * UnityEngine.Random.Range(1, 5f)), 1, inputNum);
                ResourceManager.Instance.AdjustCrew(-crewLostToFort);
                inputNum -= crewLostToFort;
                int hostilesLost = Mathf.Clamp((int)(inputNum * UnityEngine.Random.Range(1, 3) * (ResourceManager.Instance.reputationAmount * 0.1f)),
                    crewLostToFort, tile.Hostiles);
                tile.Hostiles -= hostilesLost;

                if (inputNum > 0) //some survived
                {
                    goldGained = (int)(tile.lootAmount / Mathf.Clamp(crewLostToFort, 1, crewLostToFort));

                    popup.SetActive(true);
                    UIManager.Instance.SetUpPopup("Success!", "We lost " + crewLostToFort + " crew raiding " + tile.Name + ", but found " + goldGained + 
                        " gold! " + "They'll fear us a little more now, since they lost " + hostilesLost + "people in the fight...", 0);

                    ResourceManager.Instance.AdjustReputation(hostilesLost*10);
                    ResourceManager.Instance.AdjustGold(goldGained);
                }
                else //everyone died
                {
                    popup.SetActive(true);
                    ResourceManager.Instance.AdjustReputation(-(inputNum * 10));
                    UIManager.Instance.SetUpPopup("Disaster!", "All the crew we sent died trying to raid " + tile.Name + 
                        ". Our reputation definitely took a hit.", 0);
                }
                tile.lootAmount -= goldGained;
            }
            else
            {
                crewLostToIsland = Mathf.Clamp((int)(tile.Hostiles * UnityEngine.Random.Range(0, 1.01f)), 0, inputNum);
                ResourceManager.Instance.AdjustCrew(-crewLostToIsland);
                inputNum -= crewLostToIsland;
                int hostilesLost = Mathf.Clamp((int)(inputNum * UnityEngine.Random.Range(1, 3) * (ResourceManager.Instance.reputationAmount * 0.1f)),
                   crewLostToFort, tile.Hostiles);
                tile.Hostiles -= hostilesLost;

                if (inputNum <= 0) //everyone died
                {
                    popup.SetActive(true);
                    UIManager.Instance.SetUpPopup("Disaster!", "All the crew we sent died trying to raid " + tile.Name + ". Our reputation definitely took a hit.", 0);
                    ResourceManager.Instance.AdjustReputation(-crewLostToIsland);
                }
                else if(Chance100(tile.lootChance) && inputNum > 0) //some survived; found loot. You get reputation = 10th of gold
                { 
                    goldGained = (int)(tile.lootAmount / Mathf.Clamp(crewLostToIsland, 1, crewLostToIsland));
                    tile.lootAmount = Mathf.Clamp(tile.lootAmount-goldGained, 0, tile.lootAmount);
                    tile.lootChance = Mathf.Clamp(tile.lootChance - 25, 0, tile.lootChance);
                    popup.SetActive(true);
                    UIManager.Instance.SetUpPopup("Success!", "We lost " + crewLostToIsland + " crew raiding " + tile.Name + ", but found " + goldGained +
                        " gold! " + "They'll fear us a little more now, since they lost " + hostilesLost + "people in the fight...", 0);
                    ResourceManager.Instance.AdjustGold(goldGained);
                    ResourceManager.Instance.AdjustReputation(goldGained/10);
                }
                else //some survived; didnt find any loot
                {
                    popup.SetActive(true);
                    UIManager.Instance.SetUpPopup("Arrrg.", "We lost " + crewLostToIsland + " crew raiding " + tile.Name + ", but found no booty this time...", 0);
                }
            }
        }

        while (state == GameState.Raiding)
        {
            switch (buttonChoice)
            {
                case 0:
                    break;
                case 1:
                    GSM(GameState.Resting);
                    break;
                default:
                    break;
            }
            yield return null;
        }
    }
    IEnumerator Resting()
    {
        buttonChoice = 2;
        print("RESTINGSTATE");

        var tile = PlayerManager.Instance.TilePlayerIsOn.GetComponent<Tile>();
        var popup = UIManager.Instance.Popup;
        var rm = ResourceManager.Instance;
        popup.SetActive(true);
        UIManager.Instance.SetUpPopup("time to rest, captain.",
            "We have " + rm.crewAmount + " crew aboard, and " + rm.goldAmount + " gold. It takes 4 crewmates and costs 100 gold to " +
            "repair 1 point of ship health." + "                                                                                           " +
            "Our ship's health is " + ResourceManager.Instance.healthAmount + "                                                                        " + 
            "How much health do you repair, cap'n?", 2) ;
        UIManager.Instance.Yes.interactable = false;

        while (state == GameState.Resting)
        {
            switch (buttonChoice)
            {
                case 0:
                    break;
                case 1:
                    ResourceManager.Instance.AdjustHealth(UIManager.Instance.InputFieldNum);
                    ResourceManager.Instance.AdjustGold(-UIManager.Instance.InputFieldNum*ResourceManager.Instance.goldPerHealthFix);
                    GSM(GameState.Event);
                    break;
                default:
                    break;
            }
            yield return null;
        }
    }
    IEnumerator Event()
    {
        buttonChoice = 2;
        print("EVENTSTATE");
        while (state == GameState.Event)
        {
            switch (buttonChoice)
            {
                case 0:
                    break;
                case 1:
                    break;
                default:
                    GSM(GameState.BeginTurn);
                    break;
            }
            yield return null;
        }
    }
}
