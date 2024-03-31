using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public int reputationAmount, goldAmount, healthAmount, crewAmount, goldPerHealthFix=100, goldPerCrew = 20, cannonCount;

    // Variables to store maximum and minimum values for certain resources

    [SerializeField] private int maxGold = 10000;
    [SerializeField] int maxReputation = 100;
    [SerializeField] int maxCrew = 75;

    
    private const int maxHealth = 100;

    private void Start()
    {

    }

    // Method to initialize the resources with their starting values.
    public void InitializeResources()
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
    public bool CanSpendGold(int amount)
    {
        return goldAmount >= amount;
    }

    // Method to validate sending crew on missions
    public bool CanSendCrew(int amount)
    {
        return crewAmount >= amount;
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
        int newHealth = healthAmount + amount;

        if (newHealth > maxHealth) //REFUNDING GOLD
        {
            int remainder = newHealth - maxHealth;
            healthAmount = maxHealth;
            AdjustGold(remainder*goldPerHealthFix);
        }
        else
        {
            healthAmount = Mathf.Clamp(newHealth, 0, maxHealth);
        }
        UpdateUI();
    }

    // Method to adjust the number of crew members by a specified amount.
    public void AdjustCrew(int amount)
    {
        int newCrew = crewAmount + amount;

        if (newCrew > maxCrew) //REFUNDING GOLD
        {
            int remainder = newCrew - maxCrew;
            crewAmount = maxCrew;
            AdjustGold(remainder * goldPerCrew);
        }
        else
        {
            crewAmount = Mathf.Clamp(newCrew, 0, maxCrew);
        }
        UpdateUI();
    }

    public void UpdateUI()
    {
        UIManager.Instance.Reputation.value = reputationAmount;
        UIManager.Instance.Gold.value = goldAmount;
        UIManager.Instance.Crew.value = crewAmount;
        UIManager.Instance.ShipHealth.value = healthAmount;
        UIManager.Instance.ReputationText.text = reputationAmount + "/" + maxReputation;
        UIManager.Instance.GoldText.text = goldAmount + "/" + maxGold;
        UIManager.Instance.CrewText.text = crewAmount + "/" + maxCrew;
        UIManager.Instance.HealthText.text = healthAmount + "/" + maxHealth;
        CheckWinLoss();
    }
    public void CheckWinLoss()
    {
        if (reputationAmount >= maxReputation)
        {
            //Go to win screen
            SceneManager.LoadScene(2);
        }
        else if (goldAmount >= maxGold)
        {
            //Go to lose screen
            SceneManager.LoadScene(3);
        }
        else if (crewAmount <= 0)
        {
            SceneManager.LoadScene(4);
        }
        else if (healthAmount <= 0)
        {
            SceneManager.LoadScene(5);
        }
    }
}
