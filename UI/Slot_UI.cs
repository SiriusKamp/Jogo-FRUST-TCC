using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Slot_UI : MonoBehaviour
{
    public Image itemIcon;
    public TextMeshProUGUI quantityText;
    public GameObject BtnTrigger;
    public GameObject RmvButton;
    public Text Descricao;
    public Text Efeito;
    public Text name;
    public void SetItem(Inventory.Slot slot)
    {
        if (slot != null)
        {
            name.text = slot.ItemName;
            itemIcon.sprite = slot.icon;
            itemIcon.color = new Color(1, 1, 1, 1);
            quantityText.text = slot.Count.ToString();
            RmvButton.SetActive(true);
            BtnTrigger.SetActive(true);
            Descricao.text = slot.Descricao;
            Efeito.text = slot.Efeito;
        }
    }
    public void SetEmpty()
    {
        name.text = null;
        itemIcon.sprite = null;
        itemIcon.color = new Color(1, 1, 1, 0);
        quantityText.text = "";
        RmvButton.SetActive(false);
        BtnTrigger.SetActive(false);
    }

}
