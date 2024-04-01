using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{

    #region Singleton
    private static SoundManager instance;
    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindAnyObjectByType(typeof(SoundManager)) as SoundManager;
            return instance;
        }
        set
        {
            instance = value;
        }
    }
    #endregion

    [SerializeField] private AudioClip CannonFire;
    [SerializeField] private AudioClip SwordsClashing;
    [SerializeField] private AudioClip GoldExchange;
    [SerializeField] private AudioClip RisingReputation;
    [SerializeField] private AudioClip RoyaleIsland;
    [SerializeField] private AudioClip PirateIsland;
    [SerializeField] private AudioClip NormalIsland;
    [SerializeField] private AudioClip Sailing;
    [SerializeField] private AudioClip StormEvent;
    [SerializeField] private AudioClip KrakenEvent;
    [SerializeField] private AudioClip MutinyEvent;
    [SerializeField] private AudioClip LoanSharkEvent;

    //this object is the location of where the audioclips will play in the scene
    [SerializeField] private GameObject audioLocation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FireCannons()
    {
        AudioSource.PlayClipAtPoint(CannonFire, audioLocation.transform.position);
    }
}
