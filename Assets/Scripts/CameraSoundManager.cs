
using UnityEngine;

/**
 * Holds the music
 */
public class CameraSoundManager : MonoBehaviour
{
    public AudioSource randomSong;
    public AudioSource lastSong;
    public AudioClip[] audioSources;
    public AudioClip[] audioSources2part;
    public AudioClip lastFloor;
    private GameObject _playerPos;
    private int _floor = 0;
    private bool _highFloors = false;
    private bool _lastPlay = false;
    // Start is called before the first frame update
    void Start()
    {
        _playerPos = GameObject.FindGameObjectWithTag("Player");
        randomSong = GetComponent<AudioSource>();
        lastSong = GetComponent<AudioSource>();
        randomAudio();
    }


    void randomAudio()
    {
        if (!_highFloors)
            randomSong.clip = audioSources[Random.Range(0, audioSources.Length)];
        else
            randomSong.clip = audioSources2part[Random.Range(0, audioSources2part.Length)];
        randomSong.Play();
    }
    void playFinale()
    {
        randomSong.Stop();
        _lastPlay = true;
        lastSong.clip = lastFloor;
        lastSong.loop = true;
        lastSong.Play();
    }

    // Update is called once per frame
    void Update()
    {
        //Set the floor and update the music
        if(_floor == 0)
        {
            Debug.Log("Setting floor");
            _floor = (int)(_playerPos.transform.position.y / 19)+1;
            GameManager.instance.setFloor(_floor);
            Debug.Log(GameManager.instance.getFloor());
        }
        if (GameManager.instance.getFloor() >= 7 && !_highFloors)
        {
            _highFloors = true;
            randomAudio();
        }
        if (GameManager.instance.getFloor() <= 6 && _highFloors)
        {
            _highFloors = false;
            randomAudio();
        }
        if(GameManager.instance.getFloor() == 10 && !_lastPlay)
        {
            playFinale();
        }
        if (!randomSong.isPlaying && GameManager.instance.getFloor() != 10)
        {
            randomAudio();
        }
    }
}
