﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindDontDestroyOnLoad : MonoBehaviour
{
    public GameObject[] rootsFromDontDestroyOnLoad;
    void Start()
    {
        rootsFromDontDestroyOnLoad = DontdestroyOnLoadAccessor.Instance.GetAllRootsOfDontDestroyOnLoad();
    }
}