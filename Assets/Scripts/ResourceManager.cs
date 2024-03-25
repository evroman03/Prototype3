using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    #region Singleton
    private static ResourceManager instance;
    public static ResourceManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindAnyObjectByType(typeof(ResourceManager)) as ResourceManager;
            return instance;
        }
        set
        {
            instance = value;
        }
    }
    #endregion

    // Serialized fields to set the initial values for different resources in the Unity Inspector.
    [SerializeField] int startingReputation = 0;
    [SerializeField] int startingGold = 100;
    [SerializeField] int startingHealth = 100;
    [SerializeField] int startingCrew = 10;

    // Variables to store the current amounts of different resources.
    public int reputationAmount, goldAmount, healthAmount, crewAmount, goldPerHealthFix=100;

    // Variables to store maximum and minimum values for certain resources

    [SerializeField] private int maxGold = 10000;
    [SerializeField] int maxReputation = 10000;

    // Constants to define the minimum and maximum values for certain resources.
    
    private const int maxHealth = 100;
    //public UIManager uiManager;

    private void Start()
    {
        InitializeResources();
        CheckResources();
    }

    // Method to initialize the resources with their starting values.
    private void InitializeResources()
    {
        reputationAmount = startingReputation;
        goldAmount = startingGold;
        healthAmount = startingHealth;
        crewAmount = startingCrew;
        UpdateUI();
    }

    // Method to log the current values of different resources.
    private void CheckResources()
    {
        Debug.Log("Reputation: " + reputationAmount);
        Debug.Log("Gold: " + goldAmount);
        Debug.Log("Health: " + healthAmount);
        Debug.Log("Crew: " + crewAmount);
    }

    // Method to adjust the reputation by a specified amount.
    public void AdjustReputation(int amount)
    {
        // Clamps the new reputation value between minReputation and maxReputation.
        int newReputation = Mathf.Clamp(reputationAmount + amount, 0, maxReputation);
        // If the reputation has changed, update it and log appropriate messages.
        if (newReputation != reputationAmount)
        {
            reputationAmount = newReputation;
            if (newReputation == maxReputation)
            {
                Debug.Log("Your reputation has reached maximum (" + maxReputation + ")");
            }
            else
            {
                Debug.Log("Your reputation is: " + reputationAmount);
            }
        }
        UpdateUI();
    }

    // Method to adjust the amount of gold by a specified amount.
    public void AdjustGold(int amount)
    {
        // Checks if there is enough gold to perform the action.
        if (goldAmount + amount < 0)
        {
            Debug.Log("You do not have enough gold to perform this action.");
        }
        else if(goldAmount + amount >= maxGold)
        {
            Debug.Log("You are now the richest pirate in all of the land");
        }
        else
        {
            // If there is enough gold, adjust it and log the new amount.
            goldAmount += amount;
            Debug.Log("You now have " + goldAmount + " gold");
        }
        UpdateUI();
    }

    // Method to adjust the health by a specified amount.
    public void AdjustHealth(int amount)
    {
        // Clamps the health within the range of 0 to maxHealth to prevent negative health.
        healthAmount = Mathf.Clamp(healthAmount + amount, 0, maxHealth); //ensures health will never go below 0
        // Log death message or the new health amount.
        if (healthAmount == 0)
        {
            Debug.Log("You have died");
        }
        else
        {
            Debug.Log("You now have " + healthAmount + " health");
        }
        UpdateUI();
    }

    // Method to adjust the number of crew members by a specified amount.
    public void AdjustCrew(int amount)
    {
        // Ensures the crew count doesn't go below 0.
        crewAmount = Mathf.Max(crewAmount + amount, 0);
        // Log all crew lost or the new crew count.
        if (crewAmount == 0)
        {
            Debug.Log("You have lost all your crew");
        }
        else
        {
            Debug.Log("You now have " + crewAmount + " crew");
        }
        UpdateUI();
    }

    public void UpdateUI()
    {
        UIManager.Instance.Reputation.value = reputationAmount;
        UIManager.Instance.Gold.value = goldAmount;
        UIManager.Instance.Crew.value = crewAmount;
        UIManager.Instance.ShipHealth.value = healthAmount;
    }
}
