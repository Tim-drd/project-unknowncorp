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
        player1 = GameObject.FindWithTag("Player").transform;
        audiosource.clip = playlist[0];
        audiosource.Play();
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
        float distance = Vector3.Distance(player1.position, pos);
        if (distance < 50)
        {
            GameObject tempGO = new GameObject("TempAudio");
            tempGO.transform.position = pos;
            AudioSource audioSource = tempGO.AddComponent<AudioSource>();
            audioSource.clip = clip;
            audioSource.volume = 0.1f;
            audioSource.outputAudioMixerGroup = soundEffectMixer;
            audioSource.Play();
            Destroy(tempGO, clip.length);
            return audioSource;
        }

        return null;
    }
}
