using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;                      //variavel que contem todas as informações do dialogo
    private bool alreadyTriggeredDialogue = false;
    
    public void TriggerDialogue()
    {
        if (!alreadyTriggeredDialogue)
        {
            if (dialogue.answerButtons.Length > 0)
                dialogue.haveDialogueMenu = true;
            else
                dialogue.haveDialogueMenu = false;

            dialogue.chatToSendMessage.StartDialogue(dialogue);
            alreadyTriggeredDialogue = true;
        }
    }
}
