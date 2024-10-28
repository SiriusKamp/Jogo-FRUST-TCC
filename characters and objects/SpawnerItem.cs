using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SpawnerItem : MonoBehaviour
{

    public class Cena
    {
        public string SceneName = "";
        public bool HasItem = true;
        public Cena()
        {
            SceneName = "";
            HasItem = true;
        }
    }
    public static int i = 0;
    public static List<Cena> cenas = new List<Cena>();
    private Collectable collectable;
    public GameObject item;
    int p = -1;
    public Sprite itemsprite;
    public ItemData itemdata;
    // Start is called before the first frame update
    void Start()
    {
        if (cenas.Count != 0)
            for (int u = 0; u < cenas.Count; u++)
            {
                if (SceneManager.GetActiveScene().name == cenas[u].SceneName)
                {
                    p = u;
                    if (cenas[u].HasItem == true)
                    {
                        UnityEngine.Debug.Log("FUNCIONANDO2");
                        Instantiate(item, this.transform);
                        collectable = FindObjectOfType(typeof(Collectable)) as Collectable;
                        collectable.GetComponent<Item>().data = itemdata;
                        collectable.GetComponent<SpriteRenderer>().sprite = itemsprite;
                    }
                }

            }
        if (p == -1)
        {
            UnityEngine.Debug.Log("FUNCIONANDO");
            Cena cena = new Cena();
            cenas.Add(cena);
            cenas[i].SceneName = SceneManager.GetActiveScene().name;
            cenas[i].HasItem = true;
            i++;
            Instantiate(item, this.transform);
            collectable = FindObjectOfType(typeof(Collectable)) as Collectable;
            collectable.GetComponent<Item>().data = itemdata;
            collectable.GetComponent<SpriteRenderer>().sprite = itemsprite;
        }
    }

    // Update is called once per frame
    void Update()
    {
        collectable = FindObjectOfType(typeof(Collectable)) as Collectable;
        if (collectable == null)
        {
            if (p == -1)
                cenas[i - 1].HasItem = false;
            else
                cenas[p].HasItem = false;
        }
    }
}
