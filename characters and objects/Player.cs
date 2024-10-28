using System.Runtime.CompilerServices;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(Animator))]

public class Player : MonoBehaviour
{
    public static int redpill = 0;
    public static bool tutorial = true;
    public static int buffed;
    public static string buff;
    public static int monsterkill = 0;
    public Animator animator;
    public bool canrun = true;
    public PlayerController isWalking;
    public Inventory inventory;
    public static Inventory nextInventory;
    public GameObject inventorypannel;
    public Player player;
    public int count;
    public static bool fromscene;
    public static Entity entitysoul;
    public Entity entity;
    public static Entity getstatus;
    [Header("Game Manager")]

    public GameManager manager;

    [Header("Player UI")]

    public Slider FST;

    public Slider FURT;

    public Slider STM;

    public Slider EXP;

    public float temp;
    public Text exptext;
    public Text lvltext;
    public Text willTxt;
    public Text resTxt;
    public Text stmTxt;
    public Text furtTxt;
    public Button willPositiveBtn;
    public Button resPositiveBtn;
    public Button stmPositiveBtn;
    public Button furtPositiveBtn;
    public Button willNegativeBtn;
    public Button resNegativeBtn;
    public Button stmNegativeBtn;
    public Button furtNegativeBtn;
    public Text pointsTxt;

    [Header("Player Shortcuts")]
    public KeyCode attributesKey = KeyCode.C;

    [Header("Player UI Panels")]
    public GameObject attributesPanel;

    [Header("exp")]
    public GameObject levelUpFX;
    public AudioClip levelUpSound;
    public AudioSource entityAudio;
    public int givePoints = 2;

    public GameObject keyE;


