using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GameController;

public class UIManager : MonoBehaviour
{
    #region Singleton
    private static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindAnyObjectByType(typeof(UIManager)) as UIManager;
            return instance;
        }
        set
        {
            instance = value;
        }
    }
    #endregion
    public Canvas canvas;
    public Button[] compassButtons;
    public GameObject Popup;

    public void ReadInputAsInt(string input)
    {
        int parsedNumber;
        if (int.TryParse(input, out parsedNumber))
        {
            print("Parsed integer: " + parsedNumber);
        }
        else
        {
            print("INPUT A NUMBER");

        }
    }
    public void ToggleCompassButtons(bool toggle)
    {
        foreach (Button button in compassButtons)
        {
            if (toggle)
            {
                button.interactable = true;
            }
            else
            {
                button.interactable = false;
            }
        }
    }
}
