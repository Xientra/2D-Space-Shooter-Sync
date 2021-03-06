﻿using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class AudioControllerScript : MonoBehaviour {

    public static AudioControllerScript activeInstance;


    public SoundData[] soundData;

    public bool mute = false;

    public bool useV2 = false;

    void Awake() {
        if (activeInstance != null) {
            Destroy(this.gameObject);
            return;
        }
        else if (activeInstance == null){
            activeInstance = this;
            DontDestroyOnLoad(this);

            foreach (SoundData sD in soundData) {
                sD.audioSource = gameObject.AddComponent<AudioSource>();
                sD.audioSource.clip = sD.SoundClip;
                sD.audioSource.volume = sD.volume;
                sD.audioSource.pitch = sD.pitch;
                sD.audioSource.loop = sD.loop;
            }
        }
    }

    private void OnLevelWasLoaded(int level) {
        StopAllSoundeffects();
    }

    public void PlaySound(string _soundName) {
        if (GameControllerScript.SoundIsMuted == false && mute == false) {
            if (useV2 == false) {
                if (GetSoundData(_soundName) != null) {
                    SoundData sd = GetSoundData(_soundName);
                    sd.audioSource.volume = sd.volume;
                    sd.audioSource.pitch = sd.pitch;
                    sd.audioSource.loop = sd.loop;
                    sd.audioSource.Play();
                }
                else {
                    Debug.LogError(_soundName + " Not Found");
                }
            }
        }
    }

    public void PlaySound(string _soundName, float _pitch) {
        if (GameControllerScript.SoundIsMuted == false && mute == false) {
            if (useV2 == false) {
                SoundData sd = GetSoundData(_soundName);
                if (sd != null) {
                    sd.audioSource.volume = sd.volume;
                    sd.audioSource.pitch = _pitch;
                    sd.audioSource.loop = sd.loop;
                    sd.audioSource.Play();
                }
                else {
                    Debug.LogError(_soundName + " Not Found");
                }
            }
        }
    }

    public void StopSound(string _soundName) {
        SoundData sd = GetSoundData(_soundName);
        if (sd != null) {
            sd.audioSource.Stop();
        }
    }

    public void StopAllSoundeffects() {
        foreach (SoundData sd in soundData) {
            if (sd != null) {
                if (sd.isMusic != true) {
                    if (sd.audioSource != null) {
                        sd.audioSource.Stop();
                    }
                    //else if (sd.audioSource == null) Debug.LogError("The SoundData with " + sd.SoundClip.name + " has no audio Source");
                }
            }
        }
    }

    public void StopAllLoopingSounds() {
        foreach (SoundData sd in soundData) {
            if (sd != null) {
                if (sd.isMusic != true) {
                    if (sd.loop == true) {
                        if (sd.audioSource != null) {
                            sd.audioSource.Stop();
                        }
                        //else if (sd.audioSource == null) Debug.LogError("The SoundData with " + sd.SoundClip.name + " has no audio Source");
                    }
                }
            }
        }
    }

    private SoundData GetSoundData(string _soundName) {
        foreach (SoundData sD in soundData) {
            if (sD.SoundClip.name == _soundName)
                return sD;
        }
        Debug.LogError("There is no SoundData for the Sound with the Name: \"" + _soundName + "\".");
        return null;
    }

    /*
    //only used in v2
    private AudioClip GetSound(string _soundName) {
        foreach (AudioClip ac in sounds) {
            if (ac.name == _soundName)
                return ac;
        }
        Debug.LogError("There is no Sound with the Name: \"" + _soundName + "\".");
        return null;
    }

    */
}