using System.Text.RegularExpressions;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public string Name;
    //[HideInInspector] public string title;
    [HideInInspector] public string description;
    public enum InteractableType
    {
        RoyalGalleon, Brigantine, Merchant, Pirate, Kracken, SeaSerpent, DavyJones, Megalodon
    }

    /// <summary>
    /// Thanks ChatGPT
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public string ToSeparatedString(InteractableType value)
    {
        string enumString = value.ToString();
        string[] words = Regex.Split(enumString, @"(?<!^)(?=[A-Z])"); // Split by uppercase letters
        return string.Join(" ", words); // Join the words with a space
    }
    public InteractableType type;

    [HideInInspector] public int ShipDamage, ShipHealth, ShipManpower, ShipLoot, ShipRenownValue, CatchPlayerChance;
    public void Start()
    {
        if(Name == null)
        {
            Name = gameObject.name;
        }
    }
}
