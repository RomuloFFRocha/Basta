using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffSpecial : MonoBehaviour
{
    public DialogueTrigger dialogueToTrigger;
    public NotificationTrigger notificationToTrigger;
    public GameObject zap;
    public GameObject grupoDoZap;
    public GameObject conversas;
    public GameObject canvasNotifon;
    public GameObject canvasNotifoff;


    public void Desligar()
    {
        gameObject.SetActive(false);
    }

    public void TriggerDialogue()
    {
        dialogueToTrigger.TriggerDialogue();
        zap.SetActive(false);
        grupoDoZap.SetActive(false);
        conversas.SetActive(false);
    }

    public void TriggerNotif()
    {
        notificationToTrigger.TriggerNotification();
       
    }

    public void TriggerOnCanvasNotif()
    {
        canvasNotifon.SetActive(true);

    }

    public void TriggerOffCanvasNotif()
    {
        canvasNotifoff.SetActive(false);

    }

}
