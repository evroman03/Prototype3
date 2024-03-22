using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    // Serialized fields to set the initial values for different resources in the Unity Inspector.
    [SerializeField] int startingReputation = 0;
    [SerializeField] int startingGold = 100;
    [SerializeField] int startingHealth = 100;
    [SerializeField] int startingCrew = 10;

    // Variables to store the current amounts of different resources.
    [SerializeField]  private int reputationAmount;
    [SerializeField] private int goldAmount;
    [SerializeField] private int healthAmount;
    [SerializeField] private int crewAmount;

    // Constants to define the minimum and maximum values for certain resources.
    private const int minReputation = -100;
    private const int maxReputation = 100;
    private const int maxHealth = 100;

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
        int newReputation = Mathf.Clamp(reputationAmount + amount, minReputation, maxReputation);
        // If the reputation has changed, update it and log appropriate messages.
        if (newReputation != reputationAmount)
        {
            reputationAmount = newReputation;
            if (newReputation == minReputation)
            {
                Debug.Log("Your reputation has reached minimum (" + minReputation + ")");
            }
            else if (newReputation == maxReputation)
            {
                Debug.Log("Your reputation has reached maximum (" + maxReputation + ")");
            }
            else
            {
                Debug.Log("Your reputation is: " + reputationAmount);
            }
        }
    }

    // Method to adjust the amount of gold by a specified amount.
    public void AdjustGold(int amount)
    {
        // Checks if there is enough gold to perform the action.
        if (goldAmount + amount < 0)
        {
            Debug.Log("You do not have enough gold to perform this action.");
        }
        else
        {
            // If there is enough gold, adjust it and log the new amount.
            goldAmount += amount;
            Debug.Log("You now have " + goldAmount + " gold");
        }
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
    }
}
