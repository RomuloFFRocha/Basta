using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Notification
{
    public string notificationTitle;
    [TextArea(2, 5)] public string notificationDescription;
    public Sprite notificationIcon;
}
