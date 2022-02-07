using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public Slider slider;

    public void ChangeVol()
    {
        PlayerPrefs.SetFloat("volume", slider.value / 100);
        AudioListener.volume = PlayerPrefs.GetFloat("volume");
    }
}
