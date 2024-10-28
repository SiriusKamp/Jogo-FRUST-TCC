using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaeyUI : MonoBehaviour
{
    public GameObject diaryPannel;
    public EnemyesInfo[] Enemy;
    public List<SlotDiaryUI> slots = new List<SlotDiaryUI>();
    public int numEnemies;
    public int u = 0;
    public Diary diary;
    public void Start()
    {
        diaryPannel.SetActive(false);
        diary = new Diary(numEnemies);
        foreach (EnemyesInfo enemy in Enemy)
        {
            for (int i = 0; i < Enemy.Length; i++)
            {
                if (Enemy[u].name == CenaryChanger.DefeatedMonsters[i])
                {
                    diary.slots[u].name = Enemy[u].name;
                    diary.slots[u].sprite = Enemy[u].EnemySprite;
                    diary.slots[u].descrição = Enemy[u].descrição;
                }
            }
            u++;
        }
    }
    // Update is called once per frame


    public void Update()
    {
        if (diaryPannel.activeSelf == true)
        {
            Refresh();
        }
    }
    public void Refresh()
    {
        if (slots.Count == diary.slots.Count)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                if (diary.slots[i].name != "???")
                {
                    slots[i].SetItem(diary.slots[i]);
                }
                else
                {
                    slots[i].SetEmpty(diary.slots[i]);
                }
            }
        }
    }

}
