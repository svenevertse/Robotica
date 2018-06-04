using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasterEggSong : MonoBehaviour {

    void OnTriggerEnter (Collider trigger)
    {

        if(trigger.transform.tag == "Player")
        {

            SoundSystem.ins.PlayAudio(SoundSystem.SoundState.EESong);

        }

    }
}
