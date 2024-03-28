using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public static float sensetivity = 200;
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }
    public void SetSensetivity(float sensetivity)
    {
        OptionsMenu.sensetivity = sensetivity;
    }
}
