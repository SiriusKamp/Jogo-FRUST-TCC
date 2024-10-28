using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Data", menuName = "Item Data", order = 50)]
public class ItemData : ScriptableObject
{
    public string ItemName = "Item Name";
    public bool CanDestroy = false;
    public Sprite icon;
    public string Efeito = "";
    public string Descricao = "";

    [Header("Usaveis")]
    public bool CanUse = true;
    public bool Buffer = true;
    public string BuffName = "";
    public int BuffBattles = 0;
    [Header("Equipaveis")]
    public bool CanEquip = true;
    public string Class = "";

}
