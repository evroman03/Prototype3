using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    [SerializeField] int reputationAmount;
    [SerializeField] int goldAmount;
    [SerializeField] int healthAmount;
    [SerializeField] int crewAmount;
    [SerializeField] int reputationChange;
    [SerializeField] int goldChange;
    [SerializeField] int healthChange;
    [SerializeField] int crewChange;

    private void Start()
    {
        print("Gold: " + goldAmount);
        print("Health: " + healthAmount);
        print("Crew: " + crewAmount);

        ReputationAdjust(reputationChange);
        GoldAdjust(goldChange);
        HealthAdjust(healthChange);
        CrewAdjust(crewChange);
    }

    public void ReputationAdjust(int amount)
    {
        reputationAmount += amount;
        if ((reputationAmount + amount) <= -100)
        {
            reputationAmount = -100;
            print("Your reputation has reached -100");
        }
        else if((reputationAmount + amount) >= 100 )
        {
            reputationAmount = 100;
            print("Your reputation has reached 100");
        }
        else
        {
            print("Your reputation is: " + reputationAmount);
        }

    }
    public void GoldAdjust(int amount)
    {
        if((goldAmount + amount) < 0)
        {
            print("You do not have enough money to purchase this.");
        }
        else
        {
            goldAmount += amount;
            print("You now have " + goldAmount + " gold");
        }

    }
    public void HealthAdjust(int amount)

    {
        if((healthAmount + amount) <= 0)
        {
            healthAmount = 0;
            print("You have died");
        }
        else
        {
            healthAmount += amount;
            print("You now have " + healthAmount + " health");
        }
    }
    public void CrewAdjust(int amount)
    {
        if ((crewAmount + amount) <= 0)
        {
            crewAmount = 0;
            print("You have lost all your crew");
        }
        else
        {
            crewAmount += amount;
            print("You now have " + crewAmount + " crew");
        }
    }
}
