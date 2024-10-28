using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class EquipSlotUi : MonoBehaviour
{
    public Sprite nullimage;
    public Image itemIcon;
    public GameObject BtnTrigger;
    public Text Descricao;
    public Text Efeito;

    public void SetItem(Inventory.EquipSlot slot)
    {
        if (!slot.IsEmpty)
        {
            itemIcon.sprite = slot.icon;
            itemIcon.color = new Color(1, 1, 1, 1);
            BtnTrigger.SetActive(true);
            Descricao.text = slot.Descricao;
            Efeito.text = slot.Efeito;
        }
    }
    public void SetEmpty()
    {
        itemIcon.sprite = nullimage;
        itemIcon.color = new Color(1, 1, 1, 1);
        BtnTrigger.SetActive(false);
    }

}
