using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SlotDiaryUI : MonoBehaviour
{
    public Image SlotIconDescription;
    public Image SlotIcon;
    public Text name;
    public Text descrição;

    public void SetItem(Diary.Slot slot)
    {
        if (slot != null)
        {
            SlotIcon.sprite = slot.sprite;
            SlotIconDescription.sprite = slot.sprite;
            SlotIconDescription.color = new Color(1, 1, 1, 1);
            SlotIcon.color = new Color(1, 1, 1, 1);
            descrição.text = slot.descrição;
            name.text = slot.name;
        }
    }
    public void SetEmpty(Diary.Slot slot)
    {
        SlotIcon.sprite = null;
        SlotIconDescription.sprite = null;
        SlotIconDescription.color = new Color(0, 0, 0, 1);
        SlotIcon.color = new Color(0, 0, 0, 1);
        descrição.text = slot.descrição;
        name.text = slot.name;
    }

}
