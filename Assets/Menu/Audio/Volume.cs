using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Volume : MonoBehaviour
{
    [SerializeField] AudioMixer MenuSound;
    [SerializeField] Slider MusicSlider;
    [SerializeField] Slider MusicSliderSE;

    public static Volume Instance {get; private set;}

    void Awake()
    {
        MusicSlider.onValueChanged.AddListener(SetMusicVolume);
        MusicSliderSE.onValueChanged.AddListener(SetSEVolume);
        if(Instance != null && Instance != this){
            Destroy(this.gameObject);
        }
        else{
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    void SetMusicVolume(float value)
    {
        MenuSound.SetFloat("MenuMusic", value);
    }

    void SetSEVolume(float value)
    {
        MenuSound.SetFloat("SoundEffect", value);
    }

    public void Show(){
        this.gameObject.SetActive(true);
    }
}

