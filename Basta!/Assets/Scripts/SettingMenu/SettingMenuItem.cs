using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingMenuItem : MonoBehaviour
{
    [HideInInspector] public Image img;
    [HideInInspector] public Transform trans;

    public bool hasChild;
    public Image img2;

    private void Awake()
    {
        img = GetComponent<Image>();
        trans = transform;
    }
}
