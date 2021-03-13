using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

[System.Serializable]
public class Sentence
{
    public string name = "";                                 //nome da personagem que está falando
    
    [TextArea(3, 10)]
    public string sentence = "";                                 //fala da personagem

    public string hour;

    public string minute;

    public Sprite imageSent = null;

    public bool thePlayerHasSentTheMessage = false;

    public float timeToNextMessage;

    public int nameColor;
}
