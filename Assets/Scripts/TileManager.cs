using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{

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
                var spawnedTile = Instantiate(TileObjects[ChooseTile()], new Vector3(x, 0, z), Quaternion.identity);
                var temp = spawnedTile.GetComponent<Tile>();
                spawnedTile.name = $"({x},{z}), " + temp.type.ToString();
                temp.Coordinates = new Vector3(x, 0, z);
            }
        }
    }
    public int ChooseTile()
    {

        int temp=0;
        return temp;
    }
}
