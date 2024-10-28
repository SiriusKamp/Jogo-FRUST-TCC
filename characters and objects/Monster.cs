using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Player))]
[RequireComponent(typeof(Animator))]
public class Monster : MonoBehaviour
{
    public Player player;
    public static Entity getstatus;

    [Header("Controller")]
    public Entity entity = new Entity();
    public GameManager manager;
    [Header("Patrol")]
    public bool chaser = false;
    public Transform[] waypointList;
    public float arrivalDistance = 0.5f;
    public float waitTime = 5;

    // Privates
    Transform targetWapoint;
    public int currentWaypoint = 0;
    float lastDistanceToTarget = 0f;
    float currentWaitTime = 0f;
    Rigidbody2D rb2D;
    public Animator animator;
    // public int dmgmonsters;
    // public int defmonsters;
    public int gambiarra = 0;


    private void Start()
    {
        manager = FindObjectOfType(typeof(GameManager)) as GameManager;
        getstatus = entity;

        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = FindObjectOfType(typeof(Player)) as Player;

        entity.lvl = manager.CalculateLevel(entity);
        entity.maxhealth = manager.CalculateHealth(entity);
        entity.maxstamina = manager.Calculatestamina(entity);

        entity.currenthealth = entity.maxhealth;
        entity.currentstamina = entity.maxstamina;



        currentWaitTime = waitTime;
        if (waypointList.Length > 0)
        {
            targetWapoint = waypointList[currentWaypoint];
            lastDistanceToTarget = Vector2.Distance(transform.position, targetWapoint.position);
        }

        if (chaser)
            entity.perseption = player.entity.maxstealth - 10;
    }

    private void Update()
    {

        if (getstatus.wasincombat == 1)
        {
            entity = getstatus;
            entity.wasincombat = 0;
        }
        //  dmgmonsters = manager.Calculatedamage(entity);
        // defmonsters = manager.Calculatedefense(entity);


        if (entity.currenthealth <= 0)
        {
            entity.currenthealth = 0;
            Die();
        }

        if (!entity.incombat)
        {
            if (waypointList.Length > 0)
            {
                Patrol();
            }
            else
            {
                animator.SetBool("isWalking", false);
            }
        }
        if (currentWaypoint >= waypointList.Length - 1 && entity.perseption < player.entity.currentstealth)
        {
            currentWaypoint = 0;
            currentWaitTime = 0;
            arrivalDistance = 1.0f;


        }
    }
    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.tag == "Player" && !entity.dead)
        {
            entity.incombat = true;
            entity.target = collider.gameObject;
            entity.target.GetComponent<CapsuleCollider2D>().isTrigger = true;
        }
    }


    void Patrol()
    {
        if (entity.dead)
            return;

        // calcular a distance do waypoint
        float distanceToTarget = Vector2.Distance(transform.position, targetWapoint.position);

        if (distanceToTarget <= arrivalDistance || distanceToTarget >= lastDistanceToTarget)
        {

            if (currentWaitTime <= 0)
            {
                currentWaypoint++;
                if (currentWaypoint >= waypointList.Length - 1)
                {
                    currentWaypoint = 0;
                }
                if (entity.perseption > player.entity.currentstealth)
                    currentWaypoint = waypointList.Length - 1;
                targetWapoint = waypointList[currentWaypoint];
                lastDistanceToTarget = Vector2.Distance(transform.position, targetWapoint.position);

                currentWaitTime = waitTime;

            }
            else
            {
                currentWaitTime -= Time.deltaTime;
            }
        }
        else
        {
            animator.SetBool("isWalking", true);
            lastDistanceToTarget = distanceToTarget;
        }

        Vector2 direction = (targetWapoint.position - transform.position).normalized;
        animator.SetFloat("input_x", direction.x);
        animator.SetFloat("input_y", direction.y);
        rb2D.MovePosition(Vector2.MoveTowards(transform.position, targetWapoint.position, entity.speed * Time.deltaTime));
        // transform.position = Vector2.MoveTowards(transform.position, targetWapoint.position, entity.speed * Time.deltaTime);
    }
    public void Die()
    {
        entity.dead = true;
        entity.incombat = false;
        entity.target = null;

        animator.SetBool("isWalking", false);

        // add exp no player
        //manager.GainExp(rewardExperience);

        Debug.Log("O inimigo morreu: " + entity.name);

    }
}
