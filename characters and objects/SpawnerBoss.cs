using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBoss : MonoBehaviour
{
    public GameObject[] Boss;
    public GameObject Collectable;
    public static int order = 0;
    public Transform[] carpos;
    public Transform missionitemspawn;
    public Waypoints waypoint;
    public GameObject missionitem;
    public Boss monster;
    int i = 0;
    public Player player;
    // Start is called before the first frame update
    void Start()
    {
        order = 0;
        player = FindObjectOfType(typeof(Player)) as Player;
    }

    // Update is called once per frame
    void Update()
    {
        Collectable = GameObject.FindWithTag("Collectable");
        monster = FindObjectOfType(typeof(Boss)) as Boss;
        foreach (Inventory.Slot slot in player.inventory.slots)
        {
            if (slot.ItemName == "Compras")
            {
                if (Collectable != null)
                {
                    Destroy(Collectable.gameObject);
                }
            }
        }
        spawn();
    }
    public void spawn()
    {
        if (Collectable == null)
        {
            foreach (Inventory.Slot slot in player.inventory.slots)
            {
                if (slot.ItemName != "Compras")
                {
                    i++;
                }
            }
            if (i < player.inventory.slots.Count)
                i = 0;
            else
            {
                Instantiate(missionitem, missionitemspawn);
                if (monster != null)
                    Destroy(monster.gameObject);
                i = 0;
            }
            if (monster == null)
            {
                Instantiate(Boss[order], carpos[order]);
                order++;
                if (order > carpos.Length - 1)
                    order = 0;
            }
        }
        else
        if (monster != null)
            Destroy(monster.gameObject);

    }
}
