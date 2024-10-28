using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsInteractible : MonoBehaviour
{
    public bool trigger = true;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (trigger == true && collider.tag == "Player")
            FindObjectOfType<Player>().CanInteract();
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        FindObjectOfType<Player>().keyE.SetActive(false);
    }
}
