using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckMission : MonoBehaviour
{
    public Player player;
    public Missão mission;
    public Texto text;
    public Texto textfail;
    public CenaryChanger cenary;
    public int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType(typeof(Player)) as Player;
        if (Missão.isstarted)
        {
            foreach (Inventory.Slot slot in player.inventory.slots)
            {
                if (slot.ItemName != "Compras" && slot.ItemName != "MissionItem2" && slot.ItemName != "MissionItem3" && slot.ItemName != "MissionItem4" && slot.ItemName != "MissionItem5")
                    i++;
            }
            if (i != player.inventory.slots.Count)
            {
                text.trigger = true;
                cenary.changescene = false;
            }
            else
            {
                text.trigger = false;
                cenary.changescene = true;
            }
            if (Missão.completed)
                Destroy(this.gameObject);
        }
    }

    public void Finish()
    {
        mission.complete();
        if (!Missão.completed)
        {
            textfail.trigger = true;
            textfail.StartDialogue();
        }
        else
        {
            text.trigger = false;
            cenary.changescene = true;
            Destroy(this.gameObject);
        }

    }
}
