using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(IsInteractible))]
public class Texto : MonoBehaviour
{
    public string[] dialogueNpc;
    public int dialogueIndex;

    public GameObject dialoguePanel;
    public Text dialogueText;
    public bool skip = false;
    public bool readyToSpeak;
    public bool startDialogue;
    public Animator animator;
    public bool trigger;
    public Rigidbody2D rb2d;
    public float cd;
    public Player player;
    public Vector3 PlayerPosition = new Vector3();
    public AudioSource audioSource;
    public AudioClip TextSound;
    public int timetostart;
    public bool StartWithNoInteraction;
    public Animator AnimatiorOclusion;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        dialoguePanel.SetActive(false);
        rb2d = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) | Input.GetButtonDown("Fire1") | Input.GetKeyDown(KeyCode.Space) && readyToSpeak && trigger == true)
        {
            if (Time.time > cd)
            {
                if (!startDialogue)
                {
                    dialogueIndex = 0;
                    StartDialogue();
                }
                else if (dialogueText.text == dialogueNpc[dialogueIndex])
                {
                    NextDialogue();
                }
                else if (dialogueText.text != dialogueNpc[dialogueIndex])
                {
                    skip = true;

                }
            }
        }
        if (StartWithNoInteraction && Time.time > timetostart)
        {
            if (!startDialogue && cd == 0)
            {
                StartDialogue();
                readyToSpeak = true;
            }
            else if (startDialogue && Input.GetKeyDown(KeyCode.E) | Input.GetButtonDown("Fire1") | Input.GetKeyDown(KeyCode.Space) && readyToSpeak && trigger == true)
                if (dialogueText.text == dialogueNpc[dialogueIndex])
                {
                    NextDialogue();
                }
                else if (dialogueText.text != dialogueNpc[dialogueIndex])
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
            StartCoroutine(ShowDialogue());
        }
        else
        {
            animator.SetBool("Close", true);
            readyToSpeak = false;
            StartWithNoInteraction = false;
            cd = Time.time + 0.7f;
            if (AnimatiorOclusion != null)
                AnimatiorOclusion.SetBool("FinishDialogue", true);
            startDialogue = false;
            dialogueIndex = 0;
            if (player != null)
                player.GetComponent<Rigidbody2D>().constraints = rb2d.constraints;
        }
    }
    public void StartDialogue()
    {
        if (player != null)
            rb2d.constraints = player.GetComponent<Rigidbody2D>().constraints;
        startDialogue = true;
        dialoguePanel.SetActive(true);
        if (player != null)
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
        StartCoroutine(ShowDialogue());
        if (player != null)
            player.transform.position = PlayerPosition;
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            readyToSpeak = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            readyToSpeak = false;
        }
    }
    IEnumerator ShowDialogue()
    {
        int index = 0;
        dialogueText.text = "";
        foreach (char letter in dialogueNpc[dialogueIndex])
        {
            dialogueText.text += letter;
            index++;
            if (!skip && index > 14)
            {
                if (TextSound != null)
                    audioSource.PlayOneShot(TextSound);
                index = 0;
            }
            else if (index > 50 && skip)
            {
                if (TextSound != null)
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
    public void SetTriggerOf()
    {
        trigger = false;
    }
    public void SetTriggerOn()
    {
        trigger = true;
    }
}
