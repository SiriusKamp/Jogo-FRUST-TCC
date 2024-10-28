using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Security;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(IsInteractible))]
public class CenaryChanger : MonoBehaviour
{
    public Player player;
    public string nextmonster = "";
    public string nextScene = "";
    public int count = 0;
    public KeyCode interact = KeyCode.E;
    public static Vector3 position;
    public static string[] DefeatedMonsters = new string[] { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };
    public bool changescene;
    public static bool fromscene = false;
    public static bool caminho = false;
    public Vector3 playernextposition = new Vector3(3, 2.5f, 0);
    public static int BatteryLVL = 1;
    public AudioSource audioSource;
    public AudioClip clip;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        player = FindObjectOfType(typeof(Player)) as Player;

        if (caminho == true)
        {
            player.transform.position = CenaryChanger.position;
            caminho = false;
        }
        else if (fromscene == true && this.tag == "Porta")
        {
            player.transform.position = CenaryChanger.position;
            audioSource.PlayOneShot(clip);
            fromscene = false;

        }
    }
    // Update is called once per frame
    void Update()
    {
        if (player == null)
            player = FindObjectOfType(typeof(Player)) as Player;
    }
    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.tag == "Player" && Input.GetKeyDown(interact) && this.tag == "Porta" && changescene == true)
        {
            UseDoor();
        }
        else if (Input.GetKeyDown(interact) && changescene == true && collider.tag == "Player" && this.tag == "Caminho")
        {
            NoDoor();
        }
        else if (Input.GetKeyDown(interact) && changescene == true && collider.tag == "Player" && this.tag == "Cama")
        {
            UseBed();
        }

    }
    public void UseDoor()
    {
        player.entity.recoverystm = PlayerController.tempstm;
        changescene = false;
        SceneChange.monsterE = null;
        Boss1Scene.monsterE = null;
        SceneChangeCar.monsterE = null;
        Player.getstatus = player.entity;
        Player.nextInventory = player.inventory;
        Player.fromscene = true;
        CenaryChanger.fromscene = true;
        CenaryChanger.position = playernextposition;
        count = 0;
        Destroy(GameObject.FindWithTag("Monster"));
        Destroy(GameObject.FindWithTag("SceneChanger"));
        CheckKill();
    }
    public void NoDoor()
    {
        player.entity.recoverystm = PlayerController.tempstm;
        changescene = false;
        SceneChange.monsterE = null;
        Boss1Scene.monsterE = null;
        SceneChangeCar.monsterE = null;
        Player.getstatus = player.entity;
        Player.nextInventory = player.inventory;
        Player.fromscene = true;
        position = playernextposition;
        caminho = true;
        count = 0;
        Destroy(GameObject.FindWithTag("Monster"));
        Destroy(GameObject.FindWithTag("SceneChanger"));
        CheckKill();
    }

    public void UseBed()
    {
        player.entity.recoverystm = PlayerController.tempstm;
        Missão.Completemission();
        for (int i = 0; i < player.inventory.slots.Count; i++)
            if (player.inventory.slots[i].Buffer == true)
                player.inventory.slots[i].Count = BatteryLVL;
        changescene = false;
        Player.getstatus = player.entity;
        Player.nextInventory = player.inventory;
        Player.fromscene = true;
        position = playernextposition;
        SceneManager.LoadScene(nextScene);
        Destroy(player.gameObject);
        Destroy(this.gameObject);
    }
    public void CheckKill()
    {
        for (int i = 0; i < Player.monsterkill;)
        {
            if (CenaryChanger.DefeatedMonsters[i] == nextmonster)
            {
                SceneManager.LoadScene(nextScene + "Dream");
                Destroy(player.gameObject);
                Destroy(this.gameObject);
            }
            else if (CenaryChanger.DefeatedMonsters[i] != nextmonster)
                count++;
            i++;
        }
        if (count == Player.monsterkill)
        {
            SceneManager.LoadScene(nextScene);
            Destroy(player.gameObject);
            Destroy(this.gameObject);
        }
    }
    public void CanChangeScene()
    {
        changescene = true;
    }
    public void CannotChangeScene()
    {
        changescene = false;
    }
}
