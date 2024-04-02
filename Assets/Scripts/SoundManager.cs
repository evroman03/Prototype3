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

    //these two functions are to be called when fighting a ship or an island
    public void FireCannons()
    {
        AudioSource.PlayClipAtPoint(CannonFire, audioLocation.transform.position);
    }

    public void ClashingSwords()
    {
        AudioSource.PlayClipAtPoint(SwordsClashing, audioLocation.transform.position);
    }

    //this function is to be called when islands are looted, when crew is hired, or the ship is repaired
    public void ExchangeGold()
    {
        AudioSource.PlayClipAtPoint(GoldExchange, audioLocation.transform.position);
    }

    //this function is to be alled when the reputation meter increases
    public void ReputationIncrease()
    {
        AudioSource.PlayClipAtPoint(RisingReputation, audioLocation.transform.position);
    }

    //these functions are to be called when the player interacts with a royale, pirate, or normal island
    public void RoyaleDiscovery()
    {
        AudioSource.PlayClipAtPoint(RoyaleIsland, audioLocation.transform.position);
    }

    public void PirateEncounter()
    {
        AudioSource.PlayClipAtPoint(PirateIsland, audioLocation.transform.position);
    }

    public void IslandDiscovery()
    {
        AudioSource.PlayClipAtPoint(NormalIsland, audioLocation.transform.position);
    }

    //this function is called when the player moves on the map
    public void PlayerMovement()
    {
        AudioSource.PlayClipAtPoint(Sailing, audioLocation.transform.position);
    }

    //these functions are called when an event occurs
    public void EventStorm()
    {
        AudioSource.PlayClipAtPoint(StormEvent, audioLocation.transform.position);
    }

    public void EventKraken()
    {
        AudioSource.PlayClipAtPoint(KrakenEvent, audioLocation.transform.position);
    }


}
