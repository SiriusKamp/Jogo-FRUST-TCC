using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keepobject : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
