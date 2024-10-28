using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXS : MonoBehaviour
{
    public AudioClip fxs;
    public AudioSource source;
    public float time = 0;
    float i = 0;
    bool play = false;
    public void Update()
    {
        if (i + time < Time.time && play)
        {
            source.PlayOneShot(fxs);
            play = false;
        }

    }
    public void playsound()
    {
        i = Time.time;
        play = true;
    }
}
