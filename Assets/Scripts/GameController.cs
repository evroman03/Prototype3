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
        UIManager.Instance.ToggleCompassButtons(true);
        while (state == GameState.Sailing)
        {
            yield return null;
        }
    }
    IEnumerator Interacting()
    {
        UIManager.Instance.ToggleCompassButtons(false);
        var tile = PlayerManager.Instance.TilePlayerIsOn.GetComponent<Tile>();
        if (tile.HasInteractable)
        {
            var popup = UIManager.Instance.Popup;
            var temp = tile.Interactable.GetComponent<Interactable>();
            popup.SetActive(true);
            UIManager.Instance.SetUpPopup(temp.ToSeparatedString(temp.type)+" Sighted", temp.description, 1);
        }
        while(state == GameState.Interacting)
        {
            switch (buttonChoice)
            {
                case 0:

                    if (CatchPlayerChance(tile.Interactable.GetComponent<Interactable>().CatchPlayerChance))
                    {
                        tile.HasInteractable = false;
                        tile.Interactable = null;
                        GSM(GameState.Resting);
                    }
                    else
                    {
                        tile.HasInteractable = false;
                        tile.Interactable = null;
                        GSM(GameState.Resting);
                    }
                    break;
                case 1:
                    tile.HasInteractable = false;
                    tile.Interactable = null;
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
        var tile = PlayerManager.Instance.TilePlayerIsOn.GetComponent<Tile>();
        if (tile.type == Tile.TileType.PirateCove || tile.type == Tile.TileType.Island )
        {
            var popup = UIManager.Instance.Popup;
            popup.SetActive(true);
            UIManager.Instance.SetUpPopup(tile.ToSeparatedString(tile.type) + ", land ho!", tile.description, 1);
        }

        while (state == GameState.Resting)
        {
            switch (buttonChoice)
            {
                case 0:
                    break;
                case 1:
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