    void Start()
    {
        keyE.SetActive(false);
        if (fromscene == false)
            inventory = new Inventory(5, 28);
        else if (fromscene == true)
        {
            inventory = nextInventory;
            entity = getstatus;
        }
        getstatus = entity;

        manager = FindObjectOfType(typeof(GameManager)) as GameManager;

        animator = GetComponent<Animator>();
        isWalking = GetComponent<PlayerController>();
        player = GetComponent<Player>();
        if (manager == null)
        {
            Debug.LogError("Você precisa anexar o game manager aqui no player");
            return;
        }
        entity.lvl = manager.CalculateLevel(entity);
        UpdateStatus();

        entity.currentstamina = entity.maxstamina;
        entity.currentstealth = entity.maxstealth / 2;



        temp = entity.maxstealth / 2;
        entitysoul = entity;
        entityAudio = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
        exptext.text = String.Format("Exp: {0}/{1}", entity.xp, entity.lvl * 100);
        lvltext.text = entity.lvl.ToString();
        UpdatePoints();
        SetupUIButtons();
        count = 0;
        exptext.text = String.Format("Exp: {0}/{1}", entity.xp, entity.lvl * 100);
        lvltext.text = entity.lvl.ToString();
        FST.value = entity.currenthealth;
        EXP.value = entity.xp;
        UpdatePoints();
        fromscene = false;
    }
    private void Update()
    {

        if (entity.wasincombat == 1)
        {
            if (entity.wasup == true)
            {
                entityAudio.PlayOneShot(levelUpSound);
                Instantiate(levelUpFX, this.gameObject.transform);

                entity.wasup = false;
                if (entity.wasadd < entity.lvl)
                {
                    entity.points += givePoints;
                    UpdatePoints();
                    entity.wasadd++;
                }
            }
            UpdateStatus();
            entity.wasincombat = 0;
            getstatus = entity;
        }
        FURT.value = entity.currentstealth;
        STM.value = entity.currentstamina;

        if (isWalking.isWalking == true && entity.speed == 2.5f && entity.currentstealth != entity.maxstealth)
            entity.currentstealth = temp / 2;
        else if (entity.speed == entity.correndo && entity.currentstealth != entity.maxstealth)
            entity.currentstealth = 0;
        else if (isWalking.isWalking == false && entity.currentstealth != entity.maxstealth)
            entity.currentstealth = entity.maxstealth / 2;


        if (Input.GetKeyDown(attributesKey))
            attributesPanel.SetActive(!attributesPanel.activeSelf);
        if (Input.GetKeyDown(KeyCode.I))
            inventorypannel.SetActive(!inventorypannel.activeSelf);
        if (entity.currentstamina < entity.maxstamina)
        {
            entity.currentstamina += entity.recoverystm;
        }
        if (entity.currentstamina >= entity.maxstamina * 0.5f)
            canrun = true;
        if (entity.currentstamina >= entity.maxstamina)
        {
            entity.currentstamina = entity.maxstamina;
        }

    }
    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.tag == "Bush")
        {
            entity.currentstealth = entity.maxstealth;
            animator.SetBool("isWalking", false);

        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Bush")
        {

            entity.currentstealth = entity.maxstealth / 2;
        }
    }
    public void UpdatePoints()
    {
        willTxt.text = entity.will.ToString();
        resTxt.text = entity.resistence.ToString();
        stmTxt.text = entity.stamina.ToString();
        furtTxt.text = entity.furt.ToString();
        pointsTxt.text = entity.points.ToString();
    }

    public void SetupUIButtons()
    {
        willPositiveBtn.onClick.AddListener(() => AddPoints(1));
        resPositiveBtn.onClick.AddListener(() => AddPoints(2));
        stmPositiveBtn.onClick.AddListener(() => AddPoints(3));
        furtPositiveBtn.onClick.AddListener(() => AddPoints(4));

        willNegativeBtn.onClick.AddListener(() => RemovePoints(1));
        resNegativeBtn.onClick.AddListener(() => RemovePoints(2));
        stmNegativeBtn.onClick.AddListener(() => RemovePoints(3));
        furtNegativeBtn.onClick.AddListener(() => RemovePoints(4));
    }

    public void AddPoints(int index)
    {
        if (entity.points > 0)
        {
            float lvlmod;
            lvlmod = entity.lvl * 1.5f;
            int intlvlmod;
            intlvlmod = (int)Math.Round(lvlmod);
            if (index == 1)
            {
                entity.will += entity.will / intlvlmod;
                entity.willadd++;
            }
            else if (index == 2)
            {
                entity.resistence += entity.resistence / intlvlmod;
                entity.resadd++;
            }
            else if (index == 3)
            {
                entity.stamina += entity.stamina / intlvlmod;
                entity.stmadd++;
            }
            else if (index == 4)
            {
                entity.furt += entity.furt / intlvlmod;
                entity.furtadd++;
            }

            entity.points--;
            UpdatePoints();
            UpdateStatus();
        }
    }

    public void RemovePoints(int index)
    {
        if (entity.points >= 0)
        {
            if (index == 1 && entity.willadd > 0)
            {
                entity.will -= entity.will / 2;
                entity.willadd--;
                entity.points++;
            }
            else if (index == 2 && entity.resadd > 0)
            {
                entity.resistence -= entity.resistence / 2;
                entity.resadd--;
                entity.points++;
            }
            else if (index == 3 && entity.stmadd > 0)
            {
                entity.stamina -= entity.stamina / 2;
                entity.stmadd--;
                entity.points++;
            }
            else if (index == 4 && entity.furtadd > 0)
            {
                entity.furt -= entity.furt / 2;
                entity.furtadd--;
                entity.points++;
            }
            UpdateStatus();
            UpdatePoints();
        }
    }
    public void UpdateStatus()
    {
        entity.maxhealth = manager.CalculateHealth(entity);
        entity.maxstealth = manager.Calculatefurt(entity);
        entity.maxstamina = manager.Calculatestamina(entity);
        FURT.maxValue = entity.maxstealth;
        FST.maxValue = entity.maxhealth;
        STM.maxValue = entity.maxstamina;
        EXP.maxValue = entity.lvl * 100;
        EXP.value = entity.xp;

    }
    public void CanInteract()
    {
        keyE.transform.position = this.transform.position;
        keyE.transform.position += new Vector3(0, 1.3f, 0);
        keyE.SetActive(true);
    }
    public void Tutorialoff()
    {
        tutorial = false;
    }
}
