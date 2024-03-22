using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public enum TileType
    {
        Island, PirateCove, RoyalPort, Ocean, Border, 
    }
    public TileType type;
    public Vector3 Coordinates = new Vector3(0,0,0);
    public GameObject Fog;
    public bool HasFogOfWar = true;
    public bool Visited = false;

    [HideInInspector] public int option1data;
    [HideInInspector] public int option2data;

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
}
