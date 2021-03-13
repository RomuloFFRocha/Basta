using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public static bool vibrate;

    public static float messageSpeed = 1;

    public Toggle toggleSFX;

    public Toggle vibration;

    int saveBool;

    private void Awake()
    {
        saveBool = PlayerPrefs.GetInt("SaveBool", 1);

        if (saveBool == 0)
        {
            vibrate = false;
        }

        else
        {
            vibrate = true;
        }

    }
    private void Start()
    {
        if (!AudioManager.IsMuteSFX())
            toggleSFX.isOn = true;
        else
            toggleSFX.isOn = false;

        if (saveBool == 0)
        {
            vibration.isOn = false;
        }

        else
        {
            vibration.isOn = true;
        }
    }

    public void MuteSoundFx(bool mute)
    {
        if (!mute)
            AudioManager.MuteSFX();
        else
            AudioManager.UnmuteSFX();
    }

    public void OnOffVibrate(bool vibration)
    {
        int number;

        vibrate = vibration;

        if (!vibration)
        {
            number = 0;

        }
        else
        {
            number = 1;
        }

        PlayerPrefs.SetInt("SaveBool", number);
        saveBool = number;
    }

    public void ChangeMessageSpeed(bool changeSpeed)
    {

        if(changeSpeed)
        {
            messageSpeed = 0.6f;
        }
        else
        {
            messageSpeed = 1;
        }

    }


}

