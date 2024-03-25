using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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
    public enum GameState
    {
        None, Sailing, Interacting, Resting, Event,
    }
    public GameState state;
    
    void Start()
    {
        currentState = StartCoroutine(DummyCoroutine());
        UIManager.Instance.ToggleCompassButtons(false);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            GSM(GameState.Sailing);
        }
    }
    
    public void GSM(GameState gamestate)
    {
        switch(gamestate)
        {
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
    public bool CatchPlayerChance(int chance)
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
            UIManager.Instance.SetUpPopup(tile.ToSeparatedString(tile.type) + ", ahead!", tile.description, 1);
        }
        else
        {
            var popup = UIManager.Instance.Popup;
            popup.SetActive(true);
            UIManager.Instance.SetUpPopup(tile.ToSeparatedString(tile.type) + ", ahead!", tile.description, 0);
        }
        while(state == GameState.Interacting)
        {
            switch (buttonChoice)
            {
                case 0:

                    if(tile.Interactable != null)
                    {
                        //If we catch the player
                        if (CatchPlayerChance(tile.Interactable.GetComponent<Interactable>().CatchPlayerChance))
                        {
                            tile.HasInteractable = false;
                            tile.Interactable = null;
                            GSM(GameState.Resting);
                        }
                        //Otherwise the player escapes
                        else
                        {
                            tile.HasInteractable = false;
                            tile.Interactable = null;
                            GSM(GameState.Resting);
                        }
                    }
                    GSM(GameState.Resting);
                    break;
                case 1:
                    //If the player chooses to fight
                    tile.HasInteractable = false;
                    tile.Interactable = null;
                    GSM(GameState.Resting);
                    break;
                case 2:
                    //If no option clicked (player is deciding)
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
                    break;
                default:
                    break;
            }
            yield return null;
        }
    }
    IEnumerator Event()
    {
        if (state == GameState.Event)
        {
            yield return null;
        }
    }
}
