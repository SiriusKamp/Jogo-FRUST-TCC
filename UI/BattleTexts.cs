using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BattleTexts : MonoBehaviour
{
    public string[] dialogueNpc;
    public int dialogueIndex;

    public GameObject dialoguePanel;
    public Text dialogueText;
    public bool skip = false;
    public bool startDialogue;
    public bool trigger;
    public AudioSource audioSource;
    public AudioClip TextSound;

    // Start is called before the first frame update
    void Start()
    {
        dialoguePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) | Input.GetButtonDown("Fire1") | Input.GetKeyDown(KeyCode.Space) && trigger == true)
        {

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
        dialoguePanel.SetActive(false);
        dialogueIndex = 0;
    }
    public void StartDialogue()
    {
        startDialogue = true;
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
}
