using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundSound : MonoBehaviour
{
    public AudioClip music;
    public bool loop;
    public bool persistent;
   
    [Range(0, 1)] public float volumeBGS;
    [Range(0, 1)] public float volumeSFX;

    public List<Toggle> BGSToggles;
    public List<Toggle> SFXToggles;


    private void Awake()
    {
        if (!AudioManager.IsMuteBGS())
        {
            for (int i = 0; i < BGSToggles.Count; i++)
            {
                BGSToggles[i].isOn = false;
            }
            
        }

        else
        {
            for (int i = 0; i < BGSToggles.Count; i++)
            {
                BGSToggles[i].isOn = true;
            }

            AudioManager.MuteBGS();
        }


        if (!AudioManager.IsMuteSFX())
        {
            for (int i = 0; i < SFXToggles.Count; i++)
            {
                SFXToggles[i].isOn = false;
            }
        }
            

        else
        {
            for (int i = 0; i < BGSToggles.Count; i++)
            {
                SFXToggles[i].isOn = true;
            }
           
            AudioManager.MuteSFX();
        }
    }

    void Start()
    {
        AudioManager.BGSVolume = volumeBGS;
        AudioManager.PlayBGS(music, loop, persistent);

        AudioManager.SFXVolume = volumeSFX;

    }

    public void MuteBGS()
    {
        AudioManager.ToggleMuteBGS(true);
    }

    public void MuteSFX()
    {
        AudioManager.ToggleMuteSFX(true);
    }

    public void PlaySFX(AudioClip audio)
    {
        AudioManager.PlaySFX(audio, volumeSFX);
    }
  
}
