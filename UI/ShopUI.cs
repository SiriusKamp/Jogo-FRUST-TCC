using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    public GameObject NoBuyPannel;
    public GameObject ShopPannel;
    public Text PlayerCoins;
    public Player player;
    public AudioClip clip;
    public AudioSource source;
    public List<ShoppSlot_UI> slots = new List<ShoppSlot_UI>();
    void start()
    {
    }
    // Update is called once per frame


    public void Update()
    {
        if (player == null)
        {
            player = FindObjectOfType(typeof(Player)) as Player;
            player.gameObject.SetActive(false);
        }
        if (ShopPannel.activeSelf == true)
        {
            Refresh();
        }
    }
    public void Refresh()
    {
        PlayerCoins.text = player.entity.coins.ToString();
        if (slots.Count == ShopPannel.GetComponent<OpenShop>().slots.Count)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                if (ShopPannel.GetComponent<OpenShop>().slots[i].icon != null)
                {
                    slots[i].SetItem(ShopPannel.GetComponent<OpenShop>().slots[i]);
                    if (OpenShop.ItensBoughts != null)
                        for (int u = 0; u < OpenShop.ItensBoughts.Length; u++)
                        {
                            if (slots[i].NameItem.text == OpenShop.ItensBoughts[u])
                                slots[i].SetEmpty();
                        }
                }
                else
                {
                    slots[i].SetEmpty();
                }
            }

        }
    }

    public void Buy(int slotID)
    {
        if (player.entity.coins >= slots[slotID].price)
        {
            source.PlayOneShot(clip);
            player.entity.coins -= slots[slotID].price;
            ShopPannel.GetComponent<OpenShop>().Buy(slotID);
            Refresh();
        }
        else
            NoBuyPannel.SetActive(true);
    }
}
