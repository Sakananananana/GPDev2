using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class SettingsController : MonoBehaviour
{
    public AudioMixer audioMixer;

    public Slider HoriSlider;
    public float HoriValue;
    public TMP_Text HoriText;

    public Slider VertiSlider;
    public float VertiValue;
    public TMP_Text VertiText;

    public Slider BGMSlider;
    public float BGMValue;

    public Slider SFXSlider;
    public float SFXValue;

    // Start is called before the first frame update
    void Start()
    {
        HoriValue = PlayerPrefs.GetFloat("HoriSensitivity", 1);
        HoriSlider.value = HoriValue;

        VertiValue = PlayerPrefs.GetFloat("VertiSensitivity", 1);
        VertiSlider.value = VertiValue;


        BGMValue = PlayerPrefs.GetFloat("BGMVolume", 1);
        BGMSlider.value = BGMValue;

        SFXValue = PlayerPrefs.GetFloat("SFXVolume", 1);
        SFXSlider.value = SFXValue;
    }

    // Update is called once per frame
    void Update()
    {
        HoriText.text = HoriValue.ToString("0.00");
        HoriValue = HoriSlider.value;
        PlayerPrefs.SetFloat("HoriSensitivity", HoriValue);
        PlayerPrefs.Save();

        VertiText.text = VertiValue.ToString("0.00");
        VertiValue = VertiSlider.value;
        PlayerPrefs.SetFloat("VertiSensitivity", BGMValue);
        PlayerPrefs.Save();


        BGMValue = BGMSlider.value;
        audioMixer.SetFloat("BGM", Mathf.Log10(BGMValue) * 10);
        PlayerPrefs.SetFloat("BGMVolume", BGMValue);
        PlayerPrefs.Save();

        SFXValue = SFXSlider.value;
        audioMixer.SetFloat("SFX", Mathf.Log10(SFXValue) * 10);
        PlayerPrefs.SetFloat("SFXVolume", SFXValue);
        PlayerPrefs.Save();
    }
}
