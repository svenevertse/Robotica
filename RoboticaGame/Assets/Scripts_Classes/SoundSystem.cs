﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSystem : MonoBehaviour {

    public static SoundSystem ins;

    AudioSource audioSource;
    AudioSource playerAudio;

    Coroutine ambientRoutine;

    public AudioClip waveIntro;
    public AudioClip waveEnding;
    public AudioClip ambient;
    public AudioClip fireGunSound;
    public AudioClip getDamagePlayerSound;
    public AudioClip reloadAr, reloadHG;
    public AudioClip EEsong;

    public enum SoundState
    {

        WaveStart,
        WaveEnding,
        Ambient,
        PlayerDamage,
        FireGun,
        ReloadAR,
        ReloadHG,
        EESong,
        

    }

    public SoundState soundState;

    void Awake ()
    {

        ins = this;

    }

	void Start ()
    {

        audioSource = GetComponent<AudioSource>();
        playerAudio = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();

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
                ambientRoutine = StartCoroutine(AmbientSound(25f));

                break;

            case SoundState.WaveEnding:

                audioSource.clip = waveEnding;
                audioSource.Play();
                StopCoroutine(ambientRoutine);
                break;

            case SoundState.Ambient:

                audioSource.clip = ambient;
                audioSource.Play();
                break;

            case SoundState.PlayerDamage:

                playerAudio.clip = getDamagePlayerSound;
                playerAudio.Play();
                break;

            case SoundState.FireGun:

                playerAudio.clip = fireGunSound;
                playerAudio.Play();
                break;

            case SoundState.ReloadAR:

                playerAudio.clip = reloadAr;
                playerAudio.Play();
                break;

            case SoundState.ReloadHG:

                playerAudio.clip = reloadHG;
                playerAudio.Play();
                break;

            case SoundState.EESong:

                playerAudio.clip = EEsong;
                playerAudio.Play();
                break;


        }

    }

    IEnumerator AmbientSound (float waitTime)
    {

        yield return new WaitForSeconds(waitTime);

        PlayAudio(SoundState.Ambient);

    }

    
}
