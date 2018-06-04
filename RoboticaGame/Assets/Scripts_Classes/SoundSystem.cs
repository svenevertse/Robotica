using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSystem : MonoBehaviour {

    public static SoundSystem ins;

    AudioSource audioSource;

    public AudioClip waveIntro;
    public AudioClip waveEnding;
    public AudioClip ambient;

    public enum SoundState
    {

        WaveStart,
        WaveEnding,
        Ambient,

    }

    public SoundState soundState;

    void Awake ()
    {

        ins = this;

    }

	void Start ()
    {

        audioSource = GetComponent<AudioSource>();

	}
	
	void Update ()
    {
		


	}

    public void PlayAudio (SoundState state)
    {

        switch (state)
        {

            case SoundState.WaveStart:

                audioSource.clip = waveIntro;
                audioSource.Play();
                break;

            case SoundState.WaveEnding:

                audioSource.clip = waveEnding;
                audioSource.Play();
                break;

            case SoundState.Ambient:

                audioSource.clip = ambient;
                audioSource.Play();
                break;


        }

    }

    
}
