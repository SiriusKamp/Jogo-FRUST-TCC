using System.Threading;
using System.Runtime.Serialization;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum BattleState
{
    Start,
    PlayerTurn,
    EnemyTurn,
    FinishedTurn,
    Lost,
    Dialogue,
}
public class BattleManager : MonoBehaviour
{

    int i = 0;

    public Text playerlvl;
    public Text monsterlvl;
    public Image MonsterLife;
    public BattleEnemy enemy;
    public Soul soul;
    public GameObject soulUI;
    public GameManager manager;

    [Header("temporarias")]
    public int tempxp;
    public int tempxpE;
    System.Random rnd = new System.Random();
    public float currenttempheatlh;
    public float currenttempheatlhmonster;

    [Header("Dialogo")]
    public BattleTexts battletext;

    [Header("AUDIO")]
    public AudioSource MusicSource;
    public AudioSource audioSource;
    public AudioClip Run;
    public AudioClip FCS;
    public AudioClip Hit;
    public AudioClip GetHit;

    // PARTE DO VIDEO
    public EnemyProfile[] EnemiesInBattle;
    public BattleState state;
    //inimigo
    private bool enemyActed;
    private GameObject[] EnemyAtks;
    public DeusDialogue Dialogue;
    //player 
    public GameObject PlayerUi1;
    public GameObject PlayerUi2;
    public GameObject PlayerUi3;

    public SoulControl PlayerSoul;
    public Notrespawn notrespawn;

    public float invencible;
    public SceneChange sceneChange;
    public SceneChangeCar sceneChangeCar;
    public Boss1Scene boss1scene;
    public GameObject[] scene;
    public float cd;
    public GameObject demonstration;

