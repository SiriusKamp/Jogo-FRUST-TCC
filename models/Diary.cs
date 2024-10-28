using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Diary
{
    [System.Serializable]
    public class Slot
    {
        public string name;
        public string descrição;
        public Sprite sprite;
        public Slot()
        {
            name = "???";
            descrição = "=========  =========  =========  =========";
            sprite = null;
        }
        public void AddEnemy(EnemyesInfo enemy)
        {
            this.name = enemy.name;
            this.sprite = enemy.EnemySprite;
            this.descrição = enemy.descrição;
        }
    }
    public List<Slot> slots = new List<Slot>();
    public int numEnemies;
    public Diary(int numEnemies)
    {
        for (int i = 0; i < numEnemies; i++)
        {
            Slot slot = new Slot();
            slots.Add(slot);
        }
    }
    public void Add(EnemyesInfo enemy)
    {
        foreach (Slot slot in slots)
        {
            if (slot.name == "???")
            {
                slot.AddEnemy(enemy);
                return;
            }
        }
    }
}
