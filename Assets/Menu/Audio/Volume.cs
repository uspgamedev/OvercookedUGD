using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Volume : MonoBehaviour
{
    [SerializeField] AudioMixer MenuSound;
    [SerializeField] Slider MusicSlider;

    void Awake()
    {
        MusicSlider.onValueChanged.AddListener(SetMusicVolume);
    }

    void SetMusicVolume(float value)
    {
        MenuSound.SetFloat("MenuMusic", value);
    }
}
