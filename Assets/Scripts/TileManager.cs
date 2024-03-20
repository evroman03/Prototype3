using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public int GridXSize, GridZSize;
    public GameObject TileObject;
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
                var spawnedTile = Instantiate(TileObject, new Vector3(x, 0, z), Quaternion.identity);
                spawnedTile.name = $"({x},{z})";
            }
        }
    }
}
