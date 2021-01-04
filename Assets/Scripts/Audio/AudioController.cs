using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Assets.Scripts.Audio;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;
    
    private AudioSource[][] _audioSources;

    [Header("Global volume settings")]
    [Range (0f, 1f)]
    [SerializeField] private float _globalBGMVolume = 0.8f;
    
    [Range (0f, 1f)]
    [SerializeField] private float _globalSFXVolume = 0.8f;
    
    [Header("Mute Options")]
    [SerializeField] private bool _bmgIsMuted;
    [SerializeField] private bool _sfxIsMuted;
    
    [Header("AudioClips")]
    [SerializeField] private AudioClip[] _sfxClips;
    [SerializeField] private AudioClip[] _bgmClips;

    private const int MaxSFXSources = 8;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        
        DontDestroyOnLoad(gameObject);

        Init();
    }

    private void Init()
    {
        _audioSources = new AudioSource[Enum.GetNames(typeof(AudioSourceType)).Length][];
        _audioSources[(int) AudioSourceType.Music] = new AudioSource[Enum.GetNames(typeof(AudioBGMType)).Length];
        _audioSources[(int) AudioSourceType.SFX] = new AudioSource[MaxSFXSources];

        for (int i = 0; i < _audioSources.Length; i++)
        {
            GameObject audioPanelObject = new GameObject();
            audioPanelObject.transform.SetParent(transform);
            audioPanelObject.name = (AudioSourceType) i + " [AudioPanel]";

            for (int j = 0; j < _audioSources[i].Length; j++)
            {
                GameObject audioObject = new GameObject();
                audioObject.transform.SetParent(audioPanelObject.transform);
                _audioSources[i][j] = audioObject.AddComponent<AudioSource>();
                audioObject.name = i == (int) AudioSourceType.Music
                    ? (AudioBGMType) j + " [AudioSource]"
                    : "SFX Channel " + j + " [AudioSource]";

                _audioSources[i][j].volume = i == (int) AudioSourceType.Music ? _globalBGMVolume : _globalSFXVolume;
                _audioSources[i][j].playOnAwake = false;
                _audioSources[i][j].loop = i == (int)AudioSourceType.Music ? true : false;
            }
        }
    }

    // PLAY
    public void PlayBGM(AudioBGMType audioBGMType, AudioClip audioClip, float volume = 1f, float delay = 0f)
    {
        if (_bmgIsMuted)
            return;
        
        AudioSource audioSource = GetBGMSource(audioBGMType);
        audioSource.clip = audioClip;
        audioSource.volume = _globalBGMVolume * volume;
        audioSource.PlayDelayed(delay);
    }

    public void PlayBGM(AudioBGMType audioBGMType, BGMClipType clipType, float volume = 1f, float delay = 0f)
    {
        if (_bmgIsMuted)
            return;
        
        AudioSource audioSource = GetBGMSource(audioBGMType);
        audioSource.clip = GetBGMClip(clipType);
        audioSource.volume = _globalBGMVolume * volume;
        audioSource.PlayDelayed(delay);
    }
    
    public void PlaySFX(AudioClip audioClip, float volume = 1f, float delay = 0f)
    {
        if (_sfxIsMuted)
            return;

        AudioSource audioSource = GetFreeSFXSource();
        audioSource.clip = audioClip;
        audioSource.volume = _globalSFXVolume * volume;
        audioSource.PlayDelayed(delay);
    }

    public void PlaySFX(SFXClipType clipType, float volume = 1f, float delay = 0f)
    {
        if (_sfxIsMuted)
            return;

        AudioSource audioSource = GetFreeSFXSource();
        audioSource.clip = GetSFXClip(clipType);
        audioSource.volume = _globalSFXVolume * volume;
        audioSource.PlayDelayed(delay);
    }

    // STOP
    public void StopBGM(AudioBGMType audioBGMType)
    {
        StopAudio(AudioSourceType.Music, (int) audioBGMType);
    }
    
    public void StopAllBGM()
    {
        for (int i = 0; i < _audioSources[(int) AudioSourceType.Music].Length; i++)
        {
            StopAudio(AudioSourceType.Music, i);
        }
    }

    public void StopAllSFX()
    {
        for (int i = 0; i < MaxSFXSources; i++)
        {
            StopAudio(AudioSourceType.SFX, i);
        }
    }

    public void StopAllSound()
    {
        StopAllBGM();
        StopAllSFX();
    }

    private void StopAudio(AudioSourceType audioSourceType, int sourceIndex)
    {
        _audioSources[(int) audioSourceType][sourceIndex].Stop();
    }

    // PAUSE
    public void SetPauseBGM(AudioBGMType audioBGMType, bool pause)
    {
        SetPauseAudio(AudioSourceType.Music, (int) audioBGMType, pause);
    }
    
    public void SetPauseAllBGM(bool pause)
    {
        for (int i = 0; i < _audioSources[(int) AudioSourceType.Music].Length; i++)
        {
            SetPauseAudio(AudioSourceType.Music, i, pause);
        }
    }

    public void SetPauseAllSFX(bool pause)
    {
        for (int i = 0; i < MaxSFXSources; i++)
        {
            SetPauseAudio(AudioSourceType.SFX, i, pause);
        }
    }

    public void SetPauseAllSound(bool pause)
    {
        SetPauseAllBGM(pause);
        SetPauseAllSFX(pause);
    }

    private void SetPauseAudio(AudioSourceType audioSourceType, int sourceIndex, bool pause)
    {
        if (pause)
            _audioSources[(int) audioSourceType][sourceIndex].Pause();
        else
            _audioSources[(int) audioSourceType][sourceIndex].UnPause();
    }
    
    private AudioSource GetFreeSFXSource()
    {
        AudioSource audioSource = _audioSources[(int) AudioSourceType.Music][0];
        float timeSpent = -1f;

        for (int i = 0; i < MaxSFXSources; i++)
        {
            AudioSource sfxSource = _audioSources[(int) AudioSourceType.SFX][i];
            if (!sfxSource.isPlaying)
                return sfxSource;

            if (sfxSource.time > timeSpent)
            {
                timeSpent = sfxSource.time;
                audioSource = sfxSource;
            }
        }

        return audioSource;
    }
    
    private AudioSource GetBGMSource(AudioBGMType audioBGMType)
    {
        return _audioSources[(int) AudioSourceType.Music][(int) audioBGMType];
    }

    private AudioClip GetBGMClip(BGMClipType clipType)
    {
        return _bgmClips[(int) clipType];
    }
    
    private AudioClip GetSFXClip(SFXClipType clipType)
    {
        return _sfxClips[(int) clipType];
    }

    public void SetGlobalBGMVolume(float volume)
    {
        _globalBGMVolume = volume;
        foreach (AudioSource audioSource in _audioSources[(int) AudioSourceType.Music])
        {
            audioSource.volume = _globalBGMVolume;
        }
    }
    
    public void SetGlobalSFXVolume(float volume)
    {
        _globalSFXVolume = volume;
        foreach (AudioSource audioSource in _audioSources[(int) AudioSourceType.SFX])
        {
            audioSource.volume = _globalSFXVolume;
        }
    }

    public void SetMute(AudioSourceType audioSourceType, bool mute)
    {
        if (audioSourceType == AudioSourceType.Music)
            _bmgIsMuted = mute;
        else
            _sfxIsMuted = mute;
    }
}
