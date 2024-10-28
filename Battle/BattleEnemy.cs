using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BattleEnemy : MonoBehaviour
{
    public Entity entity;
    public static Entity enemyentity;
    public static int lvltemp;
    public SpriteRenderer spriteRenderer;
    public AudioClip clip;

    [Header("temporarias")]
    public int mtemplvl;
    public int mtempwill;
    public int mtempresist;
    public int mtempstm;
    public int mtempfurt;
    public float mtempper;
    public int mtempxp;
    public float mtempmaxh;
    public float mtempmaxf;
    public int mtempmaxs;
    void Start()
    {

        entity = SceneChangeCar.monsterE;
        if (entity == null)
        {
            entity = SceneChange.monsterE;
            if (entity == null)
            {
                entity = Boss1Scene.monsterE;
                clip = GameObject.FindWithTag("SceneChanger").GetComponent<Boss1Scene>().clip;
            }
        }
        entity.incombat = true;
        lvltemp = entity.lvl;
        mtempmaxh = entity.maxhealth;
        mtempmaxf = entity.maxstealth;
        mtempmaxs = entity.maxstamina;
        mtemplvl = entity.lvl;
        mtempwill = entity.will;
        mtempresist = entity.resistence;

        mtempstm = entity.stamina;
        mtempfurt = entity.furt;
        mtempper = entity.perseption;
        mtempxp = entity.xp;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 2; i++)
            spriteRenderer.sprite = entity.sprite;
        SceneChange.monsterE = entity;
        if (entity.currenthealth > entity.maxhealth)
        {
            entity.currenthealth = entity.maxhealth;
        }
    }
}
