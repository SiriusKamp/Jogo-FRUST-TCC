using System.Diagnostics.Contracts;
using System.Security;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(IsInteractible))]
public class BossCenary : MonoBehaviour
{
    public Player player;
    public string currentScene;
    public string nextmonster;
    public string nextScene = "";
    public static int count = 0;
    public static Entity tempentity;
    public KeyCode interact = KeyCode.E;
    public GameObject collectable;
    public Vector3 playernextposition = new Vector3(3, 2.5f, 0);
    public AudioSource audioSource;
    public AudioClip clip;
    // Start is called before the first frame update
    void Start()
    {
        audioSource.PlayOneShot(clip);
        player = FindObjectOfType(typeof(Player)) as Player;
        currentScene = SceneManager.GetActiveScene().name;
        if (Boss1Scene.prebatlle == currentScene)
            player.entity = Player.getstatus;
        player.transform.position = CenaryChanger.position;
    }
    // Update is called once per frame
    void Update()
    {
        player = FindObjectOfType(typeof(Player)) as Player;
        collectable = GameObject.FindWithTag("Collectable");
        if (collectable == null)
            GetComponent<IsInteractible>().trigger = false;
        else
            GetComponent<IsInteractible>().trigger = true;
    }
    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.tag == "Player" && Input.GetKeyDown(interact) && collectable != null)
        {
            SceneChange.monsterE = null;
            Boss1Scene.monsterE = null;
            SceneChangeCar.monsterE = null;
            Player.getstatus = player.entity;
            Player.nextInventory = player.inventory;
            Player.fromscene = true;
            CenaryChanger.fromscene = true;
            CenaryChanger.position = playernextposition;
            Destroy(GameObject.FindWithTag("SceneChanger"));
            for (int i = 0; i < Player.monsterkill;)
            {
                if (CenaryChanger.DefeatedMonsters[i] == nextmonster)
                {
                    SceneManager.LoadScene(nextScene + "Dream");
                    Destroy(player.gameObject);
                    Destroy(this.gameObject);
                }
                else if (CenaryChanger.DefeatedMonsters[i] != nextmonster)
                    count++;
                i++;
            }
            if (count == Player.monsterkill)
            {
                SceneManager.LoadScene(nextScene);
                Destroy(player.gameObject);
                Destroy(this.gameObject);
            }
            Destroy(player.gameObject);
            Destroy(this.gameObject);
        }
    }

}
