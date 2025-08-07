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
            //print("entered range to start dialogue");
            canStartDialogue = true;
        }
        else
        {
            canStartDialogue = false;
        }

        if (canStartDialogue && Input.GetKeyDown(KeyCode.E))
        {
            TriggerDialogue();
        }
    }

    public void TriggerDialogue()
    {
        if(DialogueManager.instance.state == DialogueManager.DialogueState.None)
        {
            DialogueManager.instance.StartDialogue(dialogue);
        }
    }
}
