﻿using System.Collections;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class soundChanger : MonoBehaviour
{
    public AudioMixer mixer;
    public static float CurrentVolume;
    public static int isMuted = 0;
    public static Slider mySlider;
    public static Toggle myToggle;

    void Start()
    {
        myToggle = GameObject.Find("Toggle").GetComponentInChildren<Toggle>();
        mySlider = GameObject.Find("mySlider").GetComponentInChildren<Slider>();
        try
        {
            CurrentVolume = PlayerPrefs.GetFloat("myVolume");
            isMuted = PlayerPrefs.GetInt("isAudioMuted");
            if (isMuted == 1)
            {
                myToggle.isOn = true;
                mySlider.value = 0.001f;
            }
            else if (isMuted == 0)
            {
                myToggle.isOn = false;
                CurrentVolume = 1;
            }
            mySlider.value = CurrentVolume;
        }
        catch (Exception e)
        {
            myToggle.isOn = false;
            PlayerPrefs.SetFloat("myVolume", 1);
            CurrentVolume = PlayerPrefs.GetFloat("myVolume");
            mySlider.value = CurrentVolume;
            PlayerPrefs.SetInt("isAudioMuted", 0);
        }
        
    }
    void Update()
    {
        if (myToggle.isOn)
        {
            mySlider.value = 0.001f;
        }
    }
    public void SetLevel(float sliderValue)
    {
        if(sliderValue == 0.001f)
        {
            myToggle.isOn = true;
        }
        mixer.SetFloat("myGlobalVolume", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("myVolume", sliderValue);
    }
    public void MuteAudio()
    {
        if (myToggle.isOn)
        {
            CurrentVolume = mySlider.value;
            PlayerPrefs.SetInt("isAudioMuted", 1);
            isMuted = 1;
            mySlider.value = 0.001f;
            mixer.SetFloat("myGlobalVolume", Mathf.Log10(mySlider.value) * 20);
        }
        else if (myToggle.isOn == false)
        {
            PlayerPrefs.SetInt("isAudioMuted", 0);
            mySlider.value = CurrentVolume;
            isMuted = 0;
        }
    }
}