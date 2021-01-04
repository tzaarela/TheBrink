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
    [SerializeField] private float _globalMasterVolume = 0.8f;
    
    [Range (0f, 1f)]
    [SerializeField] private float _globalBGMVolume = 0.8f;
    
    [Range (0f, 1f)]
    [SerializeField] private float _globalSFXVolume = 0.8f;
    
    [Header("Mute Options")]
    [SerializeField] private bool _masterIsMuted;
    [SerializeField] private bool _bgmIsMuted;
    [SerializeField] private bool _sfxIsMuted;

    private float _masterMuteVolume;
    private float _bgmMuteVolume;
    private float _sfxMuteVolume;
    
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

        _masterMuteVolume = _masterIsMuted ? 0f : 1f;
        _bgmMuteVolume = _bgmIsMuted ? 0f : 1f;
        _sfxMuteVolume = _sfxIsMuted ? 0f : 1f;
    }

    // PLAY
    public void PlayBGM(AudioBGMType audioBGMType, AudioClip audioClip, float volume = 1f, float delay = 0f)
    {
        if (_bgmIsMuted)
            return;
        
        AudioSource audioSource = GetBGMSource(audioBGMType);
        audioSource.clip = audioClip;
        audioSource.volume = GetVolume(AudioSourceType.Music, volume);
        audioSource.PlayDelayed(delay);
    }

    public void PlayBGM(AudioBGMType audioBGMType, BGMClipType clipType, float volume = 1f, float delay = 0f)
    {
        if (_bgmIsMuted)
            return;
        
        AudioSource audioSource = GetBGMSource(audioBGMType);
        audioSource.clip = GetBGMClip(clipType);
        audioSource.volume = GetVolume(AudioSourceType.Music, volume);
        audioSource.PlayDelayed(delay);
    }
    
    public void PlaySFX(AudioClip audioClip, float volume = 1f, float delay = 0f)
    {
        if (_sfxIsMuted)
            return;

        AudioSource audioSource = GetFreeSFXSource();
        audioSource.clip = audioClip;
        audioSource.volume = GetVolume(AudioSourceType.SFX, volume);
        audioSource.PlayDelayed(delay);
    }

    public void PlaySFX(SFXClipType clipType, float volume = 1f, float delay = 0f)
    {
        if (_sfxIsMuted)
            return;

        AudioSource audioSource = GetFreeSFXSource();
        audioSource.clip = GetSFXClip(clipType);
        audioSource.volume = GetVolume(AudioSourceType.SFX, volume);
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

    public void SetGlobalVolume(VolumeType volumeType, float volume)
    {
        if (volumeType == VolumeType.Master)
        {
            _globalMasterVolume = volume;
            UpdateBGMVolume();
            UpdateSFXVolume();
        }
        else if (volumeType == VolumeType.Music)
        {
            _globalBGMVolume = volume;
            UpdateBGMVolume();
        }
        else if (volumeType == VolumeType.SFX)
        {
            _globalSFXVolume = volume;
            UpdateSFXVolume();
        }
    }

    private void UpdateBGMVolume()
    {
        foreach (AudioSource audioSource in _audioSources[(int) AudioSourceType.Music])
        {
            audioSource.volume = GetVolume(AudioSourceType.Music);
        }
    }
    
    private void UpdateSFXVolume()
    {
        foreach (AudioSource audioSource in _audioSources[(int) AudioSourceType.SFX])
        {
            audioSource.volume = GetVolume(AudioSourceType.SFX);
        }
    }

    public float GetGlobalVolume(VolumeType volumeType)
    {
        if (volumeType == VolumeType.Master)
            return _globalMasterVolume;
        else if (volumeType == VolumeType.Music)
            return _globalBGMVolume;
        else
            return _globalSFXVolume;
    }

    private float GetVolume(AudioSourceType audioSourceType, float volume = 1f)
    {
        if (audioSourceType == AudioSourceType.Music)
            return _globalMasterVolume * _globalBGMVolume * _masterMuteVolume * _bgmMuteVolume * volume;
        else
            return _globalMasterVolume * _globalSFXVolume * _masterMuteVolume * _sfxMuteVolume * volume;
    }

    public void SetMute(AudioSourceType audioSourceType, bool mute)
    {
        if (audioSourceType == AudioSourceType.Music)
            _bgmIsMuted = mute;
        else
            _sfxIsMuted = mute;
    }

    public void ToggleMute(VolumeType volumeType)
    {
        if (volumeType == VolumeType.Master)
        {
            _masterIsMuted = !_masterIsMuted;
            _masterMuteVolume = _masterIsMuted ? 0f : 1f;
            
            UpdateBGMVolume();
            UpdateSFXVolume();
        }
        else if (volumeType == VolumeType.Music)
        {
            _bgmIsMuted = !_bgmIsMuted;
            _bgmMuteVolume = _bgmIsMuted ? 0f : 1f;
            
            UpdateBGMVolume();
        }
        else
        {
            _sfxIsMuted = !_sfxIsMuted;
            _sfxMuteVolume = _sfxIsMuted ? 0f : 1f;
            
            UpdateSFXVolume();
        }
    }

    public bool IsVolumeMuted(VolumeType volumeType)
    {
        if (volumeType == VolumeType.Master)
            return _masterIsMuted;
        else if (volumeType == VolumeType.Music)
            return _bgmIsMuted;
        else
            return _sfxIsMuted;
    }
}
