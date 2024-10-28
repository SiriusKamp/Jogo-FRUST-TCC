using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_UI : MonoBehaviour
{
    public AudioClip clip;
    public AudioSource source;
    public GameObject inventorypannel;
    public Player player;
    public List<Slot_UI> slots = new List<Slot_UI>();
    public List<EquipSlotUi> equipslots = new List<EquipSlotUi>();
    void start()
    {
        player = FindObjectOfType(typeof(Player)) as Player;
    }
    // Update is called once per frame


    public void Update()
    {
        if (inventorypannel.activeSelf == true)
        {
            Refresh();
        }
    }
    public void Refresh()
    {
        if (slots.Count == player.inventory.slots.Count)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                if (player.inventory.slots[i].ItemName != "")
                {
                    slots[i].SetItem(player.inventory.slots[i]);
                }
                else
                {
                    slots[i].SetEmpty();
                }
            }
        }
        if (equipslots.Count == player.inventory.equipslots.Count)
        {
            for (int i = 0; i < equipslots.Count; i++)
            {
                if (player.inventory.equipslots[i].ItemName != "")
                {
                    equipslots[i].SetItem(player.inventory.equipslots[i]);
                }
                else
                {
                    equipslots[i].SetEmpty();
                }
            }
        }
    }

    public void Remove(int slotID)
    {
        if (player.inventory.slots[slotID].CanDestroy)
        {
            player.inventory.Remove(slotID);
            Refresh();
        }
    }
    public void Use(int slotID)
    {
        if (player.inventory.slots[slotID].CanUse)
        {
            if (player.inventory.slots[slotID].Buffer == true && player.inventory.slots[slotID].Count != 0)
                source.PlayOneShot(clip);
            player.inventory.Use(slotID);
            Refresh();
        }
    }
    public void UnEquipp(int slotID)
    {
        string Class = "";
        switch (slotID)
        {
            case 0:
                Class = "Cap";
                break;
            case 1:
                Class = "Colar";
                break;
            case 2:
                Class = "ACC";
                break;
            case 3:
                Class = "Gloves";
                break;
            case 4:
                Class = "Boots";
                break;
        }
        player.inventory.UnEquipp(slotID, Class);
        Refresh();
    }
}
