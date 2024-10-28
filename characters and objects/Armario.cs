using System.Diagnostics;
using System.Security.Cryptography;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armario : MonoBehaviour
{

    public Player player;
    public static Entity tempentity;
    public GameObject terno;
    public GameObject pijama;
    public Texto textoporta;
    public Texto textocama;
    public CenaryChanger cama;
    public CenaryChanger porta;
    public int trocado;
    public AudioSource audiosource;
    public AudioClip clip;
    // Start is called before the first frame update

    void Start()
    {
        player = FindObjectOfType(typeof(Player)) as Player;
    }

    // Update is called once per frame
    void Update()
    {
        player = FindObjectOfType(typeof(Player)) as Player;
    }
    public void ChangeClothes()
    {
        audiosource.PlayOneShot(clip);
        Player.nextInventory = player.inventory;
        Player.fromscene = true;
        Player.getstatus = player.entity;
        if (trocado == 1)
        {
            Instantiate(pijama);
            UnityEngine.Debug.Log(trocado);
            trocado = 0;
            cama.changescene = true;
            porta.changescene = false;
            textoporta.trigger = true;
            textocama.trigger = true;
            porta.changescene = false;
            cama.changescene = true;
        }
        else if (trocado == 0)
        {
            Instantiate(terno);
            trocado = 1;
            porta.changescene = true;
            cama.changescene = false;
            textoporta.trigger = false;
            textocama.trigger = true;
            porta.changescene = true;
            cama.changescene = false;
        }
        Destroy(player.gameObject);
        player = FindObjectOfType(typeof(Player)) as Player;
        textocama.player = player;
        textoporta.player = player;
        player.transform.position = new Vector3(-2.12f, 2.74f, 0);

    }
}
