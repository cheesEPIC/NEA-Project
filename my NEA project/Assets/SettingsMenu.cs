using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{

    public AudioMixer audioMixer;
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void SetFullscren(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }



    public static float gameSensitivity = 3;

    public void setSensitivity(float number)
    {
        gameSensitivity = number;
    }
}
