using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(Rigidbody2D))]
public class DeusDialogue : MonoBehaviour
{
    public string[] dialogueNpc;
    public int dialogueIndex;
    public float timetostart;
    public GameObject dialoguePanel;
    public Text dialogueText;
    public string name;
    public Text nameNpc;
    public Image imageNpc;
    public Sprite spriteNpc;
    public bool skip = false;
    public bool readyToSpeak;
    public Animator animator;
    public Animator deusanimator;
    public bool startDialogue;
    public bool tutorial;
    public static string[] cenas = new string[10];
    public static int u = 0;
    public Rigidbody2D rb2d;
    public Player player;
    public GameObject monster;
    public CenaryChanger porta;
    public AudioSource audioSource;
    public AudioClip TextSound;
    public AudioClip openDialogue;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = this.GetComponent<Rigidbody2D>();
        dialoguePanel.SetActive(false);
        timetostart = Time.time + timetostart;
        for (int i = 0; i < cenas.Length; i++)
        {
            if (SceneManager.GetActiveScene().name == cenas[i])
            {
                readyToSpeak = false;
                Destroy(this);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (readyToSpeak && Time.time >= timetostart && Player.tutorial)
        {
            if (!startDialogue)
            {

                StartDialogue();
                if (tutorial)
                    cenas[u] = SceneManager.GetActiveScene().name;
                u++;
            }
            else if (dialogueText.text == dialogueNpc[dialogueIndex] && Input.GetKeyDown(KeyCode.E) | Input.GetButtonDown("Fire1") | Input.GetKeyDown(KeyCode.Space))
            {
                NextDialogue();
            }
            else if (dialogueText.text != dialogueNpc[dialogueIndex] && Input.GetKeyDown(KeyCode.E) | Input.GetButtonDown("Fire1") | Input.GetKeyDown(KeyCode.Space))
            {
                skip = true;

            }
        }


    }
    void NextDialogue()
    {
        dialogueIndex++;
        if (dialogueIndex < dialogueNpc.Length)
        {
            if (dialogueIndex >= 3)
            {
                nameNpc.text = "Deus";
            }
            StartCoroutine(ShowDialogue());
        }
        else
        {
            animator.SetBool("Close", true);
            startDialogue = false;
            dialogueIndex = 0;
            readyToSpeak = false;
            if (deusanimator != null)
                deusanimator.SetBool("FinishDialogue", true);
            if (monster != null)
                monster.GetComponent<Rigidbody2D>().constraints = rb2d.constraints;
            if (player != null)
                FindObjectOfType<Player>().GetComponent<Rigidbody2D>().constraints = rb2d.constraints;
            if (player != null)
                porta.changescene = true;
        }
    }
    void StartDialogue()
    {
        audioSource.PlayOneShot(openDialogue);
        player = FindObjectOfType<Player>();
        if (monster != null)
            monster.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
        if (player != null)
        {
            rb2d.constraints = FindObjectOfType<Player>().GetComponent<Rigidbody2D>().constraints;
            FindObjectOfType<Player>().GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
        }
        if (porta != null)
            porta.changescene = false;
        nameNpc.text = name;
        imageNpc.sprite = spriteNpc;
        startDialogue = true;
        dialogueIndex = 0;
        dialoguePanel.SetActive(true);
        StartCoroutine(ShowDialogue());
    }
    IEnumerator ShowDialogue()
    {
        int index = 0;
        dialogueText.text = "";
        foreach (char letter in dialogueNpc[dialogueIndex])
        {
            dialogueText.text += letter;
            index++;
            if (!skip && index > 13)
            {
                audioSource.PlayOneShot(TextSound);
                index = 0;
            }
            else if (index > 50 && skip)
            {
                audioSource.PlayOneShot(TextSound);
                index = 0;
            }
            if (skip)
            {
            }
            else
                yield return new WaitForSeconds(0.01f);
        }
        skip = false;
    }

}
