using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] objeto;
    public GameObject monster;
    public static int order = 0;
    public Transform[] carpos;
    public Waypoints waypoint;
    // Start is called before the first frame update
    void Start()
    {
        order = 0;
    }

    // Update is called once per frame
    void Update()
    {
        monster = GameObject.FindWithTag("Monster");
        spawn();
    }
    public void spawn()
    {
        if (monster == null)
        {
            Instantiate(objeto[order], carpos[order]);
            waypoint.Change();
            order++;
            if (order > carpos.Length - 1)
                order = 0;

        }
    }
}
