using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss1Scene : MonoBehaviour
{
    public Player player;
    public Boss monster;
    public BattleEnemy enemy;
    public GameObject gameObject;
    public static Entity monsterE;
    public static string prebatlle;
    public Entity isdead;
    public GameManager gameManager;
    public int count = 0;
    public AudioClip clip;
    public int tempstm;
    void Start()
    {
        player = FindObjectOfType(typeof(Player)) as Player;
        isdead = player.entity;
    }

    public void Scene2()
    {
        prebatlle = SceneManager.GetActiveScene().name;
        tempstm = PlayerController.tempstm;
        player.entity.recoverystm = tempstm;
        SceneManager.LoadScene("batalha");
        Player.entitysoul = player.entity;
        monsterE = monster.entity;
    }
    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
        player.entity.dead = false;
        player.entity.currenthealth = 0;
        player.gameObject.SetActive(true);
        monster.gameObject.SetActive(true);
    }
    void Update()
    {
        monster = FindObjectOfType(typeof(Boss)) as Boss;
        enemy = FindObjectOfType(typeof(BattleEnemy)) as BattleEnemy;
        if (monster != null)
        {
            monsterE = monster.entity;
            gameObject = monster.gameObject;
        }
        gameManager = FindObjectOfType(typeof(GameManager)) as GameManager;

        if (SceneManager.GetActiveScene().name == prebatlle)
            Destroy(this.gameObject);
        if (monster != null)
        {
            if (monster.entity.incombat == true && count == 0)
            {
                count = 1;
                player.entity.wasincombat = 1;
                Player.nextInventory = player.inventory;
                player.gameObject.SetActive(false);
                Destroy(gameManager.gameObject);
                Scene2();
                Instantiate(gameObject, new Vector3(10.0f, 10.0f, 0), transform.rotation);
            }
            monster.entity = monsterE;
        }
        if (enemy != null)
        {
            if (enemy.entity.incombat == false && count == 1 && enemy.entity.dead == true && Notrespawn.runned != true)
            {
                count = 0;
                CenaryChanger.DefeatedMonsters[SceneChange.i] = "Medussa";
                Player.monsterkill++;
                SceneChange.i++;
                foreach (Inventory.Slot slot in player.inventory.slots)
                {
                    if (slot.ItemName == "Compras")
                    {
                        slot.CanDestroy = false;
                    }
                }
                Player.fromscene = true;
                Scene1();
                if (Player.buffed > 0)
                {
                    Player.buffed--;
                    if (Player.buffed == 0)
                    {
                        player.entity.buffdmg = 1;
                        player.entity.buffRes = 1;
                        player.entity.buffStm = 1;
                        player.entity.buffFurt = 1;
                        player.entity.buffHealth = 1;
                        player.entity.expmod = 1;
                        Player.buff = "";
                    }
                }
                Destroy(player.gameObject);
                Destroy(GameObject.FindWithTag("SceneChanger"));
            }
            if (enemy.entity.incombat == false && count == 1 && enemy.entity.dead == false && Notrespawn.runned == true)
            {
                count = 0;
                Player.fromscene = true;
                Continue();
                if (Player.buffed > 0)
                {
                    Player.buffed--;
                    if (Player.buffed == 0)
                    {
                        player.entity.buffdmg = 1;
                        player.entity.buffRes = 1;
                        player.entity.buffStm = 1;
                        player.entity.buffFurt = 1;
                        player.entity.buffHealth = 1;
                        player.entity.expmod = 1;
                        Player.buff = "";
                    }
                }
                Destroy(player.gameObject);
                Destroy(GameObject.FindWithTag("SceneChanger"));
            }
        }

        if (enemy != null)
        {
            if (enemy.entity.incombat == false && isdead.dead == true)
            {
                GameOver();
                isdead.dead = false;
            }
        }
    }
    public static void Scene1()
    {
        SceneManager.LoadScene(prebatlle + "Dream");
        prebatlle = "";
        monsterE = null;
    }
    public static void Continue()
    {
        Player.fromscene = true;
        Destroy(GameObject.FindWithTag("SceneChanger"));
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        SceneManager.LoadScene(prebatlle);
        prebatlle = "";
    }
}
