using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Soul : MonoBehaviour
{
    public BattleManager battlemanager;
    //CONTROLA O STATUS
    public static Entity playerentity;
    public Entity entity;
    private Vector3 Startingpos = Vector3.zero;
    public float speed;
    float input_x = 0;
    float input_y = 0;
    private Vector2 MovePos = Vector2.zero;
    Rigidbody2D rb2D;
    public int dmgTaked = 0;

    [Header("temporarias")]
    public int ptemplvl;
    public int ptempwill;
    public int ptempresist;
    public int ptempstm;
    public int ptempfurt;
    public float ptempper;
    public int ptempxp;
    public float ptempmaxh;
    public float ptempmaxf;
    public int ptempmaxs;

    [Header("Player UI")]

    public Slider FST;


    void Start()
    {
        entity = Player.entitysoul;
        ptempmaxh = entity.maxhealth;
        ptempmaxf = entity.maxstealth;
        ptempmaxs = entity.maxstamina;
        ptemplvl = entity.lvl;
        ptempwill = entity.will;
        ptempresist = entity.resistence;
        ptempstm = entity.stamina;
        ptempfurt = entity.furt;
        ptempper = entity.perseption;
        ptempxp = entity.xp;

        SetSoul();
        rb2D = GetComponent<Rigidbody2D>();

        FST.maxValue = entity.maxhealth;
        FST.value = entity.currenthealth;
    }
    public void SetSoul()
    {
        transform.position = Startingpos;
        MovePos = Startingpos;
    }
    void Update()
    {
        FST.maxValue = entity.maxhealth;
        FST.value = entity.currenthealth;

        input_x = Input.GetAxis("Horizontal");
        input_y = Input.GetAxis("Vertical");

        MovePos = new Vector2(input_x, input_y);
        if (entity.currenthealth > entity.maxhealth)
        {
            entity.currenthealth = entity.maxhealth;
        }
    }
    private void FixedUpdate()
    {
        playerentity = entity;
        rb2D.MovePosition(rb2D.position + MovePos * Time.deltaTime * speed);
    }
    public void gambiarra()
    {
        battlemanager.TakeDamage();
        dmgTaked = 1;
    }
}
