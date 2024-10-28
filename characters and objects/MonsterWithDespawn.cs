using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MonsterWithDespawn : MonoBehaviour
{
    public static Entity getstatus;

    [Header("Controller")]
    public Entity entity = new Entity();
    public GameManager manager;
    [Header("Patrol")]
    public float arrivalDistance = 0.5f;
    public float waitTime = 5;

    // Privates
    Transform targetWapoint;
    public int currentWaypoint = 0;
    float lastDistanceToTarget = 0f;
    float currentWaitTime = 0f;

    [Header("Experience Reward")]
    public int rewardExperience = 10;
    public int lootGold = 10;
    Rigidbody2D rb2D;
    public Animator animator;
    // public int dmgmonsters;
    // public int defmonsters;


    private void Start()
    {
        manager = FindObjectOfType(typeof(GameManager)) as GameManager;
        getstatus = entity;
        targetWapoint = (GameObject.FindWithTag("Waypoint")).transform;

        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        entity.lvl = manager.CalculateLevel(entity);
        entity.maxhealth = manager.CalculateHealth(entity);
        entity.maxstamina = manager.Calculatestamina(entity);

        entity.currenthealth = entity.maxhealth;
        entity.currentstamina = entity.maxstamina;



        currentWaitTime = waitTime;
        lastDistanceToTarget = Vector2.Distance(transform.position, targetWapoint.position);


    }

    private void Update()
    {
        manager = FindObjectOfType(typeof(GameManager)) as GameManager;

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
            Patrol();
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
                Destroy(this.gameObject);
            else
                currentWaitTime -= Time.deltaTime;
        }
        else
        {
            animator.SetBool("isWalking", true);
            lastDistanceToTarget = distanceToTarget;
        }

        Vector2 direction = (targetWapoint.position - transform.position).normalized;
        animator.SetFloat("input_x", direction.x);
        animator.SetFloat("input_y", direction.y);

        rb2D.MovePosition(rb2D.position + direction * (entity.speed * Time.fixedDeltaTime));
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
