using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsSound : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;

    void Awake()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("audioVolume");
        volumeSlider.value = PlayerPrefs.GetFloat("audioVolume");
    }

    void Update()
    {
        ChangeSound();
    }

    void ChangeSound()
    {
        PlayerPrefs.SetFloat("audioVolume", volumeSlider.value);
        AudioListener.volume = volumeSlider.value;
    }

}
