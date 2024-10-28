using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ShoppSlot_UI : MonoBehaviour
{
    public Image itemIcon;
    public GameObject EfeitoBtn;
    public GameObject PurchaseButton;
    public Text Efeito;
    public Text NameItem;
    public Sprite purchased;
    public GameObject coinimage;
    public GameObject TxTPrice;
    public int price;

    public void SetItem(OpenShop.Slot slot)
    {
        if (slot.icon != null)
        {
            NameItem.text = slot.ItemName;
            itemIcon.sprite = slot.icon;
            PurchaseButton.SetActive(true);
            Efeito.text = slot.Efeito;
        }
    }
    public void SetEmpty()
    {
        itemIcon.sprite = purchased;
        PurchaseButton.SetActive(false);
        coinimage.SetActive(false);
        TxTPrice.SetActive(false);
    }

}
