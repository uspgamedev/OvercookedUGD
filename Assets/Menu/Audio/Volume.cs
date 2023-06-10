using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Volume : MonoBehaviour
{
    [SerializeField] AudioMixer MenuSound;
    [SerializeField] Slider MusicSlider;
    [SerializeField] Slider MusicSliderSE;

    void Awake()
    {
        MusicSlider.onValueChanged.AddListener(SetMusicVolume);
        MusicSliderSE.onValueChanged.AddListener(SetSEVolume);
    }

    void SetMusicVolume(float value)
    {
        MenuSound.SetFloat("MenuMusic", value);
    }

    void SetSEVolume(float value)
    {
        MenuSound.SetFloat("SoundEffect", value);
    }
}

