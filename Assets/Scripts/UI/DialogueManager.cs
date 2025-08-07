using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueUI;

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

    private void Start()
    {
        sentences = new Queue<string>();

        instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            DisplayNextSentence();
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
        sentenceText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            sentenceText.text += letter;
            yield return null; // change this for slower or faster text scroll
        }
    }

    void EndDialogue()
    {
        state = DialogueState.Ended;
        dialogueUI.SetActive(false);
        print("end of conversation");
    }
}
