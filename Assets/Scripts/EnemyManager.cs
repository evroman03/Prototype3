using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public List<Interactable> SpawnedEnemies = new List<Interactable>();
    public List<GameObject> EnemiesChooseFrom = new List<GameObject>();
    public List<GameObject> AllGalleons = new List<GameObject>();
    public List<GameObject> AllBrigantines = new List<GameObject>();
    public List<GameObject> AllMerchants = new List<GameObject>();

    public int EnemySpawnChance = 5, maxGalleons = 1, maxBrigantines = 2, maxMerchants = 3;
    private GameObject objToSpawn = null;
    public void InitializeEnemyList()
    {
        PopulateECF(maxMerchants, AllMerchants);
        PopulateECF(maxBrigantines, AllBrigantines);
        PopulateECF(maxGalleons, AllGalleons);
    }
    public void PopulateECF(int max, List<GameObject> data)
    {
        for(int i =0; i <= max; i++)
        {
            EnemiesChooseFrom.Add(data[UnityEngine.Random.Range(0, data.Count)]);
        }
    }
    public void MBSpawnInteractable(Tile location)
    {
        if (GameController.Instance.Chance100(EnemySpawnChance))
        {
            List<GameObject> validEnemies = EnemiesChooseFrom.Where(enemy => enemy != null).ToList();
            var index = UnityEngine.Random.Range(0, validEnemies.Count);
            objToSpawn = validEnemies[index];
            location.Interactable = Instantiate(objToSpawn, location.transform.GetChild(1).transform);
            location.HasInteractable = true;


            Interactable objInt = location.Interactable.GetComponent<Interactable>();
            objInt.TileEnemyIsOn = objInt.transform.root.gameObject;

            SpawnedEnemies.Add(objInt);
        }
    }
    public void HandleMoveEnemies()
    {
        foreach(Interactable ntb in SpawnedEnemies)
        {
            int direction = UnityEngine.Random.Range(0, 9);
            switch(direction)
            {
                case 0: //North 1
                    MoveEnemy(1 , ntb);
                    break;
                case 1: //East 4
                    MoveEnemy(4, ntb);
                    break;
                case 2: //South 6
                    MoveEnemy(6, ntb);
                    break;
                case 3: //West 3
                    MoveEnemy(3 , ntb);
                    break;
                default: //dont move. 50% chance
                    break;
            }
        }
    }
    public void MoveEnemy(int direction, Interactable enemy)
    {
        GameObject[] tilesSurroundingEnemy = TileManager.Instance.GetSurroundingTiles(enemy.TileEnemyIsOn.GetComponent<Tile>());
        if (tilesSurroundingEnemy[direction].GetComponent<Tile>().type != Tile.TileType.Border && !tilesSurroundingEnemy[direction].GetComponent<Tile>().HasInteractable)
        {
            enemy.TileEnemyIsOn.GetComponent<Tile>().HasInteractable = false;
            enemy.TileEnemyIsOn.GetComponent<Tile>().Interactable = null;
            enemy.TileEnemyIsOn = tilesSurroundingEnemy[direction];
            enemy.TileEnemyIsOn.GetComponent<Tile>().HasInteractable = true;
            enemy.TileEnemyIsOn.GetComponent<Tile>().Interactable = enemy.gameObject;
            enemy.gameObject.transform.position = enemy.TileEnemyIsOn.transform.GetChild(1).transform.position;
        }
    }
}
