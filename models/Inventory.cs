using System.Security;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory
{
    // quando o player dropar itens um vetor de gameobjects é criado assim como um vetor transform, assim como uma string que pega info da cena.
    [System.Serializable]
    public class EquipSlot
    {
        public string SlotName;
        public string ItemName;
        public bool IsEmpty = true;
        public string Efeito;
        public string Descricao;
        public Sprite icon;
        public bool CanDestroy;
        public bool CanUse;
        public string Class = "";


        public EquipSlot()
        {
            ItemName = "";
            SlotName = "";
            IsEmpty = true;
            Descricao = "";
            Efeito = "";
            icon = null;
            CanDestroy = false;
            CanUse = false;
            Class = "";
        }
    }
    public class Slot
    {
        public bool CanDestroy;
        public string ItemName;
        public int Count;
        public int maxAllowed;
        public bool CanUse = false;
        public bool Buffer = true;
        public string BuffName = "";
        public int BuffBattles = 0;
        public string Descricao;
        public string Efeito;
        public Sprite icon;
        public string Class = "";

        public Slot()
        {
            CanUse = false;
            CanDestroy = false;
            ItemName = "";
            Count = 0;
            maxAllowed = 99;
            Buffer = false;
            BuffName = "";
            BuffBattles = 0;
            Efeito = "";
            Descricao = "";
            Class = "";
        }

        public bool CanAddItem()
        {
            if (Count < maxAllowed)
            {
                return true;
            }

            return false;
        }

        public void AddItem(Item item)
        {
            this.CanUse = item.data.CanUse;
            this.ItemName = item.data.ItemName;
            this.icon = item.data.icon;
            this.CanDestroy = item.data.CanDestroy;
            this.Buffer = item.data.Buffer;
            this.BuffName = item.data.BuffName;
            this.BuffBattles = item.data.BuffBattles;
            this.Descricao = item.data.Descricao;
            this.Efeito = item.data.Efeito;
            this.Class = item.data.Class;
            this.Count++;
        }
        public void RemoveItem()
        {
            if (Count > 0 && this.CanDestroy)
            {
                Count--;
                if (Count == 0 && !this.Buffer)
                {
                    icon = null;
                    ItemName = "";
                    CanUse = false;
                    CanDestroy = false;
                    Descricao = "";
                    Efeito = "";
                    Class = "";
                }
            }
        }
    }


    int equipIndex;

    public List<Slot> slots = new List<Slot>();
    public List<EquipSlot> equipslots = new List<EquipSlot>();

    public Inventory(int numequip, int numSlots)
    {
        for (int i = 0; i < numSlots; i++)
        {
            Slot slot = new Slot();
            slots.Add(slot);
        }
        for (int i = 0; i < numequip; i++)
        {
            EquipSlot equipslot = new EquipSlot();
            equipslots.Add(equipslot);
        }
    }

    public void Add(Item item)
    {
        foreach (Slot slot in slots)
        {
            if (slot.ItemName == item.data.ItemName && slot.CanAddItem())
            {
                slot.AddItem(item);
                return;
            }
        }

        foreach (Slot slot in slots)
        {
            if (slot.ItemName == "")
            {
                slot.AddItem(item);
                if (slot.Buffer == true)
                    for (int i = 0; i < slots.Count; i++)
                    {
                        if (slots[i].Buffer == true && slots[i].Count != slot.Count)
                            slot.Count = slots[i].Count;
                    }
                return;
            }
        }
    }
    public void Remove(int index)
    {
        if (slots[index].Buffer == true)
        {
            for (int i = 0; i < slots.Count; i++)
                if (slots[i].Buffer == true)
                    slots[i].RemoveItem();
        }
        else if (slots[index].CanDestroy)
            slots[index].RemoveItem();

    }
    public void Use(int index)
    {
        if (slots[index].Count > 0)
        {
            Player player;
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            if (slots[index].Buffer)
            {
                if (Player.buffed != 0)
                {
                    player.entity.buffdmg = 1;
                    player.entity.buffRes = 1;
                    player.entity.buffStm = 1;
                    player.entity.buffFurt = 1;
                    player.entity.buffHealth = 1;
                    player.entity.expmod = 1;
                }
                Player.buffed = slots[index].BuffBattles;
                Player.buff = slots[index].BuffName;
            }
            if (slots[index].CanUse)
            {
                switch (slots[index].ItemName)
                {
                    case "Jungle Interlude":
                        player.entity.buffRes = 1.3f;
                        break;
                    case "Bury The Bright":
                        player.entity.buffdmg = 1.5f;
                        break;
                    case "The lost trees":
                        player.entity.buffdmg = 1.3f;
                        break;
                    case "Eye of Puma":
                        player.entity.expmod = 1.3f;
                        break;
                    case "Chapeu Normal":
                        Equip(index, slots[index].Class);// APENAS MEXER COM UI
                        player.entity.CapRes = 2;// Efeito
                        break;
                    case "Botas Pretas":
                        Equip(index, slots[index].Class);
                        player.entity.BootsRes = 5;
                        player.entity.correndo = 5;
                        UnityEngine.Debug.Log("Bota preta");
                        break;
                    case "Cordão":
                        Equip(index, slots[index].Class);
                        player.entity.AccRes = 3;
                        break;
                    case "Battery":
                        CenaryChanger.BatteryLVL++;
                        for (int i = 0; i < player.inventory.slots.Count; i++)
                        {
                            if (player.inventory.slots[i].Buffer == true)
                                player.inventory.slots[i].Count = CenaryChanger.BatteryLVL;
                        }
                        break;
                    case "RedPill":
                        Player.redpill++;
                        break;
                    default:
                        UnityEngine.Debug.Log("ESTA SEM NOME");
                        break;
                }

                Remove(index);
            }
        }
    }
    public void UnEquipp(int index, string Class)
    {
        Player player;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        switch (Class)
        {
            case "Cap":
                player.entity.CapRes = 1;
                break;
            case "Colar":
                player.entity.ColarRes = 1;

                break;
            case "ACC":
                player.entity.AccRes = 1;

                break;
            case "Gloves":
                player.entity.GloveRes = 1;

                break;
            case "Boots":
                player.entity.BootsRes = 1;
                player.entity.correndo = 4;
                break;
            default:
                UnityEngine.Debug.Log("Esta sem classe");
                break;
        }
        foreach (Slot slot in slots)
        {
            if (slot.ItemName == "")
            {
                slot.CanUse = equipslots[index].CanUse;
                slot.ItemName = equipslots[index].ItemName;
                slot.icon = equipslots[index].icon;
                slot.CanDestroy = equipslots[index].CanDestroy;
                slot.Descricao = equipslots[index].Descricao;
                slot.Efeito = equipslots[index].Efeito;
                slot.Class = equipslots[index].Class;
                slot.Count = 1;
                equipslots[index].icon = null;
                equipslots[index].ItemName = "";
                equipslots[index].CanUse = false;
                equipslots[index].CanDestroy = false;
                equipslots[index].Descricao = "";
                equipslots[index].Efeito = "";
                equipslots[index].Class = "";
                equipslots[index].IsEmpty = true;
                return;
            }

        }
    }
    public void Equip(int index, string Class)
    {
        switch (Class)
        {
            case "Cap":
                equipIndex = 0;
                break;
            case "Colar":
                equipIndex = 1;
                break;
            case "ACC":
                equipIndex = 2;
                break;
            case "Gloves":
                equipIndex = 3;
                break;
            case "Boots":
                equipIndex = 4;
                break;
            default:
                UnityEngine.Debug.Log("Esta sem classe");
                break;
        }
        if (equipslots[equipIndex].IsEmpty)
        {
            equipslots[equipIndex].IsEmpty = false;
            equipslots[equipIndex].CanUse = slots[index].CanUse;
            equipslots[equipIndex].Efeito = slots[index].Efeito;
            equipslots[equipIndex].Descricao = slots[index].Descricao;
            equipslots[equipIndex].icon = slots[index].icon;
            equipslots[equipIndex].CanDestroy = slots[index].CanDestroy;
            equipslots[equipIndex].ItemName = slots[index].ItemName;
            equipslots[equipIndex].Class = slots[index].Class;
            equipslots[equipIndex].IsEmpty = false;
            Remove(index);
        }
        else
        {
            Slot temp = new Slot();

            temp.CanUse = equipslots[equipIndex].CanUse;
            temp.ItemName = equipslots[equipIndex].ItemName;
            temp.icon = equipslots[equipIndex].icon;
            temp.CanDestroy = equipslots[equipIndex].CanDestroy;
            temp.Descricao = equipslots[equipIndex].Descricao;
            temp.Efeito = equipslots[equipIndex].Efeito;
            temp.Class = equipslots[equipIndex].Class;

            equipslots[equipIndex].CanUse = slots[index].CanUse;
            equipslots[equipIndex].ItemName = slots[index].ItemName;
            equipslots[equipIndex].icon = slots[index].icon;
            equipslots[equipIndex].CanDestroy = slots[index].CanDestroy;
            equipslots[equipIndex].Descricao = slots[index].Descricao;
            equipslots[equipIndex].Efeito = slots[index].Efeito;
            equipslots[equipIndex].Class = slots[index].Class;
            equipslots[equipIndex].IsEmpty = false;

            foreach (Slot slot in slots)
            {
                if (slot.ItemName == "")
                {
                    slot.CanUse = temp.CanUse;
                    slot.ItemName = temp.ItemName;
                    slot.icon = temp.icon;
                    slot.CanDestroy = temp.CanDestroy;
                    slot.Descricao = temp.Descricao;
                    slot.Efeito = temp.Efeito;
                    slot.Class = temp.Class;
                    slot.Count = 1;
                    return;
                }
            }

        }
    }
}