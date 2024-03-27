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
    private Coroutine currentState;
    [HideInInspector] public int turnNumber, buttonChoice;
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
        InitializeGame();
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
        turnNumber = 1;
        buttonChoice = 2;
        startedGame=true;
        EnemyManager.Instance.InitializeEnemyList();
        TileManager.Instance.InitializeMap();
        ResourceManager.Instance.InitializeResources();
    }
    public void GSM(GameState gamestate)
    {
        switch(gamestate)
        {
            case GameState.BeginTurn:
                if (currentState != null)
                {
                    StopCoroutine(currentState);
                }
                state = GameState.BeginTurn;
                currentState = StartCoroutine(BeginTurn());
                break;
            case GameState.Sailing:
                if (currentState != null)
                {
                    StopCoroutine(currentState);
                }
                state = GameState.Sailing;
                currentState = StartCoroutine(Sailing());
                break;
            case GameState.Interacting:
                if (currentState != null)
                {
                    StopCoroutine(currentState);
                }
                state =GameState.Interacting;
                currentState = StartCoroutine(Interacting());
                break;
            case GameState.Fighting:
                if (currentState != null)
                {
                    StopCoroutine(currentState);
                }
                state =GameState.Fighting;
                currentState = StartCoroutine(Fighting());
                break;
            case GameState.Raiding:
                if (currentState != null)
                {
                    StopCoroutine(currentState);
                }
                state =GameState.Raiding;
                currentState = StartCoroutine(Raiding());
                break;
            case GameState.Resting:
                if (currentState != null)
                {
                    StopCoroutine(currentState);
                }
                state =GameState.Resting;
                currentState = StartCoroutine(Resting());
                break;
            case GameState.Event:
                if(currentState != null)
                {
                    StopCoroutine(currentState);
                }
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
        var popup = UIManager.Instance.Popup;
        var tile = PlayerManager.Instance.TilePlayerIsOn.GetComponent<Tile>();
        if (tile.HasInteractable)
        {
            var temp = tile.Interactable.GetComponent<Interactable>();
            popup.SetActive(true);
            UIManager.Instance.SetUpPopup("Cap'n's Log: Day " + turnNumber, "A new day starts here in " + tile.Name + ", but unfortunately we share " +
                "these waters with the cursed " + temp.Name + "; we should set sail fer new coasts or drive their cursed vessel to the watery bottom.", 0);
        }
        else if(tile.type != Tile.TileType.Ocean)
        {
            popup.SetActive(true);
            UIManager.Instance.SetUpPopup("Cap'n's Log: Day " + turnNumber, "A new day starts here in "+ tile.Name + " and we could resolve " +
                "unfinished business here if ye think it best to stay. But avast, cap'n, thar will be treasure that awaits in unmapped waters...",0);
        }
        else
        {
            popup.SetActive(true);
            UIManager.Instance.SetUpPopup("Cap'n's Log: Day " + turnNumber, "A new day starts here in " + tile.Name + " and we should probably " +
                "move to new seas, lest we be cursed with the presence of the Kraken.", 0);
        }


        while (state == GameState.BeginTurn)
        {
            switch (buttonChoice)
            {
                case 0:
                    break;
                case 1:
                    GSM(GameState.Sailing);
                    break;
                default:
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
        var popup = UIManager.Instance.Popup;
        if (tile.HasInteractable)
        {
            var temp = tile.Interactable.GetComponent<Interactable>();
            popup.SetActive(true);
            UIManager.Instance.SetUpPopup(temp.ToSeparatedString(temp.type)+" Sighted", temp.description, 1);
        }
        else if (!tile.HasInteractable && (tile.type == Tile.TileType.RoyalPort || tile.type == Tile.TileType.Island))
        {
            popup.SetActive(true);
            UIManager.Instance.SetUpPopup(tile.ToSeparatedString(tile.type) + ", ahead!", tile.description, 2);
        }
        else
        {
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

        int dmgToPlayer = (enemy.Damage * (enemy.Manpower / enemyRand)) - ((rm.healthAmount / enemy.Damage) * (enemy.Manpower / enemyRand));
        int dmgToEnemy = (rm.cannonCount * (rm.crewAmount/playerRand)) - (enemy.Health / rm.cannonCount) * (rm.crewAmount / playerRand)     
            * (int)(rm.reputationAmount*0.1f);
        
        int enemyLosses = (rm.crewAmount * enemyRand * (int)((rm.reputationAmount * 0.1f) +1));
        int playerLosses = (int)((enemy.Manpower * playerRand)*0.25f);

        int lootGained = enemy.Loot / (int)Mathf.Clamp((playerLosses * 0.5f), 1, (playerLosses * 0.5f));

        print(dmgToPlayer + " " + playerLosses + " " + lootGained + " " + dmgToEnemy + " " + enemyLosses);
        enemy.Loot -= lootGained;
        enemy.Health -= dmgToEnemy;
        enemy.Manpower -= enemyLosses;
        enemy.CatchPlayerChance /= 2;

        rm.AdjustCrew(-playerLosses);
        rm.AdjustHealth(-dmgToPlayer);
        rm.AdjustGold(lootGained);
        rm.AdjustReputation((enemyLosses / enemyRand) + enemy.RenownValue);

        //sank the enemy 
        if (enemy.CatchPlayerChance < 10 || enemy.Loot <= 0 ||  enemy.Health <= 0 || enemy.Manpower <= 10)
        {
            EnemyManager.Instance.SpawnedEnemies.Remove(PlayerManager.Instance.TilePlayerIsOn.GetComponent<Tile>().Interactable.GetComponent<Interactable>());
            Destroy(PlayerManager.Instance.TilePlayerIsOn.GetComponent<Tile>().Interactable);
            tile.HasInteractable = false;
            tile.Interactable = null;
            popup.SetActive(true);
            UIManager.Instance.SetUpPopup("Arrr, Victory!", "We sunk the " + enemy.Name + ", a " + enemy.type + ", to the briney depths. Though we lost " +
                playerLosses + " mateys, our remaining " + rm.crewAmount + " crewmembers stole " + lootGained + " gold. With the burned hulk of the " 
                + enemy.Name +" and all " + enemyLosses + " of its crew are in Davy Jones' locker, we be free to raid in the area to hearts' content.", 0);
        }
        //didnt sink the enemy but still "Won"
        else
        {
            popup.SetActive(true);
            UIManager.Instance.SetUpPopup("Arrr, Victory!", "We beat the " + enemy.Name + ", a " + enemy.type + ", in battle. Though we lost " + 
                playerLosses +" mateys, our remaining " + rm.crewAmount + " crewmembers stole " + lootGained + " gold. The battered " + enemy.Name + 
                " still remains in this sea zone, (perhaps with some treasure we missed) so ye be warned if ye choose to stay here next morn'.", 0);
        }

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
                    UIManager.Instance.SetUpPopup("Success!", "We lost " + crewLostToFort + " crew raiding " + tile.Name + ", but found " + 
                        goldGained + " gold! " + "They'll fear us a little more now, since they lost " + hostilesLost + " people in the fight...", 0);

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
                   crewLostToIsland, tile.Hostiles);
                tile.Hostiles -= hostilesLost;

                if (inputNum <= 0) //everyone died
                {
                    popup.SetActive(true);
                    UIManager.Instance.SetUpPopup("Disaster!", "All the crew we sent died trying to raid " + tile.Name +
                        ". Our reputation definitely took a hit.", 0);
                    ResourceManager.Instance.AdjustReputation(-crewLostToIsland);
                }
                else if(Chance100(tile.lootChance) && inputNum > 0) //some survived; found loot. You get reputation = 10th of gold
                { 
                    goldGained = (int)(tile.lootAmount / Mathf.Clamp(crewLostToIsland, 1, crewLostToIsland));
                    tile.lootAmount = Mathf.Clamp(tile.lootAmount-goldGained, 0, tile.lootAmount);
                    tile.lootChance = Mathf.Clamp(tile.lootChance - 25, 0, tile.lootChance);
                    popup.SetActive(true);
                    UIManager.Instance.SetUpPopup("Success!", "We lost " + crewLostToIsland + " crew raiding " + tile.Name + ", but found " + 
                        goldGained +" gold! " + "They'll fear us a little more now, since they lost " + hostilesLost + " people in the fight...", 0);
                    ResourceManager.Instance.AdjustGold(goldGained);
                    ResourceManager.Instance.AdjustReputation(goldGained/10);
                }
                else //some survived; didnt find any loot
                {
                    popup.SetActive(true);
                    UIManager.Instance.SetUpPopup("Arrrg.", "We lost " + crewLostToIsland + " crew raiding " + tile.Name + 
                        ", but found no booty this time...", 0);
                }
            }
        }
        else
        {
            GSM(GameState.Resting);
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
            "Our ship's health is " + rm.healthAmount + "                                                                        " + 
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
        EnemyManager.Instance.HandleMoveEnemies();
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
                    turnNumber += 1;
                    GSM(GameState.BeginTurn);
                    break;
            }
            yield return null;
        }
    }
}
