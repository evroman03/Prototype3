using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public string Name;

    public enum TileType
    {
        Island, PirateCove, RoyalPort, Ocean, Border, 
    }
    public TileType type;
    public Vector3 Coordinates = new Vector3(0,0,0);
    public GameObject Fog, Interactable;
    public bool HasFogOfWar = true, Visited=false, HasInteractable =false;

    [HideInInspector] public int lootAmount, lootChance, Hostiles;
    [SerializeField] public string description;

    public string ToSeparatedString(TileType value)
    {
        string enumString = value.ToString();
        string[] words = Regex.Split(enumString, @"(?<!^)(?=[A-Z])"); // Split by uppercase letters
        return string.Join(" ", words); // Join the words with a space
    }
    public void FogState(bool state)
    {
        HasFogOfWar = state;
        if(HasFogOfWar)
        {
            Fog.GetComponent<Renderer>().enabled = false;
        }
        else if(!HasFogOfWar)
        {
            Fog.GetComponent<Renderer>().enabled = true;
        }
    }
    public void Awake()
    {
        switch(type)
        {
            case TileType.Island:
                Name = TileNameScript.Instance.IslandTileNames[UnityEngine.Random.Range(0, TileNameScript.Instance.IslandTileNames.Length)];
                break;
            case TileType.PirateCove:
                Name = TileNameScript.Instance.PirateTileNames[UnityEngine.Random.Range(0, TileNameScript.Instance.PirateTileNames.Length)];
                break;
            case TileType.RoyalPort:
                Name =TileNameScript.Instance.RoyalTileNames[UnityEngine.Random.Range(0, TileNameScript.Instance.RoyalTileNames.Length)];
                break;
            case TileType.Ocean:
                Name = TileNameScript.Instance.OceanTileNames[UnityEngine.Random.Range(0, TileNameScript.Instance.OceanTileNames.Length)];
                break;
        }
    }
}
