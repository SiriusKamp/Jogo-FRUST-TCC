using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

[Serializable]


public class Entity
{

    [Header("Name")]
    public string name;
    public int lvl;
    public int xp;


    [Header("Frustração")]
    public float currenthealth;
    public float maxhealth;

    [Header("Furtividade")]
    public float currentstealth;
    public float maxstealth;

    [Header("Stamina")]
    public int currentstamina;
    public int maxstamina;
    public int recoverystm;

    [Header("Stats")]
    public float correndo = 4;
    public int will = 1;
    public int resistence = 1;
    public int stamina = 5;
    public int furt = 100;
    public float speed = 2.5f;
    public float perseption = 100.0f;
    public int points = 0;
    public int wasincombat = 0;

    [Header("Equipamentos")]
    public int CapRes = 1;
    public int ColarRes = 1;
    public int AccRes = 1;
    public int GloveRes = 1;
    public int BootsRes = 1;

    [Header("combat")]
    public bool incombat = false;
    public GameObject target;
    public bool dead = false;
    public float expmod;
    public bool wasup;
    public string[] BattleTexts;
    public EnemyProfile atacks;
    public Sprite sprite;
    public int coins;
    [Header("BuffMultiplicators")]
    public float buffdmg = 1f;
    public float buffRes = 1f;
    public float buffFurt = 1f;
    public float buffStm = 1f;
    public float buffHealth = 1f;
    [Header("addcount")]
    public int willadd;
    public int resadd;
    public int stmadd;
    public int furtadd;
    public int wasadd = 1;


}