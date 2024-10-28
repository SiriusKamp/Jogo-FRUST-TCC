using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Functions : MonoBehaviour
{
    public Texto texto;
    public void TurnTriggerOFTexto()
    {
        texto.trigger = false;
    }
    public void turnon()
    {
        this.gameObject.SetActive(true);
    }

    public void turnoff()
    {
        this.gameObject.SetActive(false);
    }
    public void Destroy()
    {
        Destroy(this.gameObject);
    }
    public void quit()
    {
        Application.Quit();
    }
}
