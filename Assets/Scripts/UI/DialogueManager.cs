using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueUI;
    AudioSource typewritingSound;

    public TextMeshProUGUI speakerName;
    public TextMeshProUGUI sentenceText;

    // FIFO collection
    Queue<string> sentences;
    public static DialogueManager instance;

    public enum DialogueState
    {
        None,
        Started,
        Talking,
        Ended
    }
    [HideInInspector]
    public DialogueState state;

    private void Awake()
    {
        Time.timeScale = 1f;
    }

    private void Start()
    {
        instance = this;
        typewritingSound = GetComponent<AudioSource>();
        sentences = new Queue<string>();
        state = DialogueState.None;
    }

    private void Update()
    {
        if (state == DialogueState.Talking)
        {
            typewritingSound.Play();
        }
        else
        {
            typewritingSound.Stop();
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        state = DialogueState.Started;

        dialogueUI.SetActive(true);

        speakerName.text = dialogue.speakerName;
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            print("loading sentences");
            sentences.Enqueue(sentence);
        }
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        else
        {
            string sentenceToShow = sentences.Dequeue();
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentenceToShow));
        }
    }

    IEnumerator TypeSentence (string sentence)
    {
        print("typing sentence...");
        sentenceText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            state = DialogueState.Talking;
            sentenceText.text += letter;
            yield return new WaitForSecondsRealtime(0.02f); // why is this the problem
        }
        state = DialogueState.Started;
    }

    void EndDialogue()
    {
        state = DialogueState.Ended;
        PlayerMovement.instance.Unfreeze();
        dialogueUI.SetActive(false);
        print("dialogue ended :(");
    }
}
