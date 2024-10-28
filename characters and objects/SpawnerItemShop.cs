using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerItemShop : MonoBehaviour
{
    public Transform SpawnPos;
    public GameObject Item;
    public GameObject[] ItensInScene;
    // Start is called before the first frame update
    void Start()
    {
        if (OpenShop.numeroID > 0)
        {
            for (int i = 0; i < OpenShop.numeroID; i++)
            {
                Instantiate(Item, SpawnPos);
            }
            ItensInScene = GameObject.FindGameObjectsWithTag("Collectable");
            for (int i = 0; i < OpenShop.numeroID; i++)
            {
                ItensInScene[i].GetComponent<Item>().data = OpenShop.Itemdata[i];
                OpenShop.Itemdata[i] = null;
            }
            OpenShop.numeroID = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
