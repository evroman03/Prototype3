using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    public TextMeshProUGUI PopupTitle;
    public TextMeshProUGUI PopupMainBody;
    public TMP_InputField PopupInputField;
    public Button Yes;
    public Button No;
    public Slider Reputation;
    public Slider Gold;
    public Slider Crew;
    public Slider ShipHealth;
    public int InputFieldNum = 0;

    public void Start()
    {
        InputFieldNum = 0;
    }
    /// <summary>
    /// BoxesToShow: 0 is yes/ok. 1 is yes+no. 2 is yes/ok+input
    /// </summary>
    /// <param name="title"></param>
    /// <param name="description"></param>
    /// <param name="boxesToShow"></param>
    public void SetUpPopup(string title, string description, int boxesToShow)
    {
        switch(boxesToShow)
        {
            case 0:
                Yes.gameObject.SetActive(true);
                break;
            case 1:
                Yes.gameObject.SetActive(true);
                No.gameObject.SetActive(true);
                break;
            case 2:
                Yes.gameObject.SetActive(true);
                PopupInputField.gameObject.SetActive(true);
                break;
        }
        PopupTitle.text = title;    
        PopupMainBody.text = description;
    }
    public void ReadButton(int noYesContinue)
    {
        GameController.Instance.buttonChoice = noYesContinue;
    }
    public void ReadInputAsInt(string input)
    {
        InputFieldNum = 0;
        int parsedNumber;
        if (int.TryParse(input, out parsedNumber))
        {
            if(parsedNumber*ResourceManager.Instance.goldPerHealthFix < ResourceManager.Instance.goldAmount && ResourceManager.Instance.crewAmount > 4)
            {
                Yes.interactable = true;
                InputFieldNum = parsedNumber;
            }
        }
        else
        {
            PopupInputField.text = "";
            Yes.interactable = false;
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
