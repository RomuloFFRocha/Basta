using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckScore : MonoBehaviour
{
    [SerializeField] private DialogueTrigger goodEnding;
    [SerializeField] private DialogueTrigger badEnding;
    
    public void Check(int points)
    {
        if (points <= 0)
            badEnding.TriggerDialogue();
        else
            goodEnding.TriggerDialogue();
    }
}
