using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Notrespawn : MonoBehaviour
{
    public bool isdead = false; //monsters 
    public bool isdeadp = false; //player
    public static bool runned = false;
    public int temp = 0;
    public int gambiarra = 0;
    public string cena;
    public SceneChange sceneChange;
    public SceneChangeCar sceneChangeCar;
    public Boss1Scene boss1scene;
    public GameObject[] scene;
    void Start()
    {
        isdead = false;
        isdeadp = false;
        runned = false;
        temp = 0;
        gambiarra = 0;
    }

    // Update is called once per frame
    void Update()
    {
        scene = GameObject.FindGameObjectsWithTag("SceneChanger");
        if (isdead == true)
        {
            Destroy(GameObject.FindWithTag("Monster"));
            Player.getstatus = Soul.playerentity;
            Destroy(GameObject.FindWithTag("SceneChanger"));
            if (Player.buffed == 0 && Player.buff != "")
            {

            }
            temp++;
            if (temp == 5)
                Destroy(this.gameObject);
        }
        if (isdeadp == true)
        {
            cena = SceneManager.GetActiveScene().name;
            if (gambiarra == 0)
            {
                Destroy(GameObject.FindWithTag("Monster"));
                Destroy(GameObject.FindWithTag("Player"));
                gambiarra++;
            }
            if (cena != "GameOver")
            {
                scenechanger();
                Monster.getstatus = BattleEnemy.enemyentity;
                Player.getstatus = Soul.playerentity;
                temp++;
                if (temp == 5)
                    Destroy(this.gameObject);
            }
        }
        if (!isdead && !isdeadp && runned == true)
        {
            if (scene != null && temp == 0)
            {
                scenechanger();
                Destroy(scene[0].gameObject);
            }
            Player.getstatus = Soul.playerentity;
            Monster.getstatus = BattleEnemy.enemyentity;
            temp++;
            if (temp == 10)
                Destroy(this.gameObject);
        }
    }
    public void scenechanger()
    {
        sceneChange = scene[0].GetComponent<SceneChange>();
        if (sceneChange != null)
            SceneChange.monsterE = BattleEnemy.enemyentity;
        if (sceneChange == null)
        {
            sceneChangeCar = scene[0].GetComponent<SceneChangeCar>();
            if (sceneChangeCar != null)
                SceneChangeCar.monsterE = BattleEnemy.enemyentity;

            if (sceneChangeCar == null)
            {
                boss1scene = scene[0].GetComponent<Boss1Scene>();
                Boss1Scene.monsterE = BattleEnemy.enemyentity;

            }
        }
    }
}
