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
        }
    }
}
