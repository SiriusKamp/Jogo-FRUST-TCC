using System;
using System.Globalization;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSRC : MonoBehaviour
{
    public static int song = 0;
    public int thissong;
    public void Awake(){
        GameObject[] musicobj = GameObject.FindGameObjectsWithTag("Game Music");
        if(thissong == song){
            Destroy(this.gameObject);
        }
        else if (musicobj.Length > 1)
        {
            Destroy(musicobj[0]);
        }
        DontDestroyOnLoad(this.gameObject);
        song = thissong;
        
    }
}
