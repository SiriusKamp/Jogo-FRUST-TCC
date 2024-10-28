using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missão : MonoBehaviour
{
    public static bool isstarted;
    public static bool completed = false;
    public static int reward;
    public int recompença;
    public string missionname;
    public Player player;
    public Texto portaTxT;
    public CenaryChanger porta;
    public GameObject geladeira;
    public Inventory_UI inventory;
    public Item item;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType(typeof(Player)) as Player;
        if (isstarted)
        {
            this.gameObject.SetActive(false);
            portaTxT.trigger = false;
            porta.changescene = true;
            geladeira.gameObject.SetActive(true);
            this.transform.position = new Vector3(100, 100, 0);
        }
        if (completed)
        {
            portaTxT.trigger = true;
            porta.changescene = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void StartMission()
    {
        isstarted = true;
        player.inventory.Add(item);
    }
    public void FinishtMission()
    {
        reward = recompença;
        completed = true;
        UnityEngine.Debug.Log(Missão.reward);
    }
    public static void Completemission()
    {
        if (Missão.completed == true)
        {
            Player.getstatus.coins += reward;
            completed = false;
            reward = 0;
        }
        isstarted = false;
    }
    public void complete()
    {
        int i = 0;
        switch (missionname)
        {
            case "Mission1":
                foreach (Inventory.Slot slot in player.inventory.slots)
                {
                    if (slot.ItemName == "Compras")
                    {
                        slot.CanDestroy = true;
                        inventory.Remove(i);
                        FinishtMission();
                    }
                    i++;
                }
                break;
            default:
                return;
        }

    }

}
