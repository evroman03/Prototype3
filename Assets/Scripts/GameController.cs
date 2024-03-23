using System;
using System.Collections;
using System.Collections.Generic;
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
            UIManager.Instance.Popup.SetActive(true);
            var interactable = tile.GetComponent<Interactable>();
        }
        while(state == GameState.Interacting)
        {
            yield return null;
        }
    }
    IEnumerator Resting()
    {
        if (state == GameState.Resting)
        {
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
