using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public int ordem = 0;
    public Transform[] waypos;
    public GameObject monster;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        monster = GameObject.FindWithTag("Monster");
    }
    public void Change()
    {
        if (ordem > waypos.Length - 1)
            ordem = 0;
        this.gameObject.transform.position = waypos[ordem].position;
        ordem++;
    }
}
