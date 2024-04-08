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

    public AudioClip[] playlist;
    AudioSource playlistSource;

    private void Awake()
    {
        playlistSource = gameObject.GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if(!playlistSource.playOnAwake)
        {
            playlistSource.clip = playlist[Random.Range(0, playlist.Length)];
            playlistSource.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!playlistSource.isPlaying)
        {
            playlistSource.clip = playlist[Random.Range(0, playlist.Length)];
            playlistSource.Play();
        }
    }

    [SerializeField] private AudioClip CannonFire;
    [SerializeField] private AudioClip SwordsClashing;
    [SerializeField] private AudioClip GoldExchange;
    [SerializeField] private AudioClip RisingReputation;
    [SerializeField] private AudioClip Sailing;
    [SerializeField] private AudioClip StormEvent;
    [SerializeField] private AudioClip KrakenEvent;
    [SerializeField] private AudioClip MutinyEvent;
    [SerializeField] private AudioClip LoanSharkEvent;
    [SerializeField] private AudioClip MessageEvent;
    [SerializeField] private AudioClip TitanicEvent;
    [SerializeField] private AudioClip DJonesEvent;
    [SerializeField] private AudioClip GIslandEvent;
    [SerializeField] private AudioClip LevelComplete;
    [SerializeField] private AudioClip GameFail;

    //this object is the location of where the audioclips will play in the scene
    [SerializeField] private GameObject audioLocation;

    //this function is to be called when fighting a ship
    public void FireCannons()
    {
        AudioSource.PlayClipAtPoint(CannonFire, audioLocation.transform.position);
    }

    //this function is to be called when fighting on an island
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

    public void EventShark()
    {
        AudioSource.PlayClipAtPoint(LoanSharkEvent, audioLocation.transform.position);
    }

    public void EventMutiny()
    {
        AudioSource.PlayClipAtPoint(MutinyEvent, audioLocation.transform.position);
    }

    public void EventBottle()
    {
        AudioSource.PlayClipAtPoint(MessageEvent, audioLocation.transform.position);
    }

    public void EventTitanic()
    {
        AudioSource.PlayClipAtPoint(TitanicEvent, audioLocation.transform.position);
    }

    public void EventDavyJones()
    {
        AudioSource.PlayClipAtPoint(DJonesEvent, audioLocation.transform.position);
    }

    public void EventGoldISland()
    {
        AudioSource.PlayClipAtPoint(GIslandEvent, audioLocation.transform.position);
    }

    //this function is to be called when the player wins the game
    public void VictorySound()
    {
        AudioSource.PlayClipAtPoint(LevelComplete, audioLocation.transform.position);
    }

    //this function is to be called when the player loses the game
    public void GameOverSound()
    {
        AudioSource.PlayClipAtPoint(GameFail, audioLocation.transform.position);
    }
}
