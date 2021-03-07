using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] playlist;
    public AudioSource audiosource;
    private int MusicIndex = 0;
    public AudioMixerGroup soundEffectMixer;
    public Transform player1;
    public Transform player2;
    public int number_of_players;

    public static AudioManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance d'AudioManager dans la scène");
            return;
        }

        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        number_of_players = 0;
        audiosource.clip = playlist[0];
        audiosource.Play();
    }
    
    void LateUpdate()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("PlayerClone");
        number_of_players = players.Length;
        if (number_of_players > 0)
        {
            player1 = players[0].transform;
            if (number_of_players > 1)
            {
                player2 = players[1].transform;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!audiosource.isPlaying)
        {
            PlayNext();
        }
    }

    void PlayNext()
    {
        MusicIndex = (MusicIndex + 1) % playlist.Length;
        audiosource.clip = playlist[MusicIndex];
        audiosource.Play();
    }

    public AudioSource PlayClip(AudioClip clip, Vector3 pos)
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("PlayerClone");
        if (number_of_players == 1)
        {
            player1 = players[0].transform;
        }
        if (number_of_players == 2)
        {
            player1 = players[0].transform;
            player2 = players[1].transform;
        }
        if (number_of_players > 0)
        {
            if (number_of_players == 1 || Vector3.Distance(player1.position, pos) <= Vector3.Distance(player2.position, pos))
            {
                float distance = Vector3.Distance(player1.position, pos);
                if (distance < 20)
                {
                    GameObject tempGO = new GameObject("TempAudio");
                    tempGO.transform.position = pos;
                    AudioSource audioSource = tempGO.AddComponent<AudioSource>();
                    audioSource.clip = clip;
                    audioSource.volume = 0.1f;
                    audioSource.spatialize = true;
                    audioSource.outputAudioMixerGroup = soundEffectMixer;
                    audioSource.Play();
                    Destroy(tempGO, clip.length);
                    return audioSource;
                }
            }
            else
            {
                float distance2 = Vector3.Distance(player2.position, pos);
                if (distance2 < 20)
                {
                    GameObject tempGO = new GameObject("TempAudio");
                    tempGO.transform.position = pos;
                    AudioSource audioSource = tempGO.AddComponent<AudioSource>();
                    audioSource.clip = clip;
                    audioSource.volume = 1.5f;
                    audioSource.spatialize = true;
                    audioSource.spread= 10f;
                    audioSource.outputAudioMixerGroup = soundEffectMixer;
                    audioSource.Play();
                    Destroy(tempGO, clip.length);
                    return audioSource;
                }
            }
        }
        return null;
    }
}
