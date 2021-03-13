using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Dialogue
{
    public DialogueManager chatToSendMessage;

    [HideInInspector] public bool haveDialogueMenu;

    public Sentence[] sentences;

    public AnswerButton[] answerButtons;                  //botões de resposta do menu de dialogo

    public CheckScore checkScore;
    
    public int index;

    public bool newMessage = true;

    public UnityEvent eventToTrigger;
}
