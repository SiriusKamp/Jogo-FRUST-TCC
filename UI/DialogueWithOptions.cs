using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Rigidbody2D))]
public class DialogueWithOptions : MonoBehaviour
{
    public string[] dialogueNpc;
    public int dialogueIndex;
    public GameObject ButtonPannel;
    public GameObject dialoguePanel;
    public Text dialogueText;
    public string name;
    public Text nameNpc;
    public Image imageNpc;
    public Sprite spriteNpc;
    public bool skip = false;
    public bool readyToSpeak;
    public bool startDialogue;
    public Animator Dialogueanimator;
    public Rigidbody2D rb2d;
    public bool trigger;
    public float cd;
    public bool StartWithNoInteraction = false;
    public float timetostart;
    public AudioSource audioSource;
    public AudioClip TextSound;
    public AudioClip OptionSound;
    public AudioClip openDialogue;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = this.GetComponent<Rigidbody2D>();
        dialoguePanel.SetActive(false);
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
    public void NextDialogue()
    {
        dialogueIndex++;
        if (dialogueIndex < dialogueNpc.Length)
        {
            StartCoroutine(ShowDialogue());
        }
        else if (dialogueIndex == dialogueNpc.Length)
        {
            readyToSpeak = false;
            ButtonPannel.SetActive(true);
        }
        else
        {
            FindObjectOfType<Player>().GetComponent<Rigidbody2D>().constraints = rb2d.constraints;
            Dialogueanimator.SetBool("Close", true);
            ButtonPannel.SetActive(false);
            cd = Time.time + 0.7f;
            startDialogue = false;
            dialogueIndex = 0;
        }
    }
    void StartDialogue()
    {
        audioSource.PlayOneShot(openDialogue);
        readyToSpeak = true;
        rb2d.constraints = FindObjectOfType<Player>().GetComponent<Rigidbody2D>().constraints;
        nameNpc.text = name;
        imageNpc.sprite = spriteNpc;
        startDialogue = true;
        dialogueIndex = 0;
        dialoguePanel.SetActive(true);
        ButtonPannel.SetActive(false);
        StartCoroutine(ShowDialogue());
        FindObjectOfType<Player>().GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        FindObjectOfType<Player>().CanInteract();
        if (collider.CompareTag("Player"))
        {
            readyToSpeak = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        FindObjectOfType<Player>().keyE.SetActive(false);
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
    public void SetTriggerOf()
    {
        trigger = false;
    }
    public void SetTriggerOn()
    {
        trigger = true;
    }
    public void PlaySound()
    {
        audioSource.PlayOneShot(OptionSound);
    }
}
