using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    [Tooltip("These MUST add up to 100")] public int OceanChance=75, PirateCoveChance=3, RoyalPortChance=10, IslandChance=12;
    [Tooltip("Use these to control how many locations spawn.")] public int MaxPirateCoves = 3, MaxRoyalPorts=4, MaxIslands =10;
    private int currentPirateCoves = 0, currentPorts = 0, currentIslands=0;
    public int GridXSize, GridZSize;
    public List<GameObject> TileObjects = new List<GameObject>();
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            CreateGrid();
        }
    }
    public void CreateGrid()
    {
        int previousTile = 0;
        for(int x =0; x < GridXSize; x++)
        {
            for(int z = 0;  z < GridZSize; z++) 
            {
                if(x == GridXSize-1 || z == GridZSize-1 || x==0 || z==0)
                {
                    var spawnedBorder = Instantiate(TileObjects[0], new Vector3(x, 0, z), Quaternion.identity);
                    var temp = spawnedBorder.GetComponent<Tile>();
                    spawnedBorder.name = $"({x},{z}), " + temp.type.ToString();
                    temp.Coordinates = new Vector3(x, 0, z);
                }
                else
                {
                    int randomTile = TileChooser(previousTile);
                    var spawnedTile = Instantiate(TileObjects[randomTile], new Vector3(x, 0, z), Quaternion.identity);
                    
                    //var TileComponent = spawnedTile.GetComponent<Tile>();
                    //foreach(GameObject tile in GetSurroundingTiles(TileComponent))
                    //{
                    //    if (TileComponent.type.Equals(tile.GetComponent<Tile>().type))
                    //    {
                    //        Destroy(spawnedTile);
                    //    }
                    //}
                    var temp = spawnedTile.GetComponent<Tile>();
                    spawnedTile.name = $"({x},{z}), " + temp.type.ToString();
                    temp.Coordinates = new Vector3(x, 0, z);
                    previousTile = randomTile;
                }
            }
        }
    }
    public int TileChooser(int previousTile)
    {
        int randomValue = Random.Range(1, 101);
        int numToReturn=4; //Default to ocean
        if (randomValue <= OceanChance)
        {
            numToReturn =  4; //Ocean
        }
        else if (randomValue <= OceanChance + PirateCoveChance && currentPirateCoves < MaxPirateCoves)
        {
            numToReturn = 1; //Friendly
            currentPirateCoves++;
        }
        else if (randomValue <= OceanChance + PirateCoveChance + RoyalPortChance && currentPorts <MaxRoyalPorts)
        {
            numToReturn = 2; //Enemy
            currentPorts++;
        }
        else if (currentIslands<MaxIslands)
        {
            numToReturn = 3; //Island
            currentIslands++;
        }
        else
        {
            numToReturn = 4;
        }
        if (previousTile == numToReturn)
        {
            numToReturn = 4; //This prevents next-to tile placements in the x direction.
        }
        return numToReturn;
    }
    public GameObject[] GetSurroundingTiles(Tile tile)
    {
        GameObject[] surroundingTiles = new GameObject[8];
        int index = 0;
        for (int xOffset = -1; xOffset <= 1; xOffset++)
        {
            for (int zOffset = -1; zOffset <= 1; zOffset++)
            {
                // Skip the center tile (the given tile)
                if (xOffset == 0 && zOffset == 0)
                    continue;

                // Calculate the coordinates of the adjacent tile
                float adjacentX = tile.Coordinates.x + xOffset;
                float adjacentZ = tile.Coordinates.z + zOffset;
                surroundingTiles[index++] = GetTileAtCoordinates(adjacentX, adjacentZ);
            }
        }
        return surroundingTiles;
    }
    private GameObject GetTileAtCoordinates(float x, float z)
    {
        foreach(GameObject gameObject in TileObjects)
        {
            Vector2 temp = gameObject.GetComponent<Tile>().Coordinates;
            if(x == temp.x && z == temp.y)
            {
                return gameObject;
            }
        }
        print("I couldn't find the tile at " + x + ", " + z);
        return null; //This should never happen...
    }
}
