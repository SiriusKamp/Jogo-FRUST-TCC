using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Boss : MonoBehaviour
{

    public static Entity getstatus;

    [Header("Controller")]
    public Entity entity = new Entity();
    public GameManager manager;


    // public int dmgmonsters;
    // public int defmonsters;


    private void Start()
    {
        manager = FindObjectOfType(typeof(GameManager)) as GameManager;
        getstatus = entity;

        entity.lvl = manager.CalculateLevel(entity);
        entity.maxhealth = manager.CalculateHealth(entity);
        entity.maxstamina = manager.Calculatestamina(entity);

        entity.currenthealth = entity.maxhealth;
        entity.currentstamina = entity.maxstamina;





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




    }
    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            entity.incombat = true;
            entity.target = collider.gameObject;
            entity.target.GetComponent<CapsuleCollider2D>().isTrigger = true;
        }
    }

}
