using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{
    public AudioMixer audioMixer;
    public void SetVolume(float volume){
        audioMixer.SetFloat("Volume", volume);
    }

    public void low (){
        QualitySettings.SetQualityLevel(0);
    }

    public void mid (){
        QualitySettings.SetQualityLevel(1);
    }

    public void high (){
        QualitySettings.SetQualityLevel(2);
    }
}
