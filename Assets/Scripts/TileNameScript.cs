using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TileNameScript : MonoBehaviour
{
    #region Singleton
    private static TileNameScript instance;
    public static TileNameScript Instance
    {
        get
        {
            if (instance == null)
                instance = FindAnyObjectByType(typeof(TileNameScript)) as TileNameScript;
            return instance;
        }
        set
        {
            instance = value;
        }
    }
    #endregion
    public string[] OceanTileNames = new string[0];
    public string[] PirateTileNames = new string[0];
    public string[] RoyalTileNames = new string[0];
    public string[] IslandTileNames = new string[0];
    /*
Yarr there be both prostitutes and mateys here. Also the carpeter and the surgeon are the same person. Anything's fer sale, as long as you have dabloons!

Should we send crew ashore?
The steel of cannons glints off many a watchtower by the docks of this outpost, and the jolly skeletons of many a fellow pirate smile from signs to beware. However, we could steal precious hardware from this place, despite the risk. 

Should we drop anchor and attempt a raid?

The natives ne'r take kindly to bothersome outsiders; and there be snakes wanting yer legs and 'gators yer boots. However, its sands may hide BOOTY - a great treasure fer all the crew.

Should we send crew ashore?

Arrr, true, cap'n. There be naught but the sound of waves and the smell of a salty breeze out here.

Maybe we'll go fishing...
     */
}
