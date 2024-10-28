using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenShop : MonoBehaviour
{
    public ItemData[] itens;
    public class Slot
    {
        public string ItemName;
        public Sprite icon;
        public string Efeito;
        public ItemData itemdata;
        public Slot()
        {
            ItemName = "";
            icon = null;
            Efeito = "";
            itemdata = null;
        }
        public void BeBought()
        {
            icon = null;
        }
    }
    public List<Slot> slots = new List<Slot>();
    public static string[] ItensBoughts = new string[10];
    public static ItemData[] Itemdata = new ItemData[10];
    public static int numeroID = 0;
    public static int numeroIN = 0;

    void Start()
    {
        for (int i = 0; i < itens.Length; i++)
        {
            Slot slot = new Slot();
            slots.Add(slot);
        }
        for (int i = 0; i < itens.Length; i++)
        {
            slots[i].ItemName = itens[i].ItemName;
            slots[i].icon = itens[i].icon;
            slots[i].Efeito = itens[i].Efeito;
            slots[i].itemdata = itens[i];
            if (OpenShop.ItensBoughts != null)
                for (int u = 0; u < ItensBoughts.Length; u++)
                {
                    if (slots[i].ItemName == name)
                        slots[i].icon = null;
                }
        }
    }

    // Update is called once per frame
    public void Buy(int index)
    {
        Itemdata[numeroID] = slots[index].itemdata;
        ItensBoughts[numeroIN] = slots[index].ItemName;
        numeroID++;
        numeroIN++;
        slots[index].BeBought();
    }
}
