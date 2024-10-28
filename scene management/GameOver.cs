using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    public SceneChange sceneChange;
    public SceneChangeCar sceneChangeCar;
    public Boss1Scene boss1scene;
    public GameObject[] scene;
    void Start()
    {
        scene = GameObject.FindGameObjectsWithTag("SceneChanger");

    }
    void Update()
    {

    }
    public void continuar()
    {
        Debug.Log("clicou");
        sceneChange = scene[0].GetComponent<SceneChange>();
        if (sceneChange != null)
            SceneChange.Continue();
        if (sceneChange == null)
        {
            sceneChangeCar = scene[0].GetComponent<SceneChangeCar>();
            if (sceneChangeCar != null)
                SceneChangeCar.Continue();

            if (sceneChangeCar == null)
            {
                boss1scene = scene[0].GetComponent<Boss1Scene>();
                Boss1Scene.Continue();
            }
        }
    }
    public void desistir()
    {
        SceneManager.LoadScene("suicidio");
    }
}
