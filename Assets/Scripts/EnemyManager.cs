using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    #region Singleton
    private static EnemyManager instance;
    public static EnemyManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindAnyObjectByType(typeof(EnemyManager)) as EnemyManager;
            return instance;
        }
        set
        {
            instance = value;
        }
    }
    #endregion
    public List<string> AllEnemyTypes = new List<string>();
    public List<GameObject> AllGalleons = new List<GameObject>();
    public List<GameObject> AllBrigantines = new List<GameObject>();
    public List<GameObject> AllMerchants = new List<GameObject>();
    public int EnemySpawnChance =5, maxGalleons=1, maxBrigantines=2, maxMerchants=3;
    private GameObject objToSpawn = null;
    public void InitializeEnemyPlacements()
    {   
        //foreach(GameObject tile in TileManager.Instance.AllTiles)
        //{
        //    if (GameController.Instance.Chance100(EnemySpawnChance))
        //    {
        //        var temp = tile.GetComponent<Tile>();
        //        temp.HasInteractable = true;

        //        var shipType = UnityEngine.Random.Range(0, AllEnemyTypes.Count);
        //        temp.Interactable = Instantiate(objToSpawn, temp.transform.GetChild(1).transform);

        //    }
        //}
    }
}
