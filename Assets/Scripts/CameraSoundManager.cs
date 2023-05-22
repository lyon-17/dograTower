using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSoundManager : MonoBehaviour
{
    public AudioSource randomSong;
    public AudioSource lastSong;
    public AudioClip[] audioSources;
    public AudioClip[] audioSources2part;
    public AudioClip lastFloor;
    bool highFloors = false;
    bool lastPlay = false;
    // Start is called before the first frame update
    void Start()
    {
        randomSong = GetComponent<AudioSource>();
        lastSong = GetComponent<AudioSource>();
        randomAudio();
    }


    void randomAudio()
    {
        if (!highFloors)
            randomSong.clip = audioSources[Random.Range(0, audioSources.Length)];
        else
            randomSong.clip = audioSources2part[Random.Range(0, audioSources2part.Length)];
        randomSong.Play();
    }
    void playFinale()
    {
        randomSong.Stop();
        lastPlay = true;
        lastSong.clip = lastFloor;
        lastSong.loop = true;
        lastSong.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.getFloor() >= 6 && !highFloors)
        {
            highFloors = true;
            randomAudio();
        }
        if (GameManager.instance.getFloor() <= 5 && highFloors)
        {
            highFloors = false;
            randomAudio();
        }
        if(GameManager.instance.getFloor() == 10 && !lastPlay)
        {
            playFinale();
        }
        if (!randomSong.isPlaying && GameManager.instance.getFloor() != 10)
        {
            randomAudio();
        }
    }
}
