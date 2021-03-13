using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AnswerButton
{
    [TextArea(3, 10)] public string buttonText;

    public DialogueTrigger dialogueToTrigger;

    public int pointsWorth;
}
