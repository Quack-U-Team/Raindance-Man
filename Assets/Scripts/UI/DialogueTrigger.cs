using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Transform player;
    public Transform speaker;
    public float range;

    public Dialogue dialogue;

    bool canStartDialogue = false;

    private void FixedUpdate()
    {
        if(Vector2.Distance(player.position, speaker.position) < range)
        {
            canStartDialogue = true;
        }
        else
        {
            canStartDialogue = false;
        }

        if (canStartDialogue && Input.GetKeyDown(KeyCode.E))
        {
            PlayerMovement.instance.Freeze();
            TriggerDialogue();
        }
    }

    public void TriggerDialogue()
    {
        if(DialogueManager.instance.state == DialogueManager.DialogueState.None)
        {
            print("starting dialogue");
            DialogueManager.instance.StartDialogue(dialogue);
        }
        if(DialogueManager.instance.state == DialogueManager.DialogueState.Started)
        {
            print("continuing dialogue");
            DialogueManager.instance.DisplayNextSentence();
        }
    }
}