    // Start is called before the first frame update
    void Start()
    {
        scene = GameObject.FindGameObjectsWithTag("SceneChanger");
        MusicSource = GameObject.FindWithTag("Game Music").GetComponent<AudioSource>();


        currenttempheatlh = soul.entity.currenthealth;
        currenttempheatlhmonster = enemy.entity.currenthealth;
        soul.entity.xp = 0;
        enemy.entity.xp = 0;

        //PARTE DO VIDEO
        state = BattleState.Dialogue;
        cd = Time.time + 1.1f;
        enemyActed = false;
        PlayerUi1.SetActive(false);
        PlayerUi2.SetActive(false);
        PlayerUi3.SetActive(false);

    }
    public void ResetPlayer()
    {
        soul.entity.lvl = soul.ptemplvl;
        soul.entity.will = soul.ptempwill;
        soul.entity.resistence = soul.ptempresist;
        soul.entity.stamina = soul.ptempstm;
        soul.entity.furt = soul.ptempfurt;
        soul.entity.perseption = soul.ptempper;
        soul.entity.xp = soul.ptempxp;
        soul.entity.maxhealth = soul.ptempmaxh;
        soul.entity.maxstamina = soul.ptempmaxs;
        soul.entity.maxstealth = soul.ptempmaxf;
    }
    public void ResetEnemy()
    {
        enemy.entity.lvl = enemy.mtemplvl;
        enemy.entity.will = enemy.mtempwill;
        enemy.entity.resistence = enemy.mtempresist;
        enemy.entity.stamina = enemy.mtempstm;
        enemy.entity.furt = enemy.mtempfurt;
        enemy.entity.perseption = enemy.mtempper;
        enemy.entity.xp = enemy.mtempxp;
        enemy.entity.maxhealth = enemy.mtempmaxh;
        enemy.entity.maxstamina = enemy.mtempmaxs;
        enemy.entity.maxstealth = enemy.mtempmaxf;
    }
    public void TakeDamage()
    {
        if (Time.time > invencible)
        {
            audioSource.PlayOneShot(GetHit);
            int mdmg = manager.Calculatedamage(enemy.entity);
            int pdfs = manager.Calculatedefense(soul.entity);
            if (mdmg > pdfs)
            {
                invencible = Time.time + 0.7f;
                soul.entity.currenthealth += mdmg - pdfs;
                if (soul.entity.currenthealth > soul.entity.maxhealth / 3 * 2 && currenttempheatlh < soul.entity.maxhealth / 3 * 2)
                {
                    soul.entity.xp -= soul.entity.lvl * 100;
                    battletext.dialogueIndex = 1;
                    manager.CalculateLevel(soul.entity);
                    soul.entity.xp = 0;
                    playerlvl.text = "LVL" + soul.entity.lvl;
                }
                if (soul.entity.currenthealth >= soul.entity.maxhealth)
                {
                    float resultm;
                    resultm = enemy.entity.currenthealth / enemy.entity.maxhealth;
                    ResetPlayer();
                    ResetEnemy();
                    enemy.entity.currenthealth = enemy.entity.maxhealth * resultm;
                    soul.entity.currenthealth = 0;
                    soul.entity.dead = true;
                    enemy.entity.xp = enemy.entity.lvl * 100;
                    manager.CalculateLevel(enemy.entity);
                    enemy.entity.xp = enemy.entity.lvl * 100;
                    manager.CalculateLevel(enemy.entity);
                    sceneChange = scene[0].GetComponent<SceneChange>();
                    if (sceneChange != null)
                        sceneChange.isdead.dead = true;
                    if (sceneChange == null)
                    {
                        sceneChangeCar = scene[0].GetComponent<SceneChangeCar>();
                        if (sceneChangeCar != null)
                            sceneChangeCar.isdead.dead = true;
                        if (sceneChangeCar == null)
                        {
                            boss1scene = scene[0].GetComponent<Boss1Scene>();
                            boss1scene.isdead.dead = true;
                        }
                    }

                    soul.entity.wasincombat = 1;
                    notrespawn.isdeadp = true;
                    enemy.entity.incombat = false;
                    enemy.entity.wasincombat = 1;
                    BattleEnemy.enemyentity = enemy.entity;

                }
                currenttempheatlh = soul.entity.currenthealth;
            }
            else
            {
                invencible = Time.time + 0.7f;
                enemy.entity.xp += enemy.entity.lvl * 100;
                manager.CalculateLevel(enemy.entity);
                enemy.entity.xp = 0;
                monsterlvl.text = "LVL" + enemy.entity.lvl;
                battletext.dialogueIndex = 7;
            }
        }
    }
    public void DealDamage()
    {
        int pdmg = manager.Calculatedamage(soul.entity);
        int mdfs = manager.Calculatedefense(enemy.entity);
        audioSource.PlayOneShot(Hit);
        if (pdmg > mdfs)
        {
            enemy.entity.currenthealth -= pdmg - mdfs;
            MonsterLife.fillAmount = enemy.entity.currenthealth / enemy.entity.maxhealth;
            if (enemy.entity.currenthealth < enemy.entity.maxhealth / 3 && currenttempheatlhmonster > enemy.entity.maxhealth / 3)
            {
                battletext.dialogueIndex = 2;
                soul.entity.xp += soul.entity.lvl * 100;
                manager.CalculateLevel(soul.entity);
                soul.entity.xp = 0;
                playerlvl.text = "LVL" + soul.entity.lvl;
            }
            if (enemy.entity.currenthealth <= 0)
            {
                float resultp;
                resultp = soul.entity.currenthealth / soul.entity.maxhealth;

                ResetPlayer();
                enemy.entity.lvl = enemy.mtemplvl;
                soul.entity.currenthealth = soul.entity.maxhealth * resultp;

                soul.entity.xp += (int)Math.Round(enemy.entity.lvl * 10 * soul.entity.expmod);
                manager.CalculateLevel(soul.entity);
                if (soul.entity.lvl > soul.ptemplvl)
                    soul.entity.wasup = true;
                enemy.entity.currenthealth = 0;
                soul.entity.coins += enemy.entity.lvl * 80;
                enemy.entity.dead = true;
                soul.entity.wasincombat = 1;
                enemy.entity.wasincombat = 1;
                enemy.entity.incombat = false;
                notrespawn.isdead = true;
            }
        }
        else
        {
            enemy.entity.xp += enemy.entity.lvl * 100;
            manager.CalculateLevel(enemy.entity);
            enemy.entity.xp = 0;
            monsterlvl.text = "LVL" + enemy.entity.lvl;
            battletext.dialogueIndex = 3;
        }
    }
    public void Escape()
    {
        if (soul.entity.stamina * rnd.Next(1, 30) > enemy.entity.perseption * 3)
        {
            float resultp;
            resultp = soul.entity.currenthealth / soul.entity.maxhealth;
            float resultm;
            resultm = enemy.entity.currenthealth / enemy.entity.maxhealth;
            audioSource.PlayOneShot(Run);
            ResetEnemy();
            ResetPlayer();

            soul.entity.currenthealth = soul.entity.maxhealth * resultp;
            enemy.entity.currenthealth = enemy.entity.maxhealth * resultm;

            enemy.entity.xp = enemy.entity.lvl * 100;
            manager.CalculateLevel(enemy.entity);
            enemy.entity.wasincombat = 1;
            Notrespawn.runned = true;
            enemy.entity.incombat = false;
            BattleEnemy.enemyentity = enemy.entity;


        }
        else
        {
            battletext.dialogueIndex = 4;
        }
    }
    public void Focus()
    {
        audioSource.PlayOneShot(FCS);
        soul.entity.xp += soul.entity.lvl * 100;
        manager.CalculateLevel(soul.entity);
        soul.entity.xp = 0;
        playerlvl.text = "LVL" + soul.entity.lvl;
        if (enemy.entity.lvl == 1)
            battletext.dialogueIndex = 5;
        else
        {
            enemy.entity.xp -= enemy.entity.lvl * 100;
            manager.CalculateLevel(enemy.entity);
            enemy.entity.xp = 0;
            monsterlvl.text = "LVL" + enemy.entity.lvl;

            battletext.dialogueIndex = 6;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (enemy.clip != null)
        {
            MusicSource.Stop();
            MusicSource.PlayOneShot(enemy.clip);
            MusicSource.loop = true;
            MusicSource.GetComponent<AudioSRC>().thissong = 100;
            enemy.clip = null;
        }
        if (enemy.entity.incombat == true && i == 0)
        {
            EnemiesInBattle[0] = enemy.entity.atacks;
            enemy.entity.resistence += (enemy.entity.will / (enemy.entity.lvl * 2)) * soul.entity.willadd;
            enemy.entity.will += (enemy.entity.resistence / (enemy.entity.lvl * 2)) * soul.entity.resadd;
            enemy.entity.perseption += (enemy.entity.perseption / (enemy.entity.lvl * 2)) * soul.entity.stmadd;
            MonsterLife.fillAmount = enemy.entity.currenthealth / enemy.entity.maxhealth;
            battletext.dialogueNpc = enemy.entity.BattleTexts;
            i++;
        }

        if (state == BattleState.Start)
        {
            //SETUP THE LEVEL 
            battletext.trigger = false;
            battletext.startDialogue = false;
            battletext.dialogueIndex = 0;
            state = BattleState.PlayerTurn;
        }
        else if (state == BattleState.PlayerTurn)
        {

            //espera o player fazer a ação
            if (Time.time > cd)
            {
                PlayerUi1.SetActive(true);
                PlayerUi2.SetActive(true);
                PlayerUi3.SetActive(true);
                MonsterLife.gameObject.SetActive(true);
                playerlvl.gameObject.SetActive(true);
                monsterlvl.gameObject.SetActive(true);
                soulUI.SetActive(true);
                if (Dialogue != null)
                {
                    if (Dialogue.dialoguePanel.activeSelf == true)
                    {
                        soul.gameObject.SetActive(true);
                        PlayerUi1.SetActive(false);
                        PlayerUi2.SetActive(false);
                        PlayerUi3.SetActive(false);
                        MonsterLife.gameObject.SetActive(false);
                        playerlvl.gameObject.SetActive(false);
                        monsterlvl.gameObject.SetActive(false);
                        soulUI.SetActive(false);
                        switch (Dialogue.dialogueIndex)
                        {
                            case 0:
                                demonstration.SetActive(false);
                                break;
                            case 1:
                                break;
                            case 2:
                                MonsterLife.gameObject.SetActive(true);
                                soulUI.SetActive(true);
                                break;
                            case 3:
                                MonsterLife.gameObject.SetActive(true);
                                soulUI.SetActive(true);
                                MonsterLife.gameObject.SetActive(true);
                                soulUI.SetActive(true);
                                break;
                            case 4:
                                MonsterLife.gameObject.SetActive(true);
                                soulUI.SetActive(true);
                                MonsterLife.gameObject.SetActive(true);
                                soulUI.SetActive(true);
                                playerlvl.gameObject.SetActive(true);
                                monsterlvl.gameObject.SetActive(true);
                                break;
                            case 5:
                                MonsterLife.gameObject.SetActive(true);
                                soulUI.SetActive(true);
                                MonsterLife.gameObject.SetActive(true);
                                soulUI.SetActive(true);
                                playerlvl.gameObject.SetActive(true);
                                monsterlvl.gameObject.SetActive(true);
                                break;
                            case 6:
                                MonsterLife.gameObject.SetActive(true);
                                soulUI.SetActive(true);
                                MonsterLife.gameObject.SetActive(true);
                                soulUI.SetActive(true);
                                playerlvl.gameObject.SetActive(true);
                                monsterlvl.gameObject.SetActive(true);
                                PlayerUi1.SetActive(true);
                                break;
                            case 7:
                                MonsterLife.gameObject.SetActive(true);
                                soulUI.SetActive(true);
                                MonsterLife.gameObject.SetActive(true);
                                soulUI.SetActive(true);
                                playerlvl.gameObject.SetActive(true);
                                monsterlvl.gameObject.SetActive(true);
                                PlayerUi1.SetActive(true);
                                PlayerUi2.SetActive(true);
                                break;
                            case 8:
                                MonsterLife.gameObject.SetActive(true);
                                soulUI.SetActive(true);
                                MonsterLife.gameObject.SetActive(true);
                                soulUI.SetActive(true);
                                playerlvl.gameObject.SetActive(true);
                                monsterlvl.gameObject.SetActive(true);
                                PlayerUi1.SetActive(true);
                                PlayerUi2.SetActive(true);
                                PlayerUi3.SetActive(true);
                                break;
                            case 9:
                                MonsterLife.gameObject.SetActive(true);
                                soulUI.SetActive(true);
                                MonsterLife.gameObject.SetActive(true);
                                soulUI.SetActive(true);
                                playerlvl.gameObject.SetActive(true);
                                monsterlvl.gameObject.SetActive(true);
                                PlayerUi1.SetActive(true);
                                PlayerUi2.SetActive(true);
                                PlayerUi3.SetActive(true);
                                break;
                            case 10:
                                MonsterLife.gameObject.SetActive(true);
                                soulUI.SetActive(true);
                                MonsterLife.gameObject.SetActive(true);
                                soulUI.SetActive(true);
                                playerlvl.gameObject.SetActive(true);
                                monsterlvl.gameObject.SetActive(true);
                                PlayerUi1.SetActive(true);
                                PlayerUi2.SetActive(true);
                                PlayerUi3.SetActive(true);
                                break;
                            case 11:
                                demonstration.SetActive(true);
                                break;
                        }

                    }
                    else
                        soul.gameObject.SetActive(false);
                }
            }
        }
        else if (state == BattleState.EnemyTurn)
        {

            playerlvl.gameObject.SetActive(false);
            monsterlvl.gameObject.SetActive(false);
            if (EnemiesInBattle.Length <= 0)
            {
                EnemyFinishedTurn();
            }
            else
            {
                if (!enemyActed)
                {

                    //turn on the player soul
                    soul.gameObject.SetActive(true);
                    soul.SetSoul();

                    //atacks
                    foreach (EnemyProfile emy in EnemiesInBattle)
                    {


                        int AtkNumb = UnityEngine.Random.Range(0, emy.EnemiesAttacks.Length);

                        Instantiate(emy.EnemiesAttacks[AtkNumb]);
                    }
                    EnemyAtks = GameObject.FindGameObjectsWithTag("Enemy");
                    enemyActed = true;
                }
                else
                {
                    bool enemyfin = true;
                    foreach (GameObject emy in EnemyAtks)
                    {

                        if (emy.GetComponent<EnemyTurnHandle>().FinishedTurn == false)
                        {
                            enemyfin = false;
                        }
                    }
                    if (enemyfin)
                    {
                        EnemyFinishedTurn();
                    }
                }
            }
            //enemy take turn
        }

        else if (state == BattleState.FinishedTurn)
        {
            soul.gameObject.SetActive(false);

            if (soul.entity.currenthealth > soul.entity.maxhealth)
                state = BattleState.Lost;
            else
                state = BattleState.Dialogue;
        }
        else if (state == BattleState.Dialogue)
        {
            if (Dialogue != null)
                Dialogue.timetostart = Time.time + 00.1f;
            soul.gameObject.SetActive(false);
            soulUI.SetActive(false);
            MonsterLife.gameObject.SetActive(false);
            playerlvl.text = "LVL" + soul.entity.lvl;
            monsterlvl.text = "LVL" + enemy.entity.lvl;
            playerlvl.gameObject.SetActive(false);
            monsterlvl.gameObject.SetActive(false);
            battletext.trigger = true;
            if (!battletext.startDialogue)
                battletext.StartDialogue();
            if (!battletext.dialoguePanel.gameObject.active)
                state = BattleState.Start;
        }

    }
    public void PlayerAtk()
    {
        DealDamage();
        playerfinishTurn();
    }
    public void PlayerFcs()
    {
        Focus();
        playerfinishTurn();
    }
    public void PlayerEsc()
    {
        Escape();
        playerfinishTurn();
    }
    void playerfinishTurn()
    {
        PlayerUi1.SetActive(false);
        PlayerUi2.SetActive(false);
        PlayerUi3.SetActive(false);
        MonsterLife.gameObject.SetActive(false);
        state = BattleState.EnemyTurn;
        soul.dmgTaked = 0;
    }
    void EnemyFinishedTurn()
    {
        foreach (GameObject obj in EnemyAtks)
        {
            Destroy(obj);
        }
        enemyActed = false;
        state = BattleState.FinishedTurn;
        if (soul.dmgTaked == 0)
        {
            UnityEngine.Debug.Log("ERA PRA UPAR");
            soul.entity.xp = soul.entity.lvl * 100;
            manager.CalculateLevel(soul.entity);
        }
    }
}
