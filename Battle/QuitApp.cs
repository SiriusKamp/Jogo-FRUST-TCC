using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitApp : MonoBehaviour
{
    public float quit;

    void Start()
    {
        quit = Time.time + 3;

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > quit)
            Application.Quit();
    }
}
