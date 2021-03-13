using UnityEngine;
using System;
using System.Collections;

public class AudioManager
{

    public delegate void VoidDelegate();
    public static event VoidDelegate OnBgMute;
    public static event VoidDelegate OnBgUnmute;

    private const string PATH = "Audio/";

    private const string BGS_GO_NAME = "AudioBGS";
    private const string SFX_GO_NAME = "AudioSFX";

    private const string BGS_MUTE_KEY = "AudioBGSKey";
    private const string SFX_MUTE_KEY = "AudioSFXKey";

    public AudioSource bgsSource = null;
    public AudioSource sfxSource = null;

    public float sfxVolume = 1.0f;
    public float bgsVolume;

    private bool sfxMute = false;
    private bool bgsMute = false;

    public static Action onMuteSFX;

    private static AudioManager _instance = null;
    private static AudioManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new AudioManager();

                if (PlayerPrefs.GetInt(BGS_MUTE_KEY, 0) != 0)
                {
                    MuteBGS();
                }
                if (PlayerPrefs.GetInt(SFX_MUTE_KEY, 0) != 0)
                {
                    MuteSFX();
                }
            }
            return _instance;
        }
    }

    private AudioManager()
    {
    }

    #region Mute Total
    public static void Mute()
    {
        AudioListener.volume = 0.0f;
    }
    public static void Unmute()
    {
        AudioListener.volume = 1.0f;
    }

    public static bool IsMute()
    {
        return IsMuteBGS() && IsMuteSFX();
    }
    #endregion

    #region SFX

    public static void MuteSFX()
    {
        MuteSFX(true);
    }

    public static void MuteSFX(bool persist)
    {
        if (instance.sfxSource != null)
        {
            instance.sfxSource.Stop();
            instance.sfxSource.volume = 0.0f;
        }
        instance.sfxMute = true;
        if (persist)
            PlayerPrefs.SetInt(SFX_MUTE_KEY, 1);

        if (onMuteSFX != null)
            onMuteSFX();
    }

    public static void UnmuteSFX()
    {
        UnmuteSFX(true);
    }

    public static void UnmuteSFX(bool persist)
    {
        if (instance.sfxSource != null)
        {
            instance.sfxSource.volume = instance.sfxVolume;
        }
        instance.sfxMute = false;

        if (persist)
            PlayerPrefs.SetInt(SFX_MUTE_KEY, 0);
    }

    public static void ToggleMuteSFX()
    {
        ToggleMuteSFX(true);
    }

    public static void ToggleMuteSFX(bool persist)
    {
        if (IsMuteSFX())
        {
            UnmuteSFX(persist);
        }
        else
        {
            MuteSFX(persist);
        }
    }

    public static bool IsMuteSFX()
    {
        return instance.sfxMute;
    }

    public static float SFXVolume
    {
        set
        {
            SetSFXVolume(value);
        }
        get
        {
            return GetSFXVolume();
        }
    }

    public static float GetSFXVolume()
    {
        return instance.sfxVolume;
    }

    public static void SetSFXVolume(float volume)
    {
        instance.sfxVolume = volume;
    }

    public static void PlaySFX(AudioClip clip, float volume = 1)
    {
        if (instance.sfxSource == null)
        {
            InitSFXSource();
        }

        PlaySFX(instance.sfxSource, clip, volume);
    }

    public static void PlaySFX(AudioSource source, float volume = 1)
    {
        PlaySFX(source, source.clip, volume);
    }

    public static void PlaySFX(AudioSource source, AudioClip clip, float volume = 1)
    {
        if (IsMuteSFX()) return;

        source.volume = instance.sfxVolume;
        source.PlayOneShot(clip, instance.sfxVolume * volume);
    }

    public static void PlaySFXSource(AudioSource source, AudioClip clip, float volume = 1)
    {
        if (IsMuteSFX()) return;

        source.volume = instance.sfxVolume;
        source.clip = clip;
        source.PlayOneShot(clip);
    }

    public static bool isSFXPlaying
    {
        get
        {
            if (instance.sfxSource == null)
                return false;

            return instance.sfxSource.isPlaying;
        }
    }

    private static void InitSFXSource()
    {
        GameObject newgo = new GameObject();
        newgo.name = SFX_GO_NAME;
        newgo.AddComponent<AudioSource>();
        GameObject.DontDestroyOnLoad(newgo);
        instance.sfxSource = newgo.GetComponent<AudioSource>();
    }
    #endregion

    #region BGS

    public static void MuteBGS()
    {
        MuteBGS(true);
    }

    public static void MuteBGS(bool persist)
    {
        instance.bgsMute = true;
        if (instance.bgsSource != null)
        {
            instance.bgsSource.volume = 0.0f;
        }

        if (OnBgMute != null)
            AudioManager.OnBgMute();

        if (persist)
            PlayerPrefs.SetInt(BGS_MUTE_KEY, 1);
    }

    public static void UnmuteBGS()
    {
        UnmuteBGS(true);
    }

    public static void UnmuteBGS(bool persist)
    {
        instance.bgsMute = false;
        if (instance.bgsSource != null)
        {
            instance.bgsSource.volume = instance.bgsVolume;
        }

        if (OnBgUnmute != null)
            AudioManager.OnBgUnmute();

        if (persist)
            PlayerPrefs.SetInt(BGS_MUTE_KEY, 0);
    }

    public static void ToggleMuteBGS()
    {
        ToggleMuteBGS(true);
    }

    public static void ToggleMuteBGS(bool persist)
    {
        if (IsMuteBGS())
        {
            UnmuteBGS();
        }
        else
        {
            MuteBGS();
        }
    }

    public static bool IsMuteBGS()
    {
        return instance.bgsMute;
    }

    public static float BGSVolume
    {
        set
        {
            SetBGSVolume(value);
        }
        get
        {
            return GetBGSVolume();
        }
    }

    public static float GetBGSVolume()
    {
        return instance.bgsVolume;
    }

    public static void SetBGSVolume(float volume)
    {
        instance.bgsVolume = volume;
    }

    public static void PlayBGS(string clipname)
    {
        PlayBGS(clipname, true);
    }

    public static void PlayBGS(string clipname, bool loop)
    {
        PlayBGS(clipname, loop, false);
    }

    public static void PlayBGS(string clipname, bool loop, bool forcestart)
    {
        AudioClip clip = Resources.Load(PATH + clipname, typeof(AudioClip)) as AudioClip;
        PlayBGS(clip, loop, forcestart);
    }

    public static void PlayBGS(AudioClip clip)
    {
        PlayBGS(clip, true);
    }

    public static void PlayBGS(AudioClip clip, bool loop)
    {
        PlayBGS(clip, loop, true);
    }

    public static void PlayBGS(AudioClip clip, bool loop, bool persistent)
    {
        PlayBGS(clip, loop, false, persistent);
    }

    public static void PlayBGS(AudioClip clip, bool loop, bool forcestart, bool persistent)
    {
        PlayBGS(clip, loop, forcestart, persistent, 0f);
    }

    public static void PlayBGS(AudioClip clip, bool loop, bool forcestart, bool persistent, float time)
    {
        if (instance.bgsSource == null)
        {
            InitBGSSource(persistent);
        }

        if (!forcestart && (clip == instance.bgsSource.clip))
        {
            return;
        }

        if (!IsMuteBGS())
        {
            instance.bgsSource.volume = instance.bgsVolume;
        }
        else
        {
            instance.bgsSource.volume = 0.0f;
        }

        instance.bgsSource.playOnAwake = false;
        instance.bgsSource.clip = clip;
        instance.bgsSource.loop = loop;
        instance.bgsSource.time = time;
        instance.bgsSource.Play();
    }

    public static void StopBGS()
    {
        if (instance.bgsSource != null)
            instance.bgsSource.Stop();
    }

    public static float PauseBGS()
    {
        if (instance.bgsSource == null) return 0;
        var time = instance.bgsSource.time;
        instance.bgsSource.Stop();
        return time;
    }

    public static string GetBGSName()
    {
        if (instance.bgsSource == null || instance.bgsSource.clip == null)
        {
            return "";
        }
        return instance.bgsSource.clip.name;
    }

    private static void InitBGSSource(bool persistent)
    {
        GameObject newgo = new GameObject();
        newgo.name = BGS_GO_NAME;
        newgo.AddComponent<AudioSource>();

        if (persistent)
            GameObject.DontDestroyOnLoad(newgo);

        instance.bgsSource = newgo.AddComponent<AudioSource>();
    }
    #endregion
}