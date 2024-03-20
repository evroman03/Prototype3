using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    [Tooltip("These MUST add up to 100")] public int OceanChance=75, PirateCoveChance=3, RoyalPortChance=10, IslandChance=12;
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
                    var spawnedTile = Instantiate(TileObjects[ChooseTile()], new Vector3(x, 0, z), Quaternion.identity);
                    var temp = spawnedTile.GetComponent<Tile>();
                    spawnedTile.name = $"({x},{z}), " + temp.type.ToString();
                    temp.Coordinates = new Vector3(x, 0, z);
                }    
            }
        }
    }
    public int ChooseTile()
    {
        int randomValue = Random.Range(1, 101);
        print(randomValue);
        if (randomValue <= OceanChance)
        {
            return 4;
        }
        else if (randomValue <= OceanChance + PirateCoveChance)
        {
            return 1;
        }
        else if (randomValue <= OceanChance + PirateCoveChance + RoyalPortChance)
        {
            return 2;
        }
        else
        {
            return 3;
        }
    }
}
