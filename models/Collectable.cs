using System.Threading;
using System.Security.Cryptography;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Item))]
public class Collectable : MonoBehaviour
{
    public AudioSource Source;
    public AudioClip clip;
    public float cd = 0;
    public void Start()
    {
        Source = GameObject.FindWithTag("AudioSource").GetComponent<AudioSource>();
    }
    private void OnTriggerStay2D(Collider2D colision)
    {
        if (Input.GetKeyDown(KeyCode.E))
            Source.PlayOneShot(clip);
        if (Input.GetKeyDown(KeyCode.E) && cd < Time.time)
        {
            Item item = GetComponent<Item>();
            Player player = colision.GetComponent<Player>();
            player.inventory.Add(item);
            cd = Time.time + 1;
            Destroy(this.gameObject);
        }

    }
}