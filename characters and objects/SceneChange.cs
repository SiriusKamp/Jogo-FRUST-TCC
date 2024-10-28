using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChange : MonoBehaviour
{
    public Player player;
    public Monster monster;
    public static Entity monsterE;
    public static string prebatlle;
    public Entity isdead;
    public GameManager gameManager;
    public static int i = 0;
    public string monstername;
    public string nextscene = "";
    public int tempstm;
    public void Start()
    {
        player = FindObjectOfType(typeof(Player)) as Player;
        monsterE = monster.entity;
        isdead = player.entity;
    }


    public void Scene2()
    {
        prebatlle = SceneManager.GetActiveScene().name;
        if (nextscene == "")
        {
            SceneManager.LoadScene("batalha");
            tempstm = PlayerController.tempstm;
            player.entity.recoverystm = tempstm;
        }
        else
        {

            player.entity.recoverystm = PlayerController.tempstm;
            Player.fromscene = true;
            Player.nextInventory = player.inventory;
            Player.getstatus = player.entity;
            SceneManager.LoadScene(nextscene);
            Destroy(player.gameObject);
            Destroy(monster.gameObject);

        }

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
        gameManager = FindObjectOfType(typeof(GameManager)) as GameManager;
        if (monster == null && SceneManager.GetActiveScene().name != "GameOver")
        {
            Destroy(this.gameObject);
        }
        if (SceneManager.GetActiveScene().name == prebatlle)
            Destroy(this.gameObject);
        if (monster.entity.incombat == true && monster.gambiarra == 0)
        {
            monster.gambiarra = 1;
            player.entity.wasincombat = 1;
            Player.nextInventory = player.inventory;
            player.gameObject.SetActive(false);
            monster.gameObject.SetActive(false);
            Destroy(gameManager.gameObject);
            Scene2();
        }
        monster.entity = monsterE;
        if (monster.entity.incombat == false && monster.gambiarra == 1 && monster.entity.dead == true && Notrespawn.runned != true)
        {
            monster.gambiarra = 0;
            CenaryChanger.DefeatedMonsters[i] = monstername;
            i++;
            Player.monsterkill++;
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
            Destroy(monster.gameObject);
            Destroy(player.gameObject);
            Destroy(GameObject.FindWithTag("SceneChanger"));
        }
        if (monster.entity.incombat == false && monster.gambiarra == 1 && monster.entity.dead == false && Notrespawn.runned == true)
        {
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
            Destroy(this.gameObject);
            Destroy(player.gameObject);
            Destroy(monster.gameObject);
        }


        if (monster.entity.incombat == false && isdead.dead == true)
        {
            GameOver();
            isdead.dead = false;
        }



    }
    public static void Scene1()
    {

        monsterE = null;
        SceneManager.LoadScene(prebatlle + "Dream");
        prebatlle = "";
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