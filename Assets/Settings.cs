using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public Slider volumeSlider;
    public AudioMixer audioMixer;

    private void Start()
    {
        audioMixer.SetFloat("Volume", PlayerPrefs.GetFloat("VOLUME", volumeSlider ? volumeSlider.value : 0));
        if (volumeSlider != null)
            volumeSlider.value = audioMixer.GetFloat("Volume", out float volume) ? volume : 0;

        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("QUALITY", 1));
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
        PlayerPrefs.SetFloat("VOLUME", volume);
    }

    public void SetGraphicsLow()
    {
        QualitySettings.SetQualityLevel(0);
        PlayerPrefs.SetInt("QUALITY", 0);
    }

    public void SetGraphicsMid()
    {
        QualitySettings.SetQualityLevel(1);
        PlayerPrefs.SetInt("QUALITY", 1);
    }

    public void SetGraphicsHigh()
    {
        QualitySettings.SetQualityLevel(2);
        PlayerPrefs.SetInt("QUALITY", 2);
    }
}
