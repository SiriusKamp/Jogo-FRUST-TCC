using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PannelInteract : MonoBehaviour
{
    public GameObject pannel;
    public float timeon;
    public bool CanInteract;
    public AudioSource audiosource;
    public AudioClip clip;
    public bool destroyafter;
    void Start()
    {
        pannel.SetActive(false);
    }
    void Update()
    {
        if (pannel.active && timeon < Time.time)
        {
            pannel.SetActive(false);
            if (destroyafter)
                Destroy(this.gameObject);
        }
        else if (pannel.active)
            if (audiosource != null)
                audiosource.PlayOneShot(clip);
    }
    private void OnTriggerStay2D(Collider2D colision)
    {
        if (Input.GetKeyDown(KeyCode.E) && CanInteract && !pannel.active)
        {
            if (audiosource != null)
                audiosource.PlayOneShot(clip);
            pannel.SetActive(true);
            timeon += Time.time;
        }

    }
}
