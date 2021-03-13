using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Message : MonoBehaviour
{
    public GameObject nameObject, sentenceObject, dialogueBoxObject, imageSentObject, imageSentChildrenObject, timeObject;
    [HideInInspector] public Text nameText, sentenceText, timeText;
    [HideInInspector] public Image dialogueBoxImage, imageSentImage;
    [HideInInspector] public Button imageSentButton;
    [HideInInspector] public RectTransform dialogueBoxRectTransform;

    private void OnEnable()
    {
        if (nameObject != null)
            nameText = nameObject.GetComponent<Text>();

        if (sentenceObject != null)
            sentenceText = sentenceObject.GetComponent<Text>();

        if (timeObject != null)
            timeText = timeObject.GetComponent<Text>();

        if (imageSentObject != null)
            imageSentButton = imageSentObject.GetComponent<Button>();

        if (imageSentChildrenObject != null)
            imageSentImage = imageSentChildrenObject.GetComponent<Image>();

        if (dialogueBoxObject)
        {
            dialogueBoxImage = dialogueBoxObject.GetComponent<Image>();
            dialogueBoxRectTransform = dialogueBoxObject.GetComponent<RectTransform>();
        }
    }
}
